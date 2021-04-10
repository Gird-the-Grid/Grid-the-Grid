using BlazorServerAPI.Models;
using dotenv.net;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using MongoDB.Bson;

namespace BlazorServerAPI.Repository
{
    public class BaseRepository<T>
    {
        //TODO: firststef: Aici am implementat repository-uld e baza folosind un template, dar are anumite dezavantaje:
        // - nu poti pune ca parametru un template si sa ii accesezi elementele (doc.Id) pentru ca nu are asa ceva
        //Am incercat sa implementez asta si folosind in loc de T BaseEntity si IBaseEntity, 
        // dar nu am reusit pentru ca nu ma lasa sa fac cast la tipul care voiam eu (in User Repository)
        //  - asta probabil se poate rezolva dolosind Builders, cum am facut mai jos
        // si nu ma lasa in User Repository sa accesez doc.Email pt ca BaseEntity nu are definitie pentru email
        //  - dar este probabil ca si asta sa se poata rezolva folosind Buildere in loc de lambda functii pentru filtrare

        protected readonly IMongoCollection<T> _documents;

        public BaseRepository(IMongoDbSettings settings, string collection)
        {
            var client = new MongoClient(DotEnv.Read()["MONGODB_URI"]);
            var database = client.GetDatabase(DotEnv.Read()["MONGODB_DB"]);
            _documents = database.GetCollection<T>(collection);
        }

        public async Task<List<T>> Get()
        {
            var x = await _documents.FindAsync<T>(document => true);
            return x.ToList();
        }

        //TODO: check that all bellow work with users (test on UserController or WeatherController)
        public async Task<T> Get(string id)
        {
            var filter = Builders<T>.Filter.Eq("_id", new ObjectId(id));
            var x = await _documents.FindAsync<T>(filter);
            return x.SingleOrDefault();
        }

        public async Task Update(string id, T documentIn)
        {
            var filter = Builders<T>.Filter.Eq("_id", new ObjectId(id));
            await _documents.ReplaceOneAsync(filter, documentIn);
        }

        public async Task Remove(string id)
        {
            var filter = Builders<T>.Filter.Eq("_id", new ObjectId(id));
            await _documents.DeleteOneAsync(filter);
        }
    }
}
