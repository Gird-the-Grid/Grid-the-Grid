using MongoDB.Bson;

namespace BlazorServerAPI.Models.Entities
{
    public interface IBaseEntity
    {
        string Id { get; set; }
        BsonDateTime CreatedAt { get; set; }
        BsonDateTime UpdatedAt { get; set; }

    }
}
