using IdFortress.Application.Services.Contracts;
using IdFortress.Communication.Dto;
using IdFortress.Communication.Dto.Response;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/notificacoes")]
public class NotificacoesController : ControllerBase
{
    private readonly INotificacaoFraudeService _notificacaoService;

    public NotificacoesController(INotificacaoFraudeService notificacaoService)
    {
        _notificacaoService = notificacaoService;
    }

    [HttpPost("fraude")]
    public async Task<ActionResult<RespostaNotificacao>> NotificarFraude([FromBody] NotificacaoFraudeDto dto)
    {
        var resposta = await _notificacaoService.NotificarFraudeAsync(dto);
        return Ok(resposta);
    }
}
