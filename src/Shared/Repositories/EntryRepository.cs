using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Shared.Contracts.Models;
using Shared.Contracts.Options;
using Shared.Contracts.Repositories;
using Shared.Entities;

namespace Shared.Repositories
{
    public class EntryRepository : RepositoryBase<Entry>, IEntryRepository
    {
        public EntryRepository(IOptionsMonitor<MongoOptions> options) : base(options)
        {}

        protected override string CollectionName => "entries";

        public Task<Entry> GetByAsync(AddressingKey addressingKey)
        {
            var filter =
                Builders<Entry>.Filter.Eq(x => x.AddressingKey.Value, addressingKey.Value) &
                Builders<Entry>.Filter.Eq(x => x.AddressingKey.Type, addressingKey.Type);
            return Collection.Find<Entry>(filter).FirstOrDefaultAsync();
        }
    }
}