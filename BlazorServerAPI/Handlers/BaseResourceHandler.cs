using BlazorServerAPI.Models.Entities;
using BlazorServerAPI.Models.Responses;
using BlazorServerAPI.Repository;
using System.Threading.Tasks;
using BlazorServerAPI.Utils;

namespace BlazorServerAPI.Handlers
{
    public class BaseResourceHandler<T> : IBaseResourceHandler<T> where T : OwnedEntity
    {
        protected readonly OwnedResourceRepository<T> _resourceRepository;

        public BaseResourceHandler(OwnedResourceRepository<T> resourceRepository)
        {
            _resourceRepository = resourceRepository;
        }

        public virtual async Task<IResponse> CreateResource(T ownedEntity)
        {
            _ = await _resourceRepository.CreateObject(ownedEntity);
            return new MessageResponse(Text.SettingsUpdated(ownedEntity.GetType()));
        }

        public async Task<IResponse> UpdateResource(T ownedEntity)
        {
            var updatedEntity = await _resourceRepository.UpdateObject(ownedEntity.Id, ownedEntity);
            if (updatedEntity == null)
            {
                return new ErrorResponse(error: Text.IllegalModification);
            }
            return new MessageResponse(Text.SettingsUpdated(updatedEntity.GetType()));
        }

        public async Task<IResponse> GetResource(string userId)
        {
            var resource = await _resourceRepository.GetObject(userId);
            if (resource == null)
            {
                return new ErrorResponse(error: Text.NoConfiguration(typeof(T)));
            }
            return new MessageResponse(resource.ToString());
        }

        public async Task<IResponse> DeleteResource(string userId)
        {
            var resource = await _resourceRepository.DeleteObject(userId);
            if (resource == null)
            {
                return new ErrorResponse(error: Text.NoConfiguration(typeof(T)));
            }
            return new MessageResponse(Text.Deleted);
        }
    }
}
