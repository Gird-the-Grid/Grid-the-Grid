using System;

namespace BlazorServerAPI.Models.Entities
{
    public interface IBaseEntity
    {
        string Id { get; set; }

        DateTime CreatedAt { get; set; }
        DateTime UpdatedAt { get; set; }

    }
}
