using BlazorServerAPI.Models.Entities;
using BlazorServerAPI.Settings;

namespace BlazorServerAPI.Repository
{
    public class CompanyRepository : BaseRepository<CompanyModel>
    {
        public CompanyRepository(IMongoDbSettings settings) : base(settings, "companies")
        { }
    }
}
