using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Horrografia.Client.Shared.Objects.ClientModels;
using Horrografia.Shared;
using Horrografia.Shared.Models;

namespace Horrografia.Server.Data.Repos.Interfaces
{
    public interface IRolRepository
    {
        Task<List<RolModel>> GetAllAsync();
        Task<List<UserRolesModel>> GetAllUserRoles();
    }
}
