using BlazorServerAPI.Models.Entities;
using BlazorServerAPI.Settings;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson.Serialization;

namespace BlazorServerAPI.Repository
{
    public class GridRepository : BaseRepository<GridModel>
    {
        //TODO: these methods are similar to CompanyRepository: see if they can be implemented only once
        public GridRepository(IMongoDbSettings settings) : base(settings, "grids")
        { }

        public async Task<GridModel> CreateGrid(GridModel grid)
        {
            grid.Id = null;
            await _documents.InsertOneAsync(grid);
            return grid;
        }

        public async Task<GridModel> UpdateGrid(string gridId, GridModel grid)
        {
            var oldGrid = (await _documents.FindAsync(document => document.Id == gridId && document.OwnerId == grid.OwnerId)).SingleOrDefault();
            if (oldGrid != null)
            {
                grid.CreatedAt = oldGrid.CreatedAt;
                await Update(gridId, grid);
                return grid;
            }
            return oldGrid;
        }

        public async Task<GridModel> GetGrid(string userId)
        {
            return (await _documents.FindAsync(document => document.OwnerId == userId)).SingleOrDefault();
        }

    }
}
