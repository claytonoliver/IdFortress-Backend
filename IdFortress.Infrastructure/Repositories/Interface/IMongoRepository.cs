using MongoDB.Bson;

namespace IdFortress.Infrastructure.Repositories.Interface;

public interface IMongoRepository<T> where T : class
{
    Task AddAsync(T entity);
    Task DeleteAsync(ObjectId id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(ObjectId id);
    Task UpdateAsync(ObjectId id, T entity);
}
                               