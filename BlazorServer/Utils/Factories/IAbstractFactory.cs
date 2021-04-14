using BlazorServerAPI.Models.Entities;

namespace BlazorServerAPI.Utils.Factories
{
    public interface IAbstractFactory
    {
        IBaseEntity Create();
    }
}
