using IdFortress.Infrastructure.Config;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace IdFortress.Infrastructure.Context;

public class MongoContext
{
    private readonly IMongoDatabase _database;
    public MongoContext(IOptions<MongoDbSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        _database = client.GetDatabase(settings.Value.Database);
    }
    public IMongoCollection<T> GetCollection<T>(string name)
    {
        return _database.GetCollection<T>(name);
    }
}
