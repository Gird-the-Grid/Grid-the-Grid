
using MongoDB.Driver;
using BlazorServerAPI.Models.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BlazorServerAPI.Repository
{
    interface IBaseRepository<T> where T : IBaseEntity
    {
        IMongoCollection<T> _documents { get; }

        Task<List<T>> Get();

        Task<T> Get(string id);

        Task Update(string id, T documentIn);

        Task Remove(string id);

    }
}