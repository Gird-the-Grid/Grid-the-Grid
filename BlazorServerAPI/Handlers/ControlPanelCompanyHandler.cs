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

        public override async Task<IResponse> CreateResource(CompanyModel ownedEntity)
        {
            var newCompanyConfiguration = new CompanyModel(ownedEntity.CompanyName, ownedEntity.CompanyIdentificationNumber, ownedEntity.Country, ownedEntity.TaxRates);
            newCompanyConfiguration.OwnerId = ownedEntity.OwnerId;
            return await base.CreateResource(newCompanyConfiguration);
        }
    }
}
