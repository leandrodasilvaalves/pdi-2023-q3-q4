using Shared.Entities;

namespace Shared.Contracts.Repositories
{
    public interface IClaimRepository : IRepositoryBase<Claim>
    {
        Task<IEnumerable<Claim>> GetByAsync(string ispb, DateTime startDate, DateTime endDate);
    }
}