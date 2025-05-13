using IdFortress.Application.Services.Contracts;
using IdFortress.Communication.Dto;
using IdFortress.Communication.Dto.Request;
using IdFortress.Communication.Dto.Response;
using IdFortress.Domain.Entitites;
using IdFortress.Infrastructure.Context;
using IdFortress.Infrastructure.Repositories;
using MongoDB.Driver;

public class DocumentosService : MongoDbGenericRepository<Validacao>, IDocumentosService
{
    private readonly IMongoCollection<Validacao> _validacoes;
    private readonly INotificacaoFraudeService _notificacaoService;

    public DocumentosService(
        IMongoDatabase database,
        MongoDbContext context,
        INotificacaoFraudeService notificacaoService)
        : base(database, "validacoes")
    {
        _validacoes = context.GetCollection<Validacao>("IFT_validacoes");
        _notificacaoService = notificacaoService;
    }

    public async Task<RespostaValidacao> ValidarAsync(RequisicaoDocumento request)
    {
        var tipoFraude = DetectarFraude(request.ImagemDocumentoBase64, request.ImagemSelfieBase64);
        var fraudeDetectada = tipoFraude != null;

        var entidade = new Validacao
        {
            TransacaoId = request.TransacaoId,
            TipoValidacao = "documentoscopia",
            ImagemBase64 = request.ImagemDocumentoBase64, // opcional: armazenar selfie separadamente
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
                TipoBiometria = "documentoscopia",
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
            Mensagem = fraudeDetectada ? "Fraude detectada na análise de documento." : "Documento validado com sucesso."
        };
    }

    private string? DetectarFraude(string imagemDoc, string imagemSelfie)
    {
        if (imagemDoc.Contains("editado") || imagemSelfie.Contains("falso"))
            return "documento falso";
        if (imagemDoc.Contains("baixaqualidade"))
            return "qualidade insuficiente";
        return null;
    }
}
