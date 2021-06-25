using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Horrografia.Client.Shared.Objects.ClientModels;
using Horrografia.Shared;

namespace Horrografia.Server.Data.Repos.Interfaces
{
    public interface IUsuariosRepository
    {
        Task<bool> CrearUsuario(ClientUserRegisterDTO usuarioACrear);
        Task<SharedConstants.LoginState> IniciarSesion(ClientUserLoginDTO modeloEnviado);
        Task<bool> CorreoDisponible(string correo);
        Task CerrarSesion();
    }
}
