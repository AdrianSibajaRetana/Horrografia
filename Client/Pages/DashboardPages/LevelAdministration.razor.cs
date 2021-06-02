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
        public bool ShowDeleteLevelDialog { get; set; }

        //Nivel que se está enseñando en el UI
        public NivelModel nivelActual { get; set; }

        //Función que utiliza el dropdown para desplegar el nivel.
        // Cambiar de descripción a nombre. 
        Func<NivelModel, string> converter = p => p?.Nombre;


        public LevelAdministration()
        {
            ShowCreateLevelDialog = false;
            ShowDeleteLevelDialog = false;
        }

        /*Carga los niveles existentes y notifica el estado actual*/
        protected override async Task OnInitializedAsync()
        {
            await CargarDatos(true);
        }

        public async Task CargarDatos(bool primerLoad)
        {
            IsLoading = true;
            var response = await _nivelService.GetAsync();
            InitialLoadStatus = response.Status;
            Niveles = response.Response;
            IsLoading = false;
            if (response.isResponseSuccesfull())
            {
                ShowNotification("Se leyeron los niveles de la base de datos exitosamente", Severity.Success, primerLoad);
            }
            else
            {
                ShowNotification("Hubo un error al cargar los niveles de la base de datos.", Severity.Error, primerLoad);
            }
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

        // Abre dialogo para crear niveles
        public void ShowCrearNivel()
        {
            ShowCreateLevelDialog = true;            
        }

        // Cierra dialogo para crear niveles
        protected void CloseDialogWindow()
        {
            ShowCreateLevelDialog = false;
        }

        // Abre dialogo para borrar niveles
        public void ShowDeleteNivel()
        {
            ShowDeleteLevelDialog = true;
        }

        // Cierra dialogo para borrar niveles
        protected void CloseDeletionDialog()
        {
            ShowDeleteLevelDialog = false;
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
            await CargarDatos(false);
        }
    }
}
