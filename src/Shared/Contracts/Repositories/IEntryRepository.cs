using Shared.Contracts.Models;
using Shared.Entities;

namespace Shared.Contracts.Repositories
{
    public interface IEntryRepository : IRepositoryBase<Entry>
    {
        Task<Entry> GetByAsync(AddressingKey addressingKey);
    }
}