using Shared.Entities;

namespace Shared.Contracts.Repositories
{
    public interface IAccountRepository : IRepositoryBase<Account>
    {
        Task<Account> GetByAsync(string doucment, string ispb);
        Task<Account> GetByAsync(string branch, string number, string ispb);
    }
}