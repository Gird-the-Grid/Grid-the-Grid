using System;

namespace BlazorClient.Model.Entities
{
    public interface IBaseEntity
    {
        string Id { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime UpdatedAt { get; set; }
    }
}
