using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Shared.Contracts;
using Shared.Contracts.Options;
using Shared.Models;

namespace Shared.Repositories
{

    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(IOptionsMonitor<MongoOptions> options) : base(options)
        { }

        protected override string CollectionName => "accounts";

        public async Task<Account> GetByAsync(string document, string ispb)
        {
            var filter = 
                Builders<Account>.Filter.Eq(x => x.Owner.Document, document) &
                Builders<Account>.Filter.Eq(x => x.Ispb, ispb);
            return await Collection.Find<Account>(filter).FirstOrDefaultAsync();
        }
    }
}