using IdFortress.Application.Services.Contracts;
using IdFortress.Communication.Dto.Request;
using IdFortress.Communication.Dto.Response;
using Microsoft.AspNetCore.Mvc;

namespace IdFortress_Backend.Controllers;

[ApiController]
[Route("api/biometria")]
public class BiometriaDigitalController : ControllerBase
{
    private readonly IBiometriaDigitalService _service;

    public BiometriaDigitalController(IBiometriaDigitalService service)
    {
        _service = service;
    }

    [HttpPost("digital")]
    public async Task<ActionResult<RespostaValidacao>> ValidarBiometriaDigital([FromBody] RequisicaoBiometriaDigital dto)
    {
        var resposta = await _service.ValidarAsync(dto);
        return Ok(resposta);
    }
}