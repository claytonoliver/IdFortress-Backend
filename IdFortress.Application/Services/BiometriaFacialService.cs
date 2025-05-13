using IdFortress.Application.Dto.Request;
using IdFortress.Application.Dto.Response;
using IdFortress.Application.Services.Contracts;
using IdFortress.Domain.Entitites;

namespace IdFortress.Application.Services;

public class BiometriaFacialService : IBiometriaFacialService
{
    private readonly IMongoDbRepository _repository;
    private readonly INotificacaoService _notificacaoService;

    public BiometriaFacialService(IMongoDbRepository repository, INotificacaoService notificacaoService)
    {
        _repository = repository;
        _notificacaoService = notificacaoService;
    }

    public async Task<RespostaValidacao> ValidarAsync(RequisicaoBiometria request)
    {
        var isFraude = SimularFraude(request);
        var resultado = new RespostaValidacao
        {
            TransacaoId = request.TransacaoId,
            Resultado = isFraude ? "fraude" : "sucesso",
            FraudeDetectada = isFraude,
            TipoFraude = isFraude ? "deepfake" : null,
            Mensagem = isFraude ? "Fraude simulada detectada (deepfake)." : "Validação realizada com sucesso."
        };

        var validacao = new Validacao
        {
            TransacaoId = request.TransacaoId,
            TipoBiometria = request.TipoBiometria,
            Resultado = resultado.Resultado,
            TipoFraude = resultado.TipoFraude,
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
            ImagemValida = true, // Simulação
            DetalhesValidacao = new DetalhesValidacao
            {
                Formato = "JPEG",
                Resolucao = "1280x720",
                TemMetadados = true,
                SuspeitaLiveness = isFraude
            },
            CriadoEm = DateTime.UtcNow
        };

        await _repository.SalvarValidacaoAsync(validacao);

        if (isFraude)
        {
            var notificacao = new NotificacaoFraudeRequest
            {
                TransacaoId = request.TransacaoId,
                TipoBiometria = request.TipoBiometria,
                TipoFraude = resultado.TipoFraude,
                DataCaptura = request.DataCaptura,
                Dispositivo = request.Dispositivo,
                CanalNotificacao = new List<string> { "sms", "email" },
                NotificadoPor = "sistema-de-monitoramento",
                Metadados = request.Metadados
            };

            await _notificacaoService.EnviarNotificacaoAsync(notificacao);
        }

        return resultado;
    }

    private bool SimularFraude(RequisicaoBiometria request)
    {
        // Exemplo simples: simula fraude se o modelo do dispositivo contiver "Emulador"
        return request.Dispositivo.Modelo?.ToLower().Contains("emulador") == true;
    }
}
