using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using Shared.Contracts.Options;
using Shared.Contracts.Repositories;
using Shared.Entities;

namespace Shared.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : EntityBase
    {
        public RepositoryBase(IOptionsMonitor<MongoOptions> options)
        {
            LoadCustomConfiguration();
            Collection = ConfigureCollection(options.CurrentValue);
        }

        protected abstract string CollectionName { get; }

        protected IMongoCollection<T> Collection { get; }

        public Task<T> GetByAsync(string id) => Collection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<bool> ExistsAsync(string id)
        {
            var filter = Builders<T>.Filter.Eq(x => x.Id, id);
            return (await Collection.CountDocumentsAsync(filter)) > 0;
        }

        public Task InsertAsync(T model)
            => Collection.InsertOneAsync(model);

        public Task UpdateAsync(T model)
            => Collection.ReplaceOneAsync(x => x.Id == model.Id, model);

        private static void LoadCustomConfiguration()
        {
            var conventionPack = new ConventionPack { new CamelCaseElementNameConvention(), new EnumRepresentationConvention(BsonType.String) };
            ConventionRegistry.Register("camelCase", conventionPack, t => true);
            ConventionRegistry.Register("EnumStringConvention", conventionPack, t => true);
        }

        private IMongoCollection<T> ConfigureCollection(MongoOptions options)
        {
            var mongoClient = new MongoClient(options.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(options.DatabaseName);
            return mongoDatabase.GetCollection<T>(CollectionName);
        }
    }
}