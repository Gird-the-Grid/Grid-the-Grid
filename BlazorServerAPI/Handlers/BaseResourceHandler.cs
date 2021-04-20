using BlazorServerAPI.Models.Entities;
using BlazorServerAPI.Models.Responses;
using BlazorServerAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServerAPI.Handlers
{
    public class BaseResourceHandler<T> : IBaseResourceHandler<T> where T : OwnedEntity
    {
        private readonly OwnedResourceRepository<T> _resourceRepository;
        public BaseResourceHandler(OwnedResourceRepository<T> resourceRepository)
        {
            _resourceRepository = resourceRepository;
        }

        public virtual async Task<IResponse> CreateResource(T ownedEntity)
        {
            _ = await _resourceRepository.CreateObject(ownedEntity);
            return new MessageResponse($"{ownedEntity.GetType()} settings updated");
        }

        public async Task<IResponse> UpdateResource(T ownedEntity)
        {
            var updatedEntity = await _resourceRepository.UpdateObject(ownedEntity.Id, ownedEntity);
            if (updatedEntity == null)
            {
                return new ErrorResponse(error: "Invalid company id or illegal company modification");
            }
            return new MessageResponse($"{updatedEntity.GetType()} settings updated");
        }

        public async Task<IResponse> GetResource(string userId)
        {
            var resource = await _resourceRepository.GetObject(userId);
            if (resource == null)
            {
                return new ErrorResponse(error: $"{resource.GetType()} has no configuration");
            }
            return new MessageResponse(resource.ToString());
        } 
    }
}
