using System;
using System.Collections.Generic;
using System.Text;

namespace Horrografia.Shared.Models
{
    public class ContieneErrorModel
    {
        public int idReporte { get; set; }
        public int idItem { get; set; }
        public string respuesta { get; set; }

        public string original { get; set; }
    }
}
