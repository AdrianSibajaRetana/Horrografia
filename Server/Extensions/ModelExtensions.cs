using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Horrografia.Shared.Models;
using Horrografia.Server.Models;

namespace Horrografia.Server.Extensions
{
    public static class ModelExtensions
    {
        public static UsuarioDTO getDTOFromApplicationUser(this ApplicationUser user)
        {
            UsuarioDTO newDTO = new UsuarioDTO(user.NombreDeUsuario, user.NormalizedEmail, user.Id, user.CodigoEscuela);
            return newDTO;
        }
    }
}
