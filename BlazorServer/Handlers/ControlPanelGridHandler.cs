using BlazorServerAPI.Models.Entities;
using BlazorServerAPI.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServerAPI.Handlers
{
    public class ControlPanelGridHandler
    {
        public ControlPanelGridHandler()
        {
            //TODO: Add GridRepository
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
