using BlazorServerAPI.Models.Entities;
using BlazorServerAPI.Models.Responses;
using BlazorServerAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var newCompanyConfiguration = new CompanyModel(company.CompanyName, company.CompanyIdentificationNumber, company.CompanyName, company.TaxRates);
            newCompanyConfiguration.OwnerId = company.OwnerId;
            var result = await _companyRepository.CreateCompany(newCompanyConfiguration);
            return new MessageResponse("Company settings updated");
        }
        
        public async Task<IResponse> UpdateCompanyConfiguration(CompanyModel companyConfiguration)
        {
            var updatedCompany = await _companyRepository.UpdateCompany(companyConfiguration.Id, companyConfiguration);
            if (updatedCompany == null)
            {
                return new ErrorResponse(error: "Invalid company id or illegal company modification");
            }
            return new MessageResponse("Company settings updated");
        }

        public async Task<IResponse> GetCompanyConfiguration(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
