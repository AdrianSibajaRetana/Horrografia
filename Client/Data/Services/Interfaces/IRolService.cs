using Horrografia.Client.Shared.Objects;
using Horrografia.Shared.Models;
using System.Threading.Tasks;
using Horrografia.Shared;
using Horrografia.Client.Shared.Objects.ClientModels;

namespace Horrografia.Client.Data.Services.Interfaces
{
    public interface IRolService
    {
        Task<ControllerResponse<RolModel>> GetRoles();
        Task<ControllerResponse<UserRolesModel>> GetUsersandRolesRelation();
    }
}
