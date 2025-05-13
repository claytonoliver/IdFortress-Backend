using IdFortress.Communication.Dto;
using IdFortress.Communication.Dto.Response;

namespace IdFortress.Application.Services.Contracts;

public interface INotificacaoFraudeService
{
    Task<RespostaNotificacao> NotificarFraudeAsync(NotificacaoFraudeDto dto);
}
