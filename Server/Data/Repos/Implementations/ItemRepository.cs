using System.Collections.Generic;
using System.Threading.Tasks;
using Horrografia.Shared.Models;
using DataLibrary;
using Microsoft.Extensions.Configuration;
using Horrografia.Server.Data.Repos.Interfaces;

namespace Horrografia.Server.Data.Repos.Implementations
{
    public class ItemRepository
    {
        private readonly IDataAccess _dbContext;
        private readonly string ConectionString;

        public ItemRepository(IDataAccess dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            ConectionString = configuration.GetConnectionString("DefaultConnection");
        }
    }
}
