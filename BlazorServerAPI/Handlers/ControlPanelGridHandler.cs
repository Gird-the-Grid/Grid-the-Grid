using BlazorServerAPI.Models.Entities;
using BlazorServerAPI.Models.Responses;
using BlazorServerAPI.Repository;
using BlazorServerAPI.Utils.Factories;
using System.Threading.Tasks;

namespace BlazorServerAPI.Handlers
{
    public class ControlPanelGridHandler : BaseResourceHandler<GridModel>
    {
        public ControlPanelGridHandler(GridRepository gridRepository) : base(gridRepository)
        {  }

        public async Task<IResponse> CreateGridParameters(GridTemplate gridTemplate)
        {
            var newGrid = new GridFactory(gridTemplate.Vertexes, gridTemplate.Edges).Create();
            newGrid.OwnerId = gridTemplate.OwnerId;
            return await base.CreateResource(newGrid);
        }

        public async Task<IResponse> UpdateGridParameters(GridTemplate gridTemplate)
        {
            var newGrid = new GridFactory(gridTemplate.Vertexes, gridTemplate.Edges).Create();
            newGrid.Id = gridTemplate.Id;
            newGrid.OwnerId = gridTemplate.OwnerId;
            return await UpdateResource(newGrid);
        }

        public async Task<string> GetGridDotString(string userId)
        {
            var grid = await _resourceRepository.GetObject(userId);
            return grid.Dot;
        }
    }
}
