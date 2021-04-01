using dotenv.net;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _users;

        public UserService(IMongoDbSettings settings)
        {
            var client = new MongoClient(DotEnv.Read()["MONGODB_URI"]);
            var database = client.GetDatabase(DotEnv.Read()["MONGODB_DB"]);

            _users = database.GetCollection<User>("users");
        }

        public async Task<List<User>> Get()
        {
            var x = await _users.FindAsync(user => true);
            return x.ToList();
        }
            
        
        public async Task<User> Get(string id)
        {
            var x = await _users.FindAsync<User>(user => user.Id == id);
            return x.SingleOrDefault();
        }
            

        public async Task<User> Find(string email)
        {
            var x = await _users.FindAsync<User>(user => user.Email == email);
            return x.SingleOrDefault();
        }

        public async Task<User> Create(User user)
        {
            user.Admin = false;
            user.Id = null;
            await _users.InsertOneAsync(user);
            return user;
        }

        public async void Update(string id, User userIn) =>
            await _users.ReplaceOneAsync(user => user.Id == id, userIn);

           

        public async void Remove(User userIn) =>
            await _users.DeleteOneAsync(user => user.Id == userIn.Id);

        public async void Remove(string id) =>
            await _users.DeleteOneAsync(user => user.Id == id);
    }
}
