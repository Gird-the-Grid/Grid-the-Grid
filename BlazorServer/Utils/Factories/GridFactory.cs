using BlazorServerAPI.Models.Entities;
using QuickGraph;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using static System.Linq.Enumerable; //C# 6.0

namespace BlazorServerAPI.Utils.Factories
{
    public class GridFactory : IAbstractFactory
    {
        private readonly int _vertexes;
        private readonly int _edges;
        private readonly Random _rand;

        public GridFactory(int vertexes, int edges)
        {
            _vertexes = vertexes;
            _edges = edges;
            _rand = new Random();
            Debug.Assert(_vertexes > 0 && _edges > 0, "vertexes and edges must be bigger than 0");
            Debug.Assert(_vertexes < 1900000, "too many vertexes");
            Debug.Assert(_edges <= _vertexes * (_vertexes - 1) / 4, "Too many edges");
        }

        public IBaseEntity Create()
        {
            AdjacencyGraph<string, Edge<string>> graph;
            Dictionary<Edge<string>, double> edgeCost;
            (graph, edgeCost) = createGraph();
            return new GridModel(graph: graph, edgeCost: edgeCost);
        }

        private void addVertexesToGraph(AdjacencyGraph<string, Edge<string>> graph)
        {
            foreach (var i in Range(0, _vertexes))
            {
                graph.AddVertex(i.ToString());
            }
        }

        private void addEdgesToGraph(AdjacencyGraph<string, Edge<string>> graph, Dictionary<Edge<string>, double> edgeCost)
        {
            var possibleEdges = new List<Edge<string>>();
            for (var vertex1 = 0; vertex1 < _vertexes - 1; ++vertex1)
            {
                for (var vertex2 = vertex1 + 1; vertex2 < _vertexes; ++vertex2)
                {
                    possibleEdges.Add(new Edge<string>(vertex1.ToString(), vertex2.ToString()));
                }
            }
            foreach (var _ in Range(0, _edges))
            {
                var edgeIndex = _rand.Next(0, possibleEdges.Count);
                var edge = possibleEdges[edgeIndex];
                possibleEdges.RemoveAt(edgeIndex);
                graph.AddEdge(edge);
                edgeCost.Add(edge, _rand.NextDouble() * 99 + 1);
            }
        }

        private (AdjacencyGraph<string, Edge<string>>, Dictionary<Edge<string>, double>) createGraph()
        {
            var graph = new AdjacencyGraph<string, Edge<string>>(false);
            addVertexesToGraph(graph);
            var edgeCost = new Dictionary<Edge<string>, double>(_edges);
            addEdgesToGraph(graph, edgeCost);
            return (graph, edgeCost);
        }
    }
}
