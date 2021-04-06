using BlazorServerAPI.Models;
using BlazorServerAPI.Models.Entities;
using dotenv.net;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServerAPI.Repository
{
    public class UserRepository : BaseRepository<User>
    {

        public UserRepository(IMongoDbSettings settings) : base(settings, "users")
        { }

        public async Task<User> FindUserByEmail(string email)
        {
            var x = await _documents.FindAsync<User>(user => user.Email == email);
            return x.SingleOrDefault();
        }

        public async Task<User> CreateUser(User user)
        {
            user.Admin = false;
            user.Id = null;
            await _documents.InsertOneAsync(user);
            return user;
        }

    }
}
