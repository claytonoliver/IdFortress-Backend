using IdFortress.Communication.Dto.Shared;

namespace IdFortress.Communication.Dto;

public record NotificacaoFraudeDto
{
    public Guid TransacaoId { get; set; }
    public string TipoBiometria { get; set; }
    public string TipoFraude { get; set; }
    public DateTime DataCaptura { get; set; }

    public Dispositivo Dispositivo { get; set; }
    public string[] CanalNotificacao { get; set; }
    public string NotificadoPor { get; set; }
    public Metadados Metadados { get; set; }
}
