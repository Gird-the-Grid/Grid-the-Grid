using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServerAPI.Models.Entities
{
    //TODO: validate this (edges must be between 1 and Vertexes * (vertexes -1) / 4, Vertexes must be between 2 and 1.9 kk 
    public class GridTemplate : BaseEntity
    {
        public GridTemplate() : base()
        { }

        public GridTemplate(int vertexes, int edges)
        {
            Vertexes = vertexes;
            Edges = edges;
        }

        public GridTemplate(int vertexes, int edges, string ownerId) : this(vertexes, edges)
        {
            OwnerId = ownerId;
        }
        [Required]
        public int Vertexes { get; set; }
        [Required]
        public int Edges { get; set; }
        public string OwnerId { get; set; }
    }
}
