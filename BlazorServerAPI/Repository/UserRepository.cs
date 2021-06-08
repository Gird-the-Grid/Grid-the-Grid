using BlazorServerAPI.Models.Entities;
using BlazorServerAPI.Settings;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace BlazorServerAPI.Repository
{
    public class UserRepository : BaseRepository<User>
    {

        public UserRepository(IMongoDbSettings settings) : base(settings, "users")
        { }

        public async Task<User> FindUserByEmail(string email)
        {
            return (await _documents.FindAsync<User>(user => user.Email == email && user.Activated)).SingleOrDefault();
        }

        public async Task<User> FindUserById(string userId)
        {
            return (await _documents.FindAsync<User>(user => user.Id == userId)).FirstOrDefault();
        }

        public async Task<User> CreateUser(User user)
        {
            user.Id = null; 
            await _documents.InsertOneAsync(user);
            return user;
        }

        public async Task<User> ConfirmUser(string userId)
        {
            var unconfirmedUser = (await _documents.FindAsync<User>(user => user.Id == userId && !user.Activated)).SingleOrDefault();
            if (unconfirmedUser != null)
            {
                unconfirmedUser.Activated = true;
                await Update(userId, unconfirmedUser);
            }
            return unconfirmedUser;
        }

        public async Task<User> ResetPassword(string userId, string hashedPassword)
        {
            var user = await FindUserById(userId);
            if (user != null)
            {
                user.Password = hashedPassword;
                user.Activated = true;
                await Update(userId, user);
            }
            return user;
        }

    }
}
