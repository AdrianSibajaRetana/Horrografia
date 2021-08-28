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
        public string codigoEscuela { get; set; }

        public UsuarioDTO(string username, string email, string userID, string schoolCode)
        {
            nombreDeUsuario = username;
            correo = email;
            id = userID;
            codigoEscuela = schoolCode;
        }

        public bool IsUserinSchool()
        {
            bool returnedValue = false; 
            if (!string.IsNullOrEmpty(codigoEscuela))
            {
                returnedValue = true; 
            }
            return returnedValue;
        }

        [JsonConstructor]
        public UsuarioDTO() { }
    }
}
