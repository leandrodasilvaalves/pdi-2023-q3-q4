using Shared.Entities;

namespace Shared.Contracts.Repositories
{
    public interface IRepositoryBase<T> where T : EntityBase
    {
        Task<T> GetByAsync(string id);
        Task InsertAsync(T model);
        Task UpdateAsync(T model);
    }
}