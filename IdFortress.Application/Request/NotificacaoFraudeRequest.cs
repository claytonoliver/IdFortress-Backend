namespace IdFortress.Application.Request;

public record NotificacaoFraudeRequest
{
    public string? Id { get; set; }
    public string? Nome { get; set; }
    public string? Email { get; set; }
    public string? Telefone { get; set; }
    public string? Mensagem { get; set; }
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
}
