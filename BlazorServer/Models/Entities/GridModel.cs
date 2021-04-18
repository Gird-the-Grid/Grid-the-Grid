using System.Collections.Generic;
using Newtonsoft.Json;
using QuickGraph;

namespace BlazorServerAPI.Models.Entities
{
    //TODO: add atributes needed
    public class GridModel : OwnedEntity
    {

        public string Graph { get; set; }

        public string EdgeCost { get; set; }
        
        public GridModel() : base()
        { }

        public GridModel(AdjacencyGraph<string, Edge<string>> graph, Dictionary<Edge<string>, double> edgeCost, string ownerId) : this(graph, edgeCost)
        {
            OwnerId = ownerId;
        }
        //TODO: Add [BsonConstructor] to constructors in all entities
        public GridModel(AdjacencyGraph<string, Edge<string>> graph, Dictionary<Edge<string>, double> edgeCost) : base()
        {
            Graph = JsonConvert.SerializeObject(graph);
            EdgeCost = JsonConvert.SerializeObject(edgeCost);
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

    }

}
