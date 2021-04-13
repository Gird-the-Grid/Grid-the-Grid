using BlazorServerAPI.Models.Entities;
using BlazorServerAPI.Settings;

namespace BlazorServerAPI.Repository
{
    public class GridRepository : BaseRepository<GridModel>
    {
        public GridRepository(IMongoDbSettings settings) : base(settings, "grids")
        { }


    }
}
