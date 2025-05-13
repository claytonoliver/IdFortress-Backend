using IdFortress.Application.Dto.Request;
using IdFortress.Application.Dto.Response;

namespace IdFortress.Application.Services.Contracts;

public interface IBiometriaFacialService
{
    Task<RespostaValidacao> ValidarAsync(RequisicaoBiometria request);
}
