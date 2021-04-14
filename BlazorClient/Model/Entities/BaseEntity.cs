using System;

namespace BlazorClient.Model.Entities
{
    public class BaseEntity : IBaseEntity
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
