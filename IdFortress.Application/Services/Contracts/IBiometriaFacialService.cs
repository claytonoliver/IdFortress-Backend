using IdFortress.Communication.Dto.Request;
using IdFortress.Communication.Dto.Response;
using IdFortress.Infrastructure.Repositories.Interface;

namespace IdFortress.Application.Services.Contracts;

public interface IBiometriaFacialService
{
    Task<RespostaValidacao> ValidarAsync(RequisicaoBiometriaFacialDto request);
}

