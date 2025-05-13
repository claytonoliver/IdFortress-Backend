using IdFortress.Application.Services.Contracts;
using IdFortress.Communication.Dto.Request;
using Microsoft.AspNetCore.Mvc;

namespace IdFortress.API.Controllers;

[ApiController]
[Route("api/biometria/facial")]
public class BiometriaFacialController : ControllerBase
{
    private readonly IBiometriaFacialService _service;

    public BiometriaFacialController(IBiometriaFacialService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Validar([FromBody] RequisicaoBiometriaFacialDto request)
    {
        if (request == null)
            return BadRequest("Requisição inválida.");

        var resultado = await _service.ValidarAsync(request);

        if (resultado.FraudeDetectada)
            return StatusCode(202, resultado); // Accepted, mas com fraude

        return Ok(resultado); // Sucesso normal
    }
}
