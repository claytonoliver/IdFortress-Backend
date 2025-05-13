using IdFortress.Application.Services.Contracts;
using IdFortress.Communication.Dto;
using IdFortress.Communication.Dto.Request;
using IdFortress.Communication.Dto.Response;
using IdFortress.Domain.Entitites;
using IdFortress.Infrastructure.Context;
using MongoDB.Driver;

public class NotificacaoFraudeService : INotificacaoFraudeService
{
    private readonly IMongoCollection<NotificacaoFraude> _notificacoes;

    public NotificacaoFraudeService(MongoDbContext context)
    {
        _notificacoes = context.GetCollection<NotificacaoFraude>("IFT_notificacoes_fraude");
    }

    public async Task<RespostaNotificacao> NotificarFraudeAsync(NotificacaoFraudeDto dto)
    {
        var entidade = new NotificacaoFraude
        {
            TransacaoId = dto.TransacaoId,
            TipoBiometria = dto.TipoBiometria,
            TipoFraude = dto.TipoFraude,
            DataCaptura = dto.DataCaptura,
            Dispositivo = new Dispositivo
            {
                Fabricante = dto.Dispositivo.Fabricante,
                Modelo = dto.Dispositivo.Modelo,
                SistemaOperacional = dto.Dispositivo.SistemaOperacional
            },
            Metadados = new Metadados
            {
                Latitude = dto.Metadados.Latitude,
                Longitude = dto.Metadados.Longitude,
                IpOrigem = dto.Metadados.IpOrigem
            },
            CanalNotificacao = dto.CanalNotificacao,
            NotificadoPor = dto.NotificadoPor,
            DataRegistro = DateTime.UtcNow
        };

        await _notificacoes.InsertOneAsync(entidade);

        return new RespostaNotificacao
        {
            Sucesso = true,
            Mensagem = "Notificação registrada com sucesso.",
            DataRegistro = entidade.DataRegistro
        };
    }
}
