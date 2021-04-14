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
            var grid = await _gridRepository.GetGrid(userId);
            if (grid == null)
            {
                return new ErrorResponse(error: "grid has no configuration");
            }
            var edgeCost = JsonConvert.DeserializeObject<Dictionary<string, double>>(grid.EdgeCost);
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
