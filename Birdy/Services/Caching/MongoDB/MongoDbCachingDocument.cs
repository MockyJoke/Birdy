using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Birdy.Services.Caching.MongoDB
{
    public class MongoDbCachingDocument<IValue>
    {
        [BsonId]
        public string Id { get; set; }
        public IValue Value { get; set; }
    }
}