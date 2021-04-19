using BlazorServerAPI.Models.Entities;
using BlazorServerAPI.Models.Responses;
using BlazorServerAPI.Repository;
using System.Threading.Tasks;

namespace BlazorServerAPI.Handlers
{
    public class ControlPanelCompanyHandler
    {
        private readonly CompanyRepository _companyRepository;

        public ControlPanelCompanyHandler(CompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }
        public async Task<IResponse> CreateCompanyConfiguration(CompanyModel company)
        {
            var newCompanyConfiguration = new CompanyModel(company.CompanyName, company.CompanyIdentificationNumber, company.Country, company.TaxRates);
            newCompanyConfiguration.OwnerId = company.OwnerId; //TODO add a new constructor with owner id
            _ = await _companyRepository.CreateObject(newCompanyConfiguration);
            return new MessageResponse("Company settings updated");
        }
        
        public async Task<IResponse> UpdateCompanyConfiguration(CompanyModel company)
        {
            var updatedCompany = await _companyRepository.UpdateObject(company.Id, company);
            if (updatedCompany == null)
            {
                return new ErrorResponse(error: "Invalid company id or illegal company modification");
            }
            return new MessageResponse("Company settings updated");
        }

        public async Task<IResponse> GetCompanyConfiguration(string userId)
        {
            var companyConfiguration = await _companyRepository.GetObject(userId);
            if (companyConfiguration == null)
            {
                return new ErrorResponse(error: "company has no configuration");
            }
            return new MessageResponse(companyConfiguration.ToString());
        }
    }
}
