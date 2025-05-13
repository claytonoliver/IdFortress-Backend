using IdFortress.Communication.Dto.Shared;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

public class RequisicaoBiometriaFacialDto
{
    [BsonRepresentation(BsonType.String)]
    public Guid TransacaoId { get; set; }
    public string ImagemBase64 { get; set; }
    public DateTime DataCaptura { get; set; }
    public Dispositivo Dispositivo { get; set; }
    public Metadados Metadados { get; set; }
}
