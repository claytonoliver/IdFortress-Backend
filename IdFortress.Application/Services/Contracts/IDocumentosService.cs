using IdFortress.Communication.Dto.Request;
using IdFortress.Communication.Dto.Response;

namespace IdFortress.Application.Services.Contracts;

public interface IDocumentosService
{
    Task<RespostaValidacao> ValidarAsync(RequisicaoDocumento request);
}
