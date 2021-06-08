using System.Collections.Generic;
using Newtonsoft.Json;
using QuickGraph;
using QuickGraph.Graphviz;

namespace BlazorServerAPI.Models.Entities
{
    //TODO: add atributes needed
    public class GridModel : OwnedEntity
    {

        public string Graph { get; set; }

        public string EdgeCost { get; set; }

        public string Dot { get; set; }
        
        public GridModel() : base()
        { }

        public GridModel(IEdgeListGraph<string, Edge<string>> graph, Dictionary<Edge<string>, double> edgeCost, string ownerId) : this(graph, edgeCost)
        {
            OwnerId = ownerId;
        }
        //TODO: Add [BsonConstructor] to constructors in all entities
        public GridModel(IEdgeListGraph<string, Edge<string>> graph, Dictionary<Edge<string>, double> edgeCost) : base()
        {
            var graphviz = new GraphvizAlgorithm<string, Edge<string>>(graph);
            Dot = graphviz.Generate();
            Graph = JsonConvert.SerializeObject(graph);
            EdgeCost = JsonConvert.SerializeObject(edgeCost);
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

    }

}
