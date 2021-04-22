using System;
using System.Collections.Generic;
using System.Text;

namespace Horrografia.Shared.Models
{
    class NivelModel
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int ErroresPermitidos { get; set; }
        public int NumeroDeItems { get; set; }
    }
}