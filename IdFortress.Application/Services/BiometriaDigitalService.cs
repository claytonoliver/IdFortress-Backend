using IdFortress.Application.Services.Contracts;
using IdFortress.Communication.Dto;
using IdFortress.Communication.Dto.Request;
using IdFortress.Communication.Dto.Response;
using IdFortress.Domain.Entitites;
using IdFortress.Infrastructure.Context;
using IdFortress.Infrastructure.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;

public class BiometriaDigitalService : MongoDbGenericRepository<Validacao>, IBiometriaDigitalService
{
    private readonly IMongoCollection<Validacao> _validacoes;
    private readonly INotificacaoFraudeService _notificacaoService;

    public BiometriaDigitalService(
        IMongoDatabase database,
        MongoDbContext context,
        INotificacaoFraudeService notificacaoService)
        : base(database, "validacoes")
    {
        _validacoes = context.GetCollection<Validacao>("IFT_validacoes");
        _notificacaoService = notificacaoService;
    }

    public async Task<RespostaValidacao> ValidarAsync(RequisicaoBiometriaDigital request)
    {
        var tipoFraude = DetectarFraude(request.ImagemBase64);
        var fraudeDetectada = tipoFraude != null;

        var entidade = new Validacao
        {
            TransacaoId = new ObjectId(),
            TipoValidacao = "digital",
            ImagemBase64 = request.ImagemBase64,
            DataCaptura = request.DataCaptura,
            Dispositivo = new Dispositivo
            {
                Fabricante = request.Dispositivo.Fabricante,
                Modelo = request.Dispositivo.Modelo,
                SistemaOperacional = request.Dispositivo.SistemaOperacional
            },
            Metadados = new Metadados
            {
                Latitude = request.Metadados.Latitude,
                Longitude = request.Metadados.Longitude,
                IpOrigem = request.Metadados.IpOrigem
            },
            Sucesso = !fraudeDetectada,
            FraudeDetectada = fraudeDetectada,
            TipoFraude = tipoFraude
        };

        await _validacoes.InsertOneAsync(entidade);

        if (fraudeDetectada)
        {
            await _notificacaoService.NotificarFraudeAsync(new NotificacaoFraudeDto
            {
                TransacaoId = request.TransacaoId,
                TipoBiometria = "digital",
                TipoFraude = tipoFraude,
                DataCaptura = request.DataCaptura,
                Dispositivo = request.Dispositivo,
                CanalNotificacao = new[] { "sms", "email" },
                NotificadoPor = "sistema-de-monitoramento",
                Metadados = request.Metadados
            });
        }

        return new RespostaValidacao
        {
            Sucesso = !fraudeDetectada,
            FraudeDetectada = fraudeDetectada,
            TipoFraude = tipoFraude,
            Mensagem = fraudeDetectada ? "Fraude detectada na biometria digital." : "Validação digital realizada com sucesso."
        };
    }

    private string? DetectarFraude(string imagemBase64)
    {
        if (imagemBase64.Contains("fake")) return "digital falsa";
        if (imagemBase64.Contains("copia")) return "imagem copiada";
        return null;
    }
}
