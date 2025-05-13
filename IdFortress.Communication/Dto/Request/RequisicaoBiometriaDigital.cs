using IdFortress.Communication.Dto.Shared;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace IdFortress.Communication.Dto.Request;

public record RequisicaoBiometriaDigital
{
    [BsonRepresentation(BsonType.String)]
    public Guid TransacaoId { get; set; }
    public string ImagemBase64 { get; set; } // Impressão digital
    public DateTime DataCaptura { get; set; }
    public Dispositivo Dispositivo { get; set; }
    public Metadados Metadados { get; set; }
}
