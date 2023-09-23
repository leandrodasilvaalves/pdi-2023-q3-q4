using Shared.Models;

namespace Shared.Contracts
{
    public interface IAccountRepository : IRepositoryBase<Account>
    {
        Task<Account> GetByAsync(string doucment, string ispb);
    }
}