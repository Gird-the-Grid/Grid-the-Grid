using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorClient.Model.Entities
{
    public class GridModelTemplate : BaseEntity
    {
        [Range(2, 2000000)]
        [Required]
        public int Vertexes { get; set; }

        //[Range(0, _Vertexes * (_Vertexes - 1) / 4)] // TODO:Rezolvare eroare CS0120
        [Required]
        public int Edges { get; set; }
        public string OwnerId { get; set; }
    }
}
