using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Birdy.Models;
using Birdy.Util;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Birdy.Services.Caching.MongoDB
{
    public class MongoDbCachingService<IKey, IValue> : ICachingService<IKey, IValue>
    {
        private IMongoClient mongoClient;
        private IMongoDatabase mongoDatabase;
        private IMongoCollection<MongoDbCachingDocument<IValue>> mongoCollection;
        private IHashGenerator hashGenerator;

        public MongoDbCachingService(IMongoClient mongoClient, string databaseName, string collectionName, IHashGenerator hashGenerator)
        {
            this.mongoClient = mongoClient;
            this.mongoDatabase = mongoClient.GetDatabase(databaseName);
            this.mongoCollection = mongoDatabase.GetCollection<MongoDbCachingDocument<IValue>>(collectionName);
            this.hashGenerator = hashGenerator;
        }

        public MongoDbCachingService(IMongoDatabase mongoDatabase, string collectionName, IHashGenerator hashGenerator)
        {
            this.mongoDatabase = mongoDatabase;
            this.mongoCollection = mongoDatabase.GetCollection<MongoDbCachingDocument<IValue>>(collectionName);
            this.hashGenerator = hashGenerator;
        }
        public MongoDbCachingService(IMongoCollection<MongoDbCachingDocument<IValue>> mongoCollection, IHashGenerator hashGenerator)
        {
            this.mongoCollection = mongoCollection;
            this.hashGenerator = hashGenerator;
        }

        public Task DeleteAsync(IKey key)
        {
            return mongoCollection.DeleteOneAsync(doc => doc.Id.Equals(key));
        }

        public async Task<IValue> GetAsync(IKey key)
        {
            IAsyncCursor<MongoDbCachingDocument<IValue>> documents = await mongoCollection.FindAsync(doc => doc.Id.Equals(GenerateIdFromKey(key)));
            MongoDbCachingDocument<IValue> firstDocument = await documents.FirstOrDefaultAsync();
            return firstDocument.Value;
        }

        public async Task<bool> HasKeyAsync(IKey key)
        {
            IAsyncCursor<MongoDbCachingDocument<IValue>> documents = await mongoCollection.FindAsync(doc => doc.Id.Equals(GenerateIdFromKey(key)));
            MongoDbCachingDocument<IValue> firstDocument = await documents.FirstOrDefaultAsync();
            return firstDocument != null;
        }

        public Task SetAsync(IKey key, IValue value)
        {
            return mongoCollection.InsertOneAsync(new MongoDbCachingDocument<IValue>()
            {
                Id = GenerateIdFromKey(key),
                Value = value
            });
        }
        private string GenerateIdFromKey(IKey key)
        {
            return this.hashGenerator.GenerateHash(Encoding.UTF8.GetBytes(key.ToString()));
        }
    }
}