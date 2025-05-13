namespace IdFortress.Application.Dto.Request;

public record NotificacaoFraudeRequest
{
    public Guid TransacaoId { get; set; }
    public string TipoBiometria { get; set; }
    public string TipoFraude { get; set; }
    public DateTime DataCaptura { get; set; }
    public DispositivoDto Dispositivo { get; set; }
    public List<string> CanalNotificacao { get; set; }
    public string NotificadoPor { get; set; }
    public MetadadosDto Metadados { get; set; } 
}
