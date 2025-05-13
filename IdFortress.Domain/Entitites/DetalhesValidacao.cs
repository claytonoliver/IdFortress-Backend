namespace IdFortress.Domain.Entitites;

public class DetalhesValidacao
{
    public string Resolucao { get; set; }
    public string Formato { get; set; }
    public bool TemMetadados { get; set; }
    public bool SuspeitaLiveness { get; set; }
}