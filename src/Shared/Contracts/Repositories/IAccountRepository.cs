using Shared.Entities;

namespace Shared.Contracts.Repositories
{
    public interface IAccountRepository : IRepositoryBase<Account>
    {
        Task<Account> GetByAsync(string doucment, string ispb);
    }
}