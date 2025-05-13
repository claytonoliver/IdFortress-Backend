namespace IdFortress.Application.Dto.Request;

public record RequisicaoDocumento
{
    public Guid TransacaoId { get; set; }
    public DateTime DataCaptura { get; set; }
    public DispositivoDto Dispositivo { get; set; }
    public MetadadosDto Metadados { get; set; }
    public string ImagemDocumentoBase64 { get; set; }
    public string ImagemSelfieBase64 { get; set; }
}
