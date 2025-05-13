namespace IdFortress.Application.Dto.Request;

public class RequisicaoBiometria
{
    public Guid TransacaoId { get; set; }
    public string TipoBiometria { get; set; } // "facial" ou "digital"
    public DateTime DataCaptura { get; set; }
    public DispositivoDto Dispositivo { get; set; }
    public MetadadosDto Metadados { get; set; }
    public string ImagemBase64 { get; set; }
}
