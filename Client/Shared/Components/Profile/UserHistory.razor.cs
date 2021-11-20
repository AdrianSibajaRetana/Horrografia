using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Horrografia.Client.Shared.Objects.ClientModels;
using Horrografia.Shared.Models;

namespace Horrografia.Client.Shared.Components.Profile
{
    public partial class UserHistory
    {
        [Parameter]
        public List<ReporteModel> Historial { get; set; }
        
        [Parameter]
        public List<NivelModel> NivelesInDatabase { get; set; }

        private string GetLevelNameFromId(int id)
        {
            return NivelesInDatabase.FirstOrDefault(n => n.Id == id)?.Nombre;
        }
    }
}
