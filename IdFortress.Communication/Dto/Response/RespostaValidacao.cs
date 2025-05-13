namespace IdFortress.Communication.Dto.Response;

public record RespostaValidacao
{
    public bool Sucesso { get; set; }
    public bool FraudeDetectada { get; set; }
    public string? TipoFraude { get; set; }
    public string Mensagem { get; set; }
}
