using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Horrografia.Shared.Models
{
    public class UsuarioDTO
    {
        public string NombreDeUsuario { get; set; }
        public string correo { get; set; }
        public string Id { get; set; }

        public UsuarioDTO(string username, string email, string userID)
        {
            NombreDeUsuario = username;
            correo = email;
            Id = userID;
        }
    }
}
