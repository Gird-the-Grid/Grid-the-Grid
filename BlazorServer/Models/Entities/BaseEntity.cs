using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace BlazorServerAPI.Models.Entities
{
    //TODO: find a way to add mandatory created_at and updated_at to controll data integrity
    public class BaseEntity : IBaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public BsonDateTime CreatedAt { get; set; }
        public BsonDateTime UpdatedAt { get; set; }

        public BaseEntity()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
    }
}
