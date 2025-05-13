namespace IdFortress.Application.Dto.Response;

public record RespostaValidacao
{
    public Guid TransacaoId { get; set; }
    public bool FraudeDetectada { get; set; }
    public string? Resultado { get; set; }
    public string? TipoFraude { get; set; }
    public string? Mensagem { get; set; }
}
