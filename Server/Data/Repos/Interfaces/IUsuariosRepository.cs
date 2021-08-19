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
    public interface IUsuariosRepository
    {
        Task<List<UsuarioDTO>> GetAllAsync();
        Task<bool> CrearUsuario(ClientUserRegisterDTO usuarioACrear);
        Task<SharedConstants.LoginState> IniciarSesion(ClientUserLoginDTO modeloEnviado);
        Task<bool> CorreoDisponible(string correo);
        Task CerrarSesion();
        Task OtorgarPermisoAdministrador(UsuarioDTO usuario);
        Task OtorgarPermisoProfesor(UsuarioDTO usuario);
        Task RemoverPermisoAdministrador(UsuarioDTO usuario);
        Task RemoverPermisoProfesor(UsuarioDTO usuario);
    }
}
