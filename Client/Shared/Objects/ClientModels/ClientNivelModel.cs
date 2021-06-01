using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Horrografia.Shared.Models;

namespace Horrografia.Client.Shared.Objects.ClientModels
{
    public class ClientNivelModel
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(30, ErrorMessage = "El nombre no puede ser mayor a 30 caracteres.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [StringLength(250, ErrorMessage = "La descripción no puede ser mayor a 250 caracteres.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "El número de errores es obligatorio.")]
        [Range(1, 5, ErrorMessage = "Un nivel puede tener un máximo de 5 errores")]
        public int PossibleErrors { get; set; }

        [Required(ErrorMessage = "El número de items es obligatorio.")]
        [Range(5, 15, ErrorMessage = "Un nivel puede tener de 5 a 15 items.")]
        public int ItemNumber { get; set; }

        public NivelModel CreateNivelModel()
        {
            NivelModel _model = new();
            _model.Nombre = Name;
            _model.Descripcion = Description;
            _model.ErroresPermitidos = PossibleErrors;
            _model.NumeroDeItems = ItemNumber;
            return _model;
        }

    }
}
