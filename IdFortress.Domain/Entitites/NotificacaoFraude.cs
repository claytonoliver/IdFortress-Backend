﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace IdFortress.Domain.Entitites;

public class NotificacaoFraude
{
    [BsonRepresentation(BsonType.String)]
    public ObjectId Id { get; set; }

    public Guid TransacaoId { get; set; }
    public string TipoBiometria { get; set; }
    public string TipoFraude { get; set; }
    public DateTime DataCaptura { get; set; }

    public Dispositivo Dispositivo { get; set; }
    public string[] CanalNotificacao { get; set; }
    public string NotificadoPor { get; set; }
    public Metadados Metadados { get; set; }

    public DateTime DataRegistro { get; set; } = DateTime.UtcNow;

}
