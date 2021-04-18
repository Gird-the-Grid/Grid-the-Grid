namespace BlazorServerAPI.Models.Entities
{
    public interface IOwnedResource
    {
        string OwnerId { get; set; }
    }
}
