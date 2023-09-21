using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using Shared.Contracts;
using Shared.Contracts.Options;

namespace Shared.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        public RepositoryBase(IOptionsMonitor<MongoOptions> options)
        {
            LoadCustomConfiguration();
            Collection = ConfigureCollection(options.CurrentValue);
        }

        protected abstract string CollectionName { get; }

        protected IMongoCollection<T> Collection { get; }

        public Task InsertAsync(T model)
            => Collection.InsertOneAsync(model);

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