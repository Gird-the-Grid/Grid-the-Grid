using BlazorServerAPI.Models.Responses;
using BlazorServerAPI.Repository;
using Newtonsoft.Json;
using QuickGraph;
using System;
using System.Collections.Generic;
using BlazorServerAPI.Utils.Extensions;
using System.Threading.Tasks;

namespace BlazorServerAPI.Handlers
{
    public class GridManagerHandler
    {

        private readonly GridRepository _gridRepository;
        private readonly Random _rand;

        public GridManagerHandler(GridRepository gridRepository)
        {
            _gridRepository = gridRepository;
            _rand = new Random();
        }

        public async Task<IResponse> GetCurrentWeights(string userId)
        {
            var grid = await _gridRepository.GetObject(userId);
            if (grid == null)
            {
                return new ErrorResponse(error: "grid has no configuration");
            }
            var edgeCost = JsonConvert.DeserializeObject<Dictionary<string, double>>(grid.EdgeCost);
            //TODO: here we sould actually have Dictionary<Edge<string>, double> but json converter cannot convert "1->3" to Edge<string> which should be new Edge<string>("1", "3")
            // either do it manually or Create a TypeConverter to convert from the string to the key type object.
            var currentEdgeCost = new Dictionary<string, double>();
            foreach(var edge in edgeCost.Keys)
            {
                var value = _rand.NextDouble(0, edgeCost[edge]);
                currentEdgeCost.Add(edge, value);
            }
            return new MessageResponse(JsonConvert.SerializeObject(currentEdgeCost));
        }

    }
}
