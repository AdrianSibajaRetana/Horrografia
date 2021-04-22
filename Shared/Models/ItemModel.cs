using System;
using System.Collections.Generic;
using System.Text;

namespace Horrografia.Shared.Models
{
    class ItemModel
    {
        public int Id { get; set; }
        public string FormaCorrecta { get; set; }
        public string FormaIncorrecta { get; set; }
        public int PistaId { get; set; }
    }
}