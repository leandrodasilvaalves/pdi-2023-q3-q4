using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Shared.Entities
{
    public class EntityBase
    {
        [BsonId]
        public ObjectId Id { get; set; }
    }
}