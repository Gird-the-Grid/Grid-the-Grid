using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServerAPI.Models.Entities
{
    public class GridTemplate : BaseEntity
    {
        public GridTemplate() : base()
        { }

        public GridTemplate(int vertexes, int edges)
        {
            Vertexes = vertexes;
            Edges = edges;
        }

        public int Vertexes { get; set; }
        public int Edges { get; set; }
    }
}
