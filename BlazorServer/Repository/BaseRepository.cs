using dotenv.net;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using MongoDB.Bson;
using BlazorServerAPI.Settings;
using System;
using BlazorServerAPI.Models.Entities;

namespace BlazorServerAPI.Repository
{
    public class BaseRepository<T> where T : IBaseEntity
    {

        protected readonly IMongoCollection<T> _documents;

        public BaseRepository(IMongoDbSettings settings, string collection)
        {
            var client = new MongoClient(DotEnv.Read()["MONGODB_URI"]);
            var database = client.GetDatabase(DotEnv.Read()["MONGODB_DB"]);
            _documents = database.GetCollection<T>(collection);
        }

        public async Task<List<T>> Get()
        {
            var entityDocs = await _documents.FindAsync<T>(document => true);
            return entityDocs.ToList();
        }

        //TODO: check that all bellow work with users (test on UserController or WeatherController)
        public async Task<T> Get(string id)
        {
            var filter = Builders<T>.Filter.Eq("_id", new ObjectId(id));
            var docsFilteredById = await _documents.FindAsync<T>(filter);
            return docsFilteredById.SingleOrDefault();
        }

        public async Task Update(string id, T documentIn)
        {
            var filter = Builders<T>.Filter.Eq("_id", new ObjectId(id));
            documentIn.UpdatedAt = DateTime.Now;
            await _documents.ReplaceOneAsync(filter, documentIn);
        }

        public async Task Remove(string id)
        {
            var filter = Builders<T>.Filter.Eq("_id", new ObjectId(id));
            await _documents.DeleteOneAsync(filter);
        }
    }
}
