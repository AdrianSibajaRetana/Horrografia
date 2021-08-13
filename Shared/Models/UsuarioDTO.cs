using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace Horrografia.Shared.Models
{
    public class UsuarioDTO
    {
        public string nombreDeUsuario { get; set; }
        public string correo { get; set; }
        public string id { get; set; }

        public UsuarioDTO(string username, string email, string userID)
        {
            nombreDeUsuario = username;
            correo = email;
            id = userID;
        }

        [JsonConstructor]
        public UsuarioDTO() { }
    }
}
