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

        private UsuarioDTO _usuarioActual { get; set; }        
        private bool _showData { get; set; }

        public RolesAsigner()
        {
            _showData = false;
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
            }
        }
    }
}
