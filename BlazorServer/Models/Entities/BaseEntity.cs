using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BlazorServerAPI.Models.Entities
{
    //TODO: find a way to add mandatory created_at and updated_at to controll data integrity
    public class BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
