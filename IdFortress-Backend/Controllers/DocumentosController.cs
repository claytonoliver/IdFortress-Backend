using IdFortress.Application.Services.Contracts;
using IdFortress.Communication.Dto.Request;
using IdFortress.Communication.Dto.Response;
using Microsoft.AspNetCore.Mvc;

namespace IdFortress_Backend.Controllers;

[ApiController]
[Route("api/documentos")]
public class DocumentosController : ControllerBase
{
    private readonly IDocumentosService _service;

    public DocumentosController(IDocumentosService service)
    {
        _service = service;
    }

    [HttpPost("validar")]
    public async Task<ActionResult<RespostaValidacao>> ValidarDocumento([FromBody] RequisicaoDocumento dto)
    {
        var resposta = await _service.ValidarAsync(dto);
        return Ok(resposta);
    }
}