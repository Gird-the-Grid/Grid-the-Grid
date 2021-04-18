namespace BlazorServerAPI.Models.Entities
{
    public class OwnedEntity : BaseEntity, IOwnedResource
    {
        public OwnedEntity() : base()
        { }


        public OwnedEntity(string ownerId) : base()
        {
            OwnerId = ownerId;
        }

        public string OwnerId { get; set; }
    }
}
