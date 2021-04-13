using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using QuickGraph;

namespace BlazorServerAPI.Models.Entities
{
    //TODO: add atributes needed
    public class GridModel : BaseEntity//, IValidatableObject
    {

        public AdjacencyGraph<string, Edge<string>> Graph { get; set; }

        public Dictionary<Edge<string>, double> EdgeCost { get; set; }

        public string OwnerId { get; set; }

        public GridModel() : base()
        { }

        public GridModel(AdjacencyGraph<string, Edge<string>> graph, Dictionary<Edge<string>, double> edgeCost, string ownerId) : this(graph, edgeCost)
        {
            OwnerId = ownerId;
        }

        public GridModel(AdjacencyGraph<string, Edge<string>> graph, Dictionary<Edge<string>, double> edgeCost) : base()
        {
            Graph = graph;
            EdgeCost = edgeCost;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }


        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    //TODO: add custom validation, validate everything (edges with foreign nodes, cicles, ...)
        //}
    }

}
