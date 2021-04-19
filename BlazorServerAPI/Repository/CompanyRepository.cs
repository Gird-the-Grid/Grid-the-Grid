using BlazorServerAPI.Models.Entities;
using BlazorServerAPI.Settings;

namespace BlazorServerAPI.Repository
{
    public class CompanyRepository : OwnedResourceRepository<CompanyModel>
    {
        public CompanyRepository(IMongoDbSettings settings) : base(settings, "companies")
        { }
    }
}
