using BlazorServerAPI.Models.Entities;
using BlazorServerAPI.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServerAPI.Repository
{
    public class CompanyRepository : BaseRepository<CompanyModel>
    {
        public CompanyRepository(IMongoDbSettings settings) : base(settings, "companies")
        { }

        public async Task<CompanyModel> CreateCompany(CompanyModel company)
        {
            company.Id = null;
            //TODO: add unique key on ownerId
            await _documents.InsertOneAsync(company);
            return company;
        }

        public async Task<CompanyModel> UpdateCompany(string companyId, CompanyModel company)
        {
            throw new NotImplementedException();
        }
    }
}
