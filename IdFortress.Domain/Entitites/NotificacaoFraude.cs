using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace IdFortress.Domain.Entitites;

public class NotificacaoFraude
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid TransacaoId { get; set; }

    [BsonElement("tipoBiometria")]
    public string TipoBiometria { get; set; }

    [BsonElement("tipoFraude")]
    public string TipoFraude { get; set; }

    [BsonElement("dataCaptura")]
    public DateTime DataCaptura { get; set; }

    [BsonElement("dispositivo")]
    public Dispositivo Dispositivo { get; set; }

    [BsonElement("canalNotificacao")]
    public List<string> CanalNotificacao { get; set; }

    [BsonElement("notificadoPor")]
    public string NotificadoPor { get; set; }

    [BsonElement("metadados")]
    public Metadados Metadados { get; set; }

    [BsonElement("dataNotificacao")]
    public DateTime DataNotificacao { get; set; } = DateTime.UtcNow;

}
