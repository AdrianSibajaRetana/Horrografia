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
        Task<UsuarioDTO> GetUserById(string id);
        Task<bool> CrearUsuario(ClientUserRegisterDTO usuarioACrear);
        Task<SharedConstants.LoginState> IniciarSesion(ClientUserLoginDTO modeloEnviado);
        Task<bool> CorreoDisponible(string correo);
        Task CerrarSesion();
        Task OtorgarPermisoAdministrador(UsuarioDTO usuario);
        Task OtorgarPermisoProfesor(UsuarioDTO usuario);
        Task RemoverPermisoAdministrador(UsuarioDTO usuario);
        Task RemoverPermisoProfesor(UsuarioDTO usuario);
        Task UpdateData(UsuarioDTO usuario);
        Task<List<string>> GetStudentsBySchoolCode(string codigo);
        Task<List<string>> GetProfessorsBySchoolCode(string codigo);
    }
}
