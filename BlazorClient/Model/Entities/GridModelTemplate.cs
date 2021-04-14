using System.ComponentModel.DataAnnotations;

namespace BlazorClient.Model.Entities
{
    public class GridModelTemplate : BaseEntity
    {
        [Required]
        public int Vertexes { get; set; }

        [Required]
        public int Edges { get; set; }
        public string OwnerId { get; set; }
    }
}
