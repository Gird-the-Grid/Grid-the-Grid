using BlazorServerAPI.Models.Entities;
using BlazorServerAPI.Models.Responses;
using BlazorServerAPI.Repository;
using BlazorServerAPI.Utils.Factories;
using System.Threading.Tasks;

namespace BlazorServerAPI.Handlers
{
    public class ControlPanelGridHandler : BaseResourceHandler<GridModel>
    {
        private readonly GridRepository _gridRepository;

        public ControlPanelGridHandler(GridRepository gridRepository) : base(gridRepository)
        {  }

        public async Task<IResponse> CreateGridParameters(GridTemplate gridTemplate)
        {
            var newGrid = (GridModel)(new GridFactory(gridTemplate.Vertexes, gridTemplate.Edges)).Create(); //TODO: see if there's a way to return a GridModel and not convert
            newGrid.OwnerId = gridTemplate.OwnerId;
            return await base.CreateResource(newGrid);
        }

        public async Task<IResponse> UpdateGridParameters(GridTemplate gridTemplate)
        {
            var newGrid = (GridModel)(new GridFactory(gridTemplate.Vertexes, gridTemplate.Edges)).Create();
            newGrid.Id = gridTemplate.Id;
            newGrid.OwnerId = gridTemplate.OwnerId;
            return await base.UpdateResource(newGrid);
        }
    }
}
