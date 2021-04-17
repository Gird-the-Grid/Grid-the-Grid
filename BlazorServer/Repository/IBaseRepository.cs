
using MongoDB.Driver;
using BlazorServerAPI.Models.Entities;

namespace BlazorServerAPI.Repository
{
    interface IBaseRepository<T> where T : BaseEntity
    {
        IMongoCollection<T> _documents { get; }
    }
}