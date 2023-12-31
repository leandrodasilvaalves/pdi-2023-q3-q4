using Shared.Entities;

namespace Shared.Contracts.Repositories
{
    public interface IRepositoryBase<T> where T : EntityBase
    {
        Task<bool> ExistsAsync(string id);
        Task<T> GetByAsync(string id);
        Task InsertAsync(T model);
        Task UpdateAsync(T model);
        Task DeleteAsync(string id);
    }
}