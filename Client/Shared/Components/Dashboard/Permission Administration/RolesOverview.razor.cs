using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Horrografia.Client.Shared.Objects.ClientModels;
using Horrografia.Shared.Models;

namespace Horrografia.Client.Shared.Components.Dashboard.Permission_Administration
{
    public partial class RolesOverview
    {
        [Parameter]
        public List<UsuarioDTO> Usuarios { get; set; }

        [Parameter]
        public List<RolModel> Roles { get; set; }

        [Parameter]
        public List<UserRolesModel> UsuariosYRoles { get; set; }

        private int _cantidadUsuarios { get; set; }

        private int _cantidadAdministradores { get; set; }

        private int _cantidadProfesores { get; set; }


        protected override void OnParametersSet()
        {
            CalcularCantidadUsuarios();
            CalcularRoles();
        }

        private void CalcularCantidadUsuarios()
        {
            _cantidadUsuarios = Usuarios.Count();
        }

        private void CalcularRoles()
        {
            int adminsCantidad = 0;
            int profesCantidad = 0;
            string adminId = Roles.Where(r => r.Name == "Admin").Select(r => r.Id).FirstOrDefault();
            string profesorId = Roles.Where(r => r.Name == "Admin").Select(r => r.Id).FirstOrDefault();
            foreach (var relacion in UsuariosYRoles)
            {
                if (relacion.RoleId == adminId)
                {
                    adminsCantidad++;
                }
                if (relacion.RoleId == profesorId)
                {
                    profesCantidad++;
                }
            }
            _cantidadAdministradores = adminsCantidad;
            _cantidadProfesores = profesCantidad;
        }
    }
}
