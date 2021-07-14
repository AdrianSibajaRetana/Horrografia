using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Horrografia.Shared.Models;

namespace Horrografia.Client.Shared.Objects.ClientModels
{
    public class ClientUserRegisterDTO
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100)]
        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "El correo ingresado no es válido.")]
        [Required(ErrorMessage = "El correo es requerido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida")]
        public string Password { get; set; }

        [Required(ErrorMessage = "La confirmación de la contraseña es requerida.")]
        [Compare("Password", ErrorMessage = "La contraseña y la confirmación no son iguales.")]
        public string ConfirmPassword { get; set; }
    }
}
