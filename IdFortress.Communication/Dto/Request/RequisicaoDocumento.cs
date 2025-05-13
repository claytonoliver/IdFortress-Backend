using IdFortress.Communication.Dto.Shared;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace IdFortress.Communication.Dto.Request;

public class RequisicaoDocumento
{
    [BsonRepresentation(BsonType.String)]
    public Guid TransacaoId { get; set; }
    public string ImagemDocumentoBase64 { get; set; }
    public string ImagemSelfieBase64 { get; set; }
    public DateTime DataCaptura { get; set; }
    public Dispositivo Dispositivo { get; set; }
    public Metadados Metadados { get; set; }
}
