namespace IdFortress.Application.Dto.Shared;

public record Metadados
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string IpOrigem { get; set; }
}
