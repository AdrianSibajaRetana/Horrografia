using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Horrografia.Shared.Models;
using Horrografia.Client.Data.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Horrografia.Client.Shared.Objects;
using MudBlazor;
using Horrografia.Client.Shared.Constants;

namespace Horrografia.Client.Pages.DashboardPages
{
    public partial class LevelAdministration : IComponent
    {
        [Inject]
        protected INivelService _nivelService { get; set; }

        [Inject]
        protected ISnackbar _snackbar { get; set; }


        public List<NivelModel> Niveles { get; set; }
        public bool IsLoading { get; set; }
        public int InitialLoadStatus { get; set; }

        public bool ShowCreateLevelDialog { get; set; }

        //Nivel que se está enseñando en el UI
        public NivelModel nivelActual { get; set; }

        //Función que utiliza el dropdown para desplegar el nivel.
        // Cambiar de descripción a nombre. 
        Func<NivelModel, string> converter = p => p?.Descripcion;

        public LevelAdministration()
        {
            IsLoading = true;
            ShowCreateLevelDialog = false;
        }

        /*Carga los niveles existentes y notifica el estado actual*/
        protected override async Task OnInitializedAsync()
        {
            var response = await _nivelService.GetAsync();
            InitialLoadStatus = response.Status;
            Niveles = response.Response;
            IsLoading = false;
            ShowNotification("Se leyeron los niveles de la base de datos exitosamente", Severity.Success, true);
        }


        public void ShowNotification(string mensaje, Severity s, bool firstMessage)
        {
            if (firstMessage) 
            { 
                //Configura la notificación en el centro.
                _snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopCenter;
            }
            //Muestra la notifiación
            _snackbar.Add(mensaje, s);
        }

        public void ShowCrearNivel()
        {
            ShowCreateLevelDialog = true;            
        }

        protected void CloseDialogWindow()
        {
            ShowCreateLevelDialog = false;
        }

        protected async Task CreateLevel(NivelModel n)
        {
            ShowCreateLevelDialog = false;
            var response = await _nivelService.PostAsync(n);
            if (response.isResponseSuccesfull())
            {
                ShowNotification("¡Se creó el nivel exitosamente!", Severity.Success, false);
            }
            else
            {
                ShowNotification("Hubo un error al crear el nivel.", Severity.Error, false);
            }
            // Añadir a que la pantalla cambie sola
            StateHasChanged();
        }

    }
}
