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

        public async Task<IEnumerable<Claim>> GetByAsync(string ispb, DateTime startDate, DateTime endDate)
        {
            var filter =
                Builders<Claim>.Filter.Eq(x => x.Donor.Account.Ispb, ispb) &
                Builders<Claim>.Filter.Gte(x => x.UpdatedAt, startDate) &
                Builders<Claim>.Filter.Lte(x => x.UpdatedAt, endDate);
            return await Collection.Find(filter).ToListAsync();
        }
    }
}