using BlazorServerAPI.Models.Entities;
using BlazorServerAPI.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServerAPI.Repository
{
    public class GridRepository : BaseRepository<GridModel>
    {
        public GridRepository(IMongoDbSettings settings) : base(settings, "grids")
        { }


    }
}
