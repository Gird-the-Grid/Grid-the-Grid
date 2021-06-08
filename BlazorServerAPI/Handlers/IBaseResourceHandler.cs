using BlazorServerAPI.Models.Responses;
using System.Threading.Tasks;

namespace BlazorServerAPI.Handlers
{
    public interface IBaseResourceHandler<in T>
    {
        Task<IResponse> CreateResource(T ownedEntity);
        Task<IResponse> UpdateResource(T ownedEntity);
        Task<IResponse> GetResource(string userId);
    }
}
