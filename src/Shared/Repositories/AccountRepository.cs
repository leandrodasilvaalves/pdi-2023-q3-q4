using Microsoft.Extensions.Options;
using Shared.Contracts;
using Shared.Contracts.Options;
using Shared.Models;

namespace Shared.Repositories
{

    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(IOptionsMonitor<MongoOptions> options) : base(options)
        {}

        protected override string CollectionName => "accounts";
    }
}