using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Horrografia.Client.Shared.Objects.ClientModels;
using Horrografia.Shared.Models;

namespace Horrografia.Client.Shared.Components.Dashboard
{
    public partial class StatsView
    {
        enum Opciones
        {
            TodasLasPartidas,
            Año,
            Mes
        }

        enum Meses
        {
            Enero,
            Febrero,
            Marzo,
            Abril,
            Mayo,
            Junio,
            Julio,
            Agosto,
            Septiembre,
            Octubre,
            Noviembre,
            Diciembre
        }

        private Opciones OpcionEscogida { get; set; }
        private Meses MesSeleccionado { get; set; }
        private int AñoSeleccionado { get; set; }
        

    }
}