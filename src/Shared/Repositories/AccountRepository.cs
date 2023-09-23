using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Shared.Contracts.Options;
using Shared.Contracts.Repositories;
using Shared.Entities;

namespace Shared.Repositories
{

    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(IOptionsMonitor<MongoOptions> options) : base(options)
        { }

        protected override string CollectionName => "accounts";

        public Task<Account> GetByAsync(string document, string ispb)
        {
            var filter =
                Builders<Account>.Filter.Eq(x => x.Owner.Document, document) &
                Builders<Account>.Filter.Eq(x => x.Ispb, ispb);
            return Collection.Find<Account>(filter).FirstOrDefaultAsync();
        }

        public Task<Account> GetByAsync(string branch, string number, string ispb)
        {
            var filter =
                Builders<Account>.Filter.Eq(x => x.Number, number) &
                Builders<Account>.Filter.Eq(x => x.Branch, branch) &
                Builders<Account>.Filter.Eq(x => x.Ispb, ispb);
            return Collection.Find<Account>(filter).FirstOrDefaultAsync();
        }
    }
}