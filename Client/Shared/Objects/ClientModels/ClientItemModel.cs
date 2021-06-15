using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Horrografia.Shared.Models;

namespace Horrografia.Client.Shared.Objects.ClientModels
{
    public class ClientItemModel
    {

        [Required(ErrorMessage = "La forma correcta es obligatoria.")]
        [StringLength(100, ErrorMessage = "La forma correcta no puede ser mayor a 100 caracteres")]
        public string FormaCorrecta { get; set; }

        [Required(ErrorMessage = "La forma incorrecta es obligatoria.")]
        [StringLength(100, ErrorMessage = "La forma incorrecta no puede ser mayor a 100 caracteres")]
        public string FormaIncorrecta { get; set; }

        [Required(ErrorMessage = "La pista es obligatoria.")]
        [StringLength(100, ErrorMessage = "La pista no puede ser mayor a 100 caracteres")]
        public string Pista { get; set; }

        public int Id { get; set; }

        public int Pistaid { get; set; }

        // Construyo un clientModel a partir de un modelo existente. 
        public void SetDataFromModel(ItemModel model)
        {
            Id = model.Id;
            FormaCorrecta = model.FormaCorrecta;
            FormaIncorrecta = model.FormaIncorrecta;
            Pistaid = model.PistaId;
        }

        public ItemModel GetItemModel()
        {
            ItemModel i = new();
            i.Id = Id;
            i.FormaCorrecta = FormaCorrecta;
            i.FormaIncorrecta = FormaIncorrecta;
            i.PistaId = Pistaid;
            return i;
        }
    }
}
