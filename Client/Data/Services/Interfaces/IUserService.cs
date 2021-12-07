using Horrografia.Client.Shared.Objects;
using Horrografia.Shared.Models;
using System.Threading.Tasks;
using Horrografia.Shared;
using Horrografia.Client.Shared.Objects.ClientModels;

namespace Horrografia.Client.Data.Services.Interfaces
{
    public interface IUserService
    {
        Task<ControllerResponse<UsuarioDTO>> GetAsync();
        Task<ControllerResponse<UsuarioDTO>> GetUserById(string id);
        Task<ControllerResponse<bool>> CerrarSesion();
        Task<ControllerResponse<SharedConstants.LoginState>> Login(ClientUserLoginDTO modelo);
        Task<ControllerResponse<bool>> Register(ClientUserRegisterDTO modelo);
        Task<ControllerResponse<bool>> VerifyEmail(ClientUserRegisterDTO modelo);
        Task<ControllerResponse<UsuarioDTO>> GiveAdministrationRole(UsuarioDTO user);
        Task<ControllerResponse<UsuarioDTO>> GiveProfessorRole(UsuarioDTO user);
        Task<ControllerResponse<UsuarioDTO>> RemoveAdministrationRole(UsuarioDTO user);
        Task<ControllerResponse<UsuarioDTO>> RemoveProfessorRole(UsuarioDTO user);
        Task<ControllerResponse<UsuarioDTO>> UpdateUser(UsuarioDTO user);
        Task<ControllerResponse<string>> GetSchoolStudents(string codigo);
        Task<ControllerResponse<string>> GetSchoolProfessors(string codigo);
        Task<ControllerResponse<UsuarioDTO>> GetSchoolUsers(string codigo);
    }
}
