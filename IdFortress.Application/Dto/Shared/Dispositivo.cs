namespace IdFortress.Application.Dto.Shared;

public record Dispositivo
{
    public string Fabricante { get; set; }
    public string Modelo { get; set; }
    public string SistemaOperacional { get; set; }
}
