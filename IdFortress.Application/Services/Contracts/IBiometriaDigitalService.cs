using IdFortress.Communication.Dto.Request;
using IdFortress.Communication.Dto.Response;

namespace IdFortress.Application.Services.Contracts;

public interface IBiometriaDigitalService
{
    Task<RespostaValidacao> ValidarAsync(RequisicaoBiometriaDigital request);
}
