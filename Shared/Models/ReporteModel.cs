using System;
using System.Collections.Generic;
using System.Text;

namespace Horrografia.Shared.Models
{
    public class ReporteModel
    {
        public int Id { get; set; }
        
        public string IdUsuario { get; set; }
        public int IdNivel { get; set; }
        public int CantidadErrores { get; set; }
        public int Puntuacion { get; set; }
        public string Fecha { get; set; }
    }
}