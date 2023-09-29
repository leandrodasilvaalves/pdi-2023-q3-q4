using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Shared.Entities
{
    public class EntityBase
    {
        protected EntityBase() => Id = ObjectId.GenerateNewId().ToString();
        
        [BsonId]
        public string Id { get; set; }
    }
}