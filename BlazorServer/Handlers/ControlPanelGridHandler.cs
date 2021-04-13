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
            throw new NotImplementedException();
        }

        public async Task<IResponse> UpdateGridParameters(GridModel gridParameters)
        {
            throw new NotImplementedException();
        }

        public async Task<IResponse> GetGridParameters(string gridId)
        {
            throw new NotImplementedException();
        }
    }
}
