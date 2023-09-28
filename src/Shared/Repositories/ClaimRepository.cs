using Microsoft.Extensions.Options;
using Shared.Contracts.Options;
using Shared.Contracts.Repositories;
using Shared.Entities;

namespace Shared.Repositories
{
    public class ClaimRepository : RepositoryBase<Claim>, IClaimRepository
    {
        public ClaimRepository(IOptionsMonitor<MongoOptions> options) : base(options)
        {
        }

        protected override string CollectionName => "claims";
    }
}