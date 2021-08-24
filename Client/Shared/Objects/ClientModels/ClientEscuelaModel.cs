using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Horrografia.Shared.Models;

namespace Horrografia.Client.Shared.Objects.ClientModels
{
    public class ClientEscuelaModel
    {
        [Required(ErrorMessage = "La escuela debe tener un nombre")]
        [StringLength(100, ErrorMessage = "El nombre de la escuela no puede ser mayor a 100 caracteres")]
        public string Nombre { get; set; }

    }
}
