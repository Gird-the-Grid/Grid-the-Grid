using BlazorServerAPI.Models.Entities;
using BlazorServerAPI.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServerAPI.Handlers
{
    public interface IBaseResourceHandler<T>
    {
        Task<IResponse> CreateResource(T ownedEntity);
        Task<IResponse> UpdateResource(T ownedEntity);
        Task<IResponse> GetResource(string userId);
    }
}
