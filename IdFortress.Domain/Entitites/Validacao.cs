using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace IdFortress.Domain.Entitites;

public class Validacao
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid TransacaoId { get; set; }

    [BsonElement("tipoBiometria")]
    public string TipoBiometria { get; set; }

    [BsonElement("resultado")]
    public string Resultado { get; set; }

    [BsonElement("tipoFraude")]
    [BsonIgnoreIfNull]
    public string? TipoFraude { get; set; }

    [BsonElement("dataCaptura")]
    public DateTime DataCaptura { get; set; }

    [BsonElement("dispositivo")]
    public Dispositivo Dispositivo { get; set; }

    [BsonElement("metadados")]
    public Metadados Metadados { get; set; }

    [BsonElement("imagemValida")]
    public bool ImagemValida { get; set; }

    [BsonElement("detalhesValidacao")]
    public DetalhesValidacao DetalhesValidacao { get; set; }

    [BsonElement("criadoEm")]
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
}
