using IdFortress.Infrastructure.Config;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace IdFortress.Infrastructure.Context;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IOptions<MongoDbSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        _database = client.GetDatabase(settings.Value.DataBaseName);
    }

    public IMongoCollection<T> GetCollection<T>(string collectionName)
    {
        return _database.GetCollection<T>(collectionName);
    }

    //public virtual DbSet<ChamadoModel> Chamados { get; set; }
    //public virtual DbSet<GeneroModel> Genero { get; set; }
    //public virtual DbSet<HistoricoOcorrenciaModel> HistoricoOcorrencia { get; set; }
    //public virtual DbSet<HistoricoServicoStatusModel> HistoricoServico { get; set; }
    //public virtual DbSet<RoleModel> Roles { get; set; }
    //public virtual DbSet<StatusChamadoModel> StatusChamado { get; set; }
    //public virtual DbSet<StatusServicoModel> StatusServico { get; set; }
    //public virtual DbSet<TipoDeOcorrenciaModel> TipoOcorrencia { get; set; }
    //public virtual DbSet<UsuarioModel> Usuarios { get; set; }
    //public virtual DbSet<VeiculoModel> Veiculos { get; set; }
}
