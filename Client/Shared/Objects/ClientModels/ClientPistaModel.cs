using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Horrografia.Shared.Models;

namespace Horrografia.Client.Shared.Objects.ClientModels
{
    public class ClientPistaModel
    {
        [Required(ErrorMessage = "La pista es obligatoria.")]
        [StringLength(100, ErrorMessage = "La pista no puede ser mayor a 100 caracteres.")]
        public string Pista { get; set; }

        public PistaModel generarPistaModel()
        {
            PistaModel p = new();
            p.Pista = Pista;
            return p;
        }
    }
}
