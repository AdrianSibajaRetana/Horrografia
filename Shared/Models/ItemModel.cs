using System;
using System.Collections.Generic;
using System.Text;

namespace Horrografia.Shared.Models
{
    public class ItemModel
    {
        public int Id { get; set; }
        public string FormaCorrecta { get; set; }
        public int? PistaId { get; set; }
        public int NivelId { get; set; }
    }
}