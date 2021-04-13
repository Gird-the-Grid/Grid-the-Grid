using BlazorServerAPI.Models.Entities;
using BlazorServerAPI.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServerAPI.Handlers
{
    public class ControlPanelHandler
    {
        public ControlPanelHandler()
        {
            //TODO: Add CompanyRepository and GridRepository
        }
        public async Task<IResponse> CreateCompanyConfiguration(CompanyModel companyConfiguration)
        {
            throw new NotImplementedException();
        }
        
        public async Task<IResponse> UpdateCompanyConfiguration(CompanyModel companyConfiguration)
        {
            throw new NotImplementedException();
        }

        public async Task<IResponse> GetCompanyConfiguration(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
