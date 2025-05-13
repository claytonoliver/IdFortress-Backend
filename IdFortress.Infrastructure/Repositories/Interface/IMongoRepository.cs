namespace IdFortress.Infrastructure.Repositories.Interface;

public interface IMongoRepository<T>
{
    Task<T> SalvarAsync();
    Task<T> UpdateByIdAsync(string id);
    Task<T> DeleteByIdAsync(string id);
    Task<T> GetAllAsync();
    Task<T> GetByIdAsync(string id);
}
