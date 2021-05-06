using BlazorServerAPI.Models.Entities;

namespace BlazorServerAPI.Utils.Factories
{
    public interface IBaseEntityFactory<out T> where T : IBaseEntity
    {
        T Create();
    }
}
