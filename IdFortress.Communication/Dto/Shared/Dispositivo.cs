namespace IdFortress.Communication.Dto.Shared;

public record Dispositivo
{
    public string Fabricante { get; set; }
    public string Modelo { get; set; }
    public string SistemaOperacional { get; set; }
}
