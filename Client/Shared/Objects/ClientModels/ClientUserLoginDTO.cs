using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Horrografia.Shared.Models;

namespace Horrografia.Client.Shared.Objects.ClientModels
{
    public class ClientUserLoginDTO
    {
        [Required(ErrorMessage = "Se requiere el correo para inciar sesión")]
        [EmailAddress(ErrorMessage = "El correo ingresado no es válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Se requiere la constraseña para iniciar sesión")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        public bool RememberMe { get; set; }
    }
}
