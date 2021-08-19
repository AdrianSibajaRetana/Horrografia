using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Horrografia.Client.Shared.Objects.ClientModels;
using Horrografia.Shared.Models;

namespace Horrografia.Client.Shared.Components.Dashboard.Permission_Administration
{
    public partial class RolesAsigner
    {
        [Parameter]
        public List<UsuarioDTO> Usuarios { get; set; }

        [Parameter]
        public List<RolModel> Roles { get; set; }

        [Parameter]
        public List<UserRolesModel> UsuariosYRoles { get; set; }

        [Parameter]
        public EventCallback<string> OnErrorDetection { get; set; }

        private enum DataShown
        {
            UserData,
            UserPermissions
        }

        private UsuarioDTO _usuarioActual { get; set; }
        private bool _showData { get; set; }
        private DataShown _dataShown;

        // Permisos actuales en la base de datos
        private bool _esAdministrador {get; set;}
        private bool _esProfesor { get; set; }

        // Permisos después de los cambios
        private bool _cambiarAAdministrador { get; set; }
        private bool _cambiarAProfesor { get; set; }

        // Demuestra si el componente está cargando
        private bool _isLoading { get; set; }

        public RolesAsigner()
        {
            _showData = false;
            _isLoading = false;
        }

        private async Task<IEnumerable<UsuarioDTO>> SearchAsync(string value)
        {
            // In real life use an asynchronous function for fetching data from an api.
            await Task.Delay(5);

            var correosTotales = Usuarios.Select(e => e.correo);

            // if text is null or empty, show complete list
            if (string.IsNullOrEmpty(value))
            {
                return Usuarios;
            }
            return Usuarios.Where(c => c.correo.Contains(value, StringComparison.InvariantCultureIgnoreCase));
        }

        private void ShowUserData()
        {
            if (!(_usuarioActual == null))
            {
                _showData = true;
                _dataShown = DataShown.UserData;
                SearchUserRoles();
            }
        }

        private void SwitchDataShown()
        {
            if (_dataShown == DataShown.UserData)
            {
                _dataShown = DataShown.UserPermissions;
            }
            else if (_dataShown == DataShown.UserPermissions)
            {
                _dataShown = DataShown.UserData;
            }
        }

        private void SearchUserRoles()
        {
            _esAdministrador = false;
            _esProfesor = false;
            foreach (var rol in Roles)
            {
                bool exists = UsuariosYRoles.Where(r => r.UserId == _usuarioActual.id)
                                            .Where(r => r.RoleId == rol.Id)
                                            .Any();
                if (exists)
                { 
                    if (rol.Name == "Admin")
                    {
                        _esAdministrador = true;
                    }
                    else if (rol.Name == "Profesor")
                    {
                        _esProfesor = true;
                    }
                }
            }
            _cambiarAAdministrador = _esAdministrador;
            _cambiarAProfesor = _esProfesor;
        }

        private async Task SaveChanges()
        {
            _isLoading = true;
            if (_cambiarAAdministrador && _cambiarAProfesor)
            {
                await OnErrorDetection.InvokeAsync("El usuario no puede tener dos roles");
            }
            else
            { 
                if (!_esAdministrador && _cambiarAAdministrador)
                {
                    //cambioAdmin = "Hacer el usuario un administrador";
                }
                if (_esAdministrador && !_cambiarAAdministrador)
                {
                    //cambioAdmin = "Quitar permisos de administrador";
                }
                if (!_esProfesor && _cambiarAProfesor)
                {
                    //cambioProfe = "Hacer el usuario un profesor";
                }
                if (_esProfesor && !_cambiarAProfesor)
                {
                    //cambioProfe = "Quitar permisos de profesor";
                }            
            }
            _isLoading = false;
        }
    }
}
