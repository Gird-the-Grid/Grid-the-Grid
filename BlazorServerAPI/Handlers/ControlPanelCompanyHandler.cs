using BlazorServerAPI.Models.Entities;
using BlazorServerAPI.Models.Responses;
using BlazorServerAPI.Repository;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace BlazorServerAPI.Handlers
{
    public class ControlPanelCompanyHandler : BaseResourceHandler<CompanyModel>
    {

        public ControlPanelCompanyHandler(CompanyRepository companyRepository) : base(companyRepository)
        { }

        public override async Task<IResponse> CreateResource(CompanyModel company)
        {
            var newCompanyConfiguration = new CompanyModel(company.CompanyName, company.CompanyIdentificationNumber, company.Country, company.TaxRates);
            newCompanyConfiguration.OwnerId = company.OwnerId;
            return await base.CreateResource(newCompanyConfiguration);
        }
    }
}
