using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Shared.Models
{
    public class BaseModel
    {
        [BsonId]
        public ObjectId Id { get; set; }
    }
}