using BlazorServerAPI.Models.Entities;
using BlazorServerAPI.Settings;

namespace BlazorServerAPI.Repository
{
    public class GridRepository : BaseRepository<GridModel>
    {
        //TODO: these methods are similar to CompanyRepository: see if they can be implemented only once
        public GridRepository(IMongoDbSettings settings) : base(settings, "grids")
        { }
    }
}
