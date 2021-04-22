using BlazorServerAPI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServerAPI.Utils.Factories
{
    public interface IBaseEntityFactory<out T> where T : IBaseEntity
    {
        T Create();
    }
}
