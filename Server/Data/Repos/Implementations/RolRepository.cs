using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Horrografia.Client.Shared.Objects.ClientModels;
using Horrografia.Shared;
using Horrografia.Shared.Models;
using Horrografia.Server.Data.Repos.Interfaces;
using Horrografia.Server.Models;
using DataLibrary;
using Microsoft.Extensions.Configuration;
using Horrografia.Server.Extensions;

namespace Horrografia.Server.Data.Repos.Implementations
{
    public class RolRepository : IRolRepository
    {
        private readonly IDataAccess _dbContext;
        private readonly string ConectionString;

        public RolRepository(IDataAccess dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            ConectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<RolModel>> GetAllAsync()
        {
            string sql = "SELECT * FROM aspnetroles";
            var roles = await _dbContext.LoadData<RolModel, dynamic>(sql, new { }, ConectionString);
            return roles;
        }

        public async Task<List<UserRolesModel>> GetAllUserRoles()
        {
            string sql = "SELECT * FROM aspnetuserroles";
            var userRoles = await _dbContext.LoadData<UserRolesModel, dynamic>(sql, new { }, ConectionString);
            return userRoles;
        }
    }
}
