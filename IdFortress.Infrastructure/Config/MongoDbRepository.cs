using IdFortress.Application.Request;
using IdFortress.Infrastructure.Repositories;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace IdFortress.Infrastructure.Config;

public class MongoDbRepository
{
    private readonly IMongoCollection<NotificacaoFraudeRequest> _colecao;

    public MongoDbRepository(IOptions<MongoDbSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        var database = client.GetDatabase(settings.Value.Database);
        _colecao = database.GetCollection<NotificacaoFraudeRequest>("Fraudes");
    }

    public async Task SalvarNotificacaoAsync(NotificacaoFraudeRequest notificacao)
    {
        await _colecao.InsertOneAsync(notificacao);
    }
}
