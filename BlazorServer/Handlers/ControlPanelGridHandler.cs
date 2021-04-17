using BlazorServerAPI.Models.Entities;
using BlazorServerAPI.Models.Responses;
using BlazorServerAPI.Repository;
using BlazorServerAPI.Utils.Factories;
using System.Threading.Tasks;

namespace BlazorServerAPI.Handlers
{
    public class ControlPanelGridHandler
    {
        private readonly GridRepository _gridRepository;

        public ControlPanelGridHandler(GridRepository gridRepository)
        {
            //TODO: This and ControlPanelCompanyHandler are similar, to verify if possible to merge
            _gridRepository = gridRepository;
        }
        public async Task<IResponse> CreateGridParameters(GridTemplate gridTemplate)
        {
            var newGrid = (GridModel)(new GridFactory(gridTemplate.Vertexes, gridTemplate.Edges)).Create(); //TODO: see if there's a way to return a GridModel and not convert
            newGrid.OwnerId = gridTemplate.OwnerId;
            _ = await _gridRepository.CreateObject(newGrid);
            return new MessageResponse("Grid parameters updated");
        }

        public async Task<IResponse> UpdateGridParameters(GridTemplate gridTemplate)
        {
            var newGrid = (GridModel)(new GridFactory(gridTemplate.Vertexes, gridTemplate.Edges)).Create();
            newGrid.Id = gridTemplate.Id;
            newGrid.OwnerId = gridTemplate.OwnerId;
            var updatedGrid = await _gridRepository.UpdateObject(gridTemplate.Id, newGrid);
            if (updatedGrid == null)
            {
                return new ErrorResponse(error: "Invalid grid id or illegal grid modification");
            }
            return new MessageResponse("Grid parameters updated");
        }

        public async Task<IResponse> GetGridParameters(string gridId)
        {
            var gridParameters = await _gridRepository.GetObject(gridId);
            if (gridParameters == null)
            {
                return new ErrorResponse(error: "grid has no configuration");
            }
            return new MessageResponse(gridParameters.ToString());
        }
    }
}
