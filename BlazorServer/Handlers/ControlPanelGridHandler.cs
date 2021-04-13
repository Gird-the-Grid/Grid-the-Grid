using BlazorServerAPI.Models.Entities;
using BlazorServerAPI.Models.Responses;
using BlazorServerAPI.Repository;
using System;
using System.Threading.Tasks;

namespace BlazorServerAPI.Handlers
{
    public class ControlPanelGridHandler
    {
        private readonly GridRepository _gridRepository;

        public ControlPanelGridHandler(GridRepository gridRepository)
        {
            _gridRepository = gridRepository;
        }
        public async Task<IResponse> CreateGridParameters(GridModel gridParameters)
        {
            var newGrid = new GridModel();
            newGrid.OwnerId = gridParameters.OwnerId; //TODO add a new constructor with owner id
            var result = await _gridRepository.CreateGrid(newGrid);
            return new MessageResponse("Company settings updated");
        }

        public async Task<IResponse> UpdateGridParameters(GridModel gridParameters)
        {
            var updatedGrid = await _gridRepository.UpdateGrid(gridParameters.Id, gridParameters);
            if (updatedGrid == null)
            {
                return new ErrorResponse(error: "Invalid company id or illegal company modification");
            }
            return new MessageResponse("Company settings updated");
        }

        public async Task<IResponse> GetGridParameters(string gridId)
        {
            var gridParameters = await _gridRepository.GetGrid(gridId);
            if (gridParameters == null)
            {
                return new ErrorResponse(error: "company has no configuration");
            }
            return new MessageResponse(gridParameters.ToString());
        }
    }
}
