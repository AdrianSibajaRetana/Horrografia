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
        public List<(ReporteModel, string)> Historial { get; set; }
        
        [Parameter]
        public List<NivelModel> NivelesInDatabase { get; set; }
        
        [Parameter]
        public List<ContieneErrorModel> Errores { get; set; }
        
        [Parameter]
        public EventCallback<int> OnMistakesRequest { get; set; }
        
        private bool ShowDetailsDialoge;

        private bool DialogIsLoading;

        private ReporteModel CurrentReporte;

        public UserHistory()
        {
            CurrentReporte = new();
            ShowDetailsDialoge = false;
            DialogIsLoading = false;
        }
        
        private string GetLevelNameFromId(int id)
        {
            return NivelesInDatabase.FirstOrDefault(n => n.Id == id)?.Nombre;
        }

        private async Task OpenDetailsDialoge(ReporteModel r)
        {
            if (CurrentReporte.Id != r.Id)
            {
                Errores = new List<ContieneErrorModel>();
                DialogIsLoading = true;
                ShowDetailsDialoge = true;
                if (r.CantidadErrores != 0)
                {
                    await OnMistakesRequest.InvokeAsync(r.Id);
                }
                DialogIsLoading = false;
                StateHasChanged();
            }
            else
            {
                DialogIsLoading = false;
                ShowDetailsDialoge = true;
            }
        }

        protected void CloseDialog()
        {
            ShowDetailsDialoge = false;
        }

    }
}
