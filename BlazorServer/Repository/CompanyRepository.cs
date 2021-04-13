using BlazorServerAPI.Models.Entities;
using BlazorServerAPI.Settings;
using System.Linq;
using MongoDB.Driver;
using System.Threading.Tasks;
using System;

namespace BlazorServerAPI.Repository
{
    public class CompanyRepository : BaseRepository<CompanyModel>
    {
        public CompanyRepository(IMongoDbSettings settings) : base(settings, "companies")
        { }

        public async Task<CompanyModel> CreateCompany(CompanyModel company)
        {
            company.Id = null;
            await _documents.InsertOneAsync(company);
            return company;
        }

        public async Task<CompanyModel> UpdateCompany(string companyId, CompanyModel company)
        {
            var oldCompany = (await _documents.FindAsync<CompanyModel>(document => document.Id == companyId && document.OwnerId == company.OwnerId)).SingleOrDefault();
            if (oldCompany != null)
            {
                company.CreatedAt = oldCompany.CreatedAt;
                await Update(companyId, company);
                return company;
            }
            return oldCompany;
        }

        public async Task<CompanyModel> GetCompany(string userId)
        {
            return (await _documents.FindAsync<CompanyModel>(document => document.OwnerId == userId)).SingleOrDefault();
        }
    }
}
