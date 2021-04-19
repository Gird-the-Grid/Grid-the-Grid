using System.Threading.Tasks;
using MongoDB.Driver;
using System.Linq;
using BlazorServerAPI.Settings;
using BlazorServerAPI.Models.Entities;

namespace BlazorServerAPI.Repository
{
    public class OwnedResourceRepository<T>  : BaseRepository<OwnedEntity> where T : OwnedEntity
    {
        public OwnedResourceRepository(IMongoDbSettings settings, string collection) : base(settings, collection)
        { }

        public async Task<T> CreateObject(T obj)
        {
            obj.Id = null;
            await _documents.InsertOneAsync(obj);
            return obj;
        }

        public async Task<T> UpdateObject(string objectId, T obj)
        {
            //TODO: initial code: var oldobj = (await _documents.FindAsync(document => document.Id == objectId && document.OwnerId == obj.OwnerId)).SingleOrDefault();
            //TODO: try to see why this above doesn't work and why `Builders<OwnedEntity>` and not `Builders<T>`
            var filter = Builders<OwnedEntity>.Filter.Where(doc => doc.Id == objectId && doc.OwnerId == obj.OwnerId);
            var oldObj = (await _documents.FindAsync<T>(filter)).SingleOrDefault();
            if (oldObj != null)
            {
                obj.CreatedAt = oldObj.CreatedAt;
                await Update(objectId, obj);
                return obj;
            }
            return oldObj;
        }

        public async Task<T> GetObject(string userId)
        {
            //TODO: same as above
            var filter = Builders<OwnedEntity>.Filter.Where(doc => doc.OwnerId == userId);
            return (await _documents.FindAsync<T>(filter)).SingleOrDefault();
        }
    }
}
