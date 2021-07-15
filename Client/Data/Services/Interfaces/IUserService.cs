using Horrografia.Client.Shared.Objects;
using Horrografia.Shared.Models;
using System.Threading.Tasks;
using Horrografia.Shared;
using Horrografia.Client.Shared.Objects.ClientModels;

namespace Horrografia.Client.Data.Services.Interfaces
{
    public interface IUserService
    {
        Task<ControllerResponse<bool>> CerrarSesion();
        Task<ControllerResponse<SharedConstants.LoginState>> Login(ClientUserLoginDTO modelo);
        Task<ControllerResponse<bool>> Register(ClientUserRegisterDTO modelo);
        Task<ControllerResponse<bool>> VerifyEmail(ClientUserRegisterDTO modelo);
    }
}
