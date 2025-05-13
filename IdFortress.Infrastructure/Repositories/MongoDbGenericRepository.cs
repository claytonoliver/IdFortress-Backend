using IdFortress.Infrastructure.Repositories.Interface;
using MongoDB.Bson;
using MongoDB.Driver;

namespace IdFortress.Infrastructure.Repositories;

public class MongoDbGenericRepository<T> where T : class
{
    private readonly IMongoCollection<T> _collection;

    public MongoDbGenericRepository(IMongoDatabase database, string collectionName)
    {
        _collection = database.GetCollection<T>(collectionName);
    }

    public async Task AddAsync(T entity)
    {
        await _collection.InsertOneAsync(entity);
    }

    public async Task DeleteAsync(ObjectId id)
    {
        var filter = Builders<T>.Filter.Eq("_id", id);
        await _collection.DeleteOneAsync(filter);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        var entities = await _collection.FindAsync(_ => true);
        return await entities.ToListAsync();
    }

    public async Task<T> GetByIdAsync(ObjectId id)
    {
        var filter = Builders<T>.Filter.Eq("_id", id);
        var entity = await _collection.FindAsync(filter);
        return await entity.FirstOrDefaultAsync();
    }

    public async Task UpdateAsync(ObjectId id, T entity)
    {
        var filter = Builders<T>.Filter.Eq("_id", id);
        await _collection.ReplaceOneAsync(filter, entity);
    }
}

