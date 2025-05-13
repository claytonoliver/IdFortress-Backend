using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Runtime.CompilerServices;

namespace IdFortress.Domain.Entitites;

public class Validacao
{
    [BsonRepresentation(BsonType.String)]
    public ObjectId Id { get; set; }
    [BsonRepresentation(BsonType.String)]
    public Guid TransacaoId { get; set; }
    public string TipoValidacao { get; set; } = "facial";
    public string? ImagemBase64 { get; set; }
    public DateTime DataCaptura { get; set; }

    public Dispositivo? Dispositivo { get; set; }
    public Metadados? Metadados { get; set; }

    public bool Sucesso { get; set; }
    public bool FraudeDetectada { get; set; }
    public string? TipoFraude { get; set; }

    public DateTime DataProcessamento { get; set; } = DateTime.UtcNow;
}