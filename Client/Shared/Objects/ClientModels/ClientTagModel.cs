using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Horrografia.Shared.Models;
namespace Horrografia.Client.Shared.Objects.ClientModels
{
    public class ClientTagModel
    {
        [Required(ErrorMessage = "El tag es obligatorio.")]
        [StringLength(100, ErrorMessage = "El tag no puede ser mayor a 100 caracteres.")]
        public string Tag { get; set; }

        public TagModel generarTagModel()
        {
            TagModel t = new();
            t.Tag = Tag; 
            return t;
        }
    }
}
