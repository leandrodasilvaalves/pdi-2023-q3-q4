using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Shared.Contracts.Options;
using Shared.Contracts.Repositories;
using Shared.Entities;

namespace Shared.Repositories
{
    public class ClaimRepository : RepositoryBase<Claim>, IClaimRepository
    {
        public ClaimRepository(IOptionsMonitor<MongoOptions> options) : base(options) { }

        protected override string CollectionName => "claims";

        public Task<List<Claim>> GetByAsync(string ispb, DateTime startDate, DateTime endDate)
        {
            return Task.FromResult(Collection
                .AsQueryable()
                .Where(x => (x.Donor.Account.Ispb == ispb || x.Claimer.Account.Ispb == ispb)
                       && x.UpdatedAt >= startDate && x.UpdatedAt <= endDate)
                .ToList());
        }
    }
}