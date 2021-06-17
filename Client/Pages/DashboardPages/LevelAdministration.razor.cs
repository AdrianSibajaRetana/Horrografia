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

        [Inject]
        protected IPerteneceAService _perteneceAService { get; set; }

        [Inject]
        protected IItemService _itemService { get; set; }

        [Inject]
        protected IPistaService _pistaService { get; set; }

        private bool IsLoading { get; set; }
        private int InitialLoadStatus { get; set; }

        //Función que utiliza el dropdown para desplegar el nivel.
        // Cambiar de descripción a nombre. 
        Func<NivelModel, string> converter = p => p?.Nombre;

        // Booleanos que controlan la visión de los dialogos
        private bool ShowCreateLevelDialog { get; set; }
        private bool ShowDeleteLevelDialog { get; set; }
        private bool ShowUpdateLevelDialog { get; set; }

        //Nivel que se está enseñando en el UI
        private NivelModel nivelActual { get; set; }

        // Todos los niveles
        private List<NivelModel> Niveles { get; set; }

        // Todas las relaciones entre items y niveles.
        private List<PerteneceAModel> RelacionesNivelItem { get; set; }

        // Todos los items
        private List<ItemModel> ItemsTotales { get; set; }

        // Todas las pistas
        private List<PistaModel> PistasTotales { get; set; }

        // booleano que me indica que la renderización fue exitosa.
        private bool _lecturaExitosa { get; set; }

        // En primera creación de instancia se asignan los valores.
        public LevelAdministration()
        {
            IsLoading = true;
            ShowCreateLevelDialog = false;
            ShowDeleteLevelDialog = false;
            ShowUpdateLevelDialog = false;            
            _lecturaExitosa = false;
        }

        /*Carga los niveles existentes y notifica el estado actual*/
        protected override async Task OnInitializedAsync()
        {
            await CargarDatos();
        }

        public async Task CargarDatos()
        {   
            var nivelesResponse = await _nivelService.GetAsync();
            var relacionesResponse = await _perteneceAService.GetAsync();
            var itemsResponse = await _itemService.GetAsync();
            var pistasResponse = await _pistaService.GetAsync();
            if (
                    nivelesResponse.isResponseSuccesfull()
                    && relacionesResponse.isResponseSuccesfull()
                    && itemsResponse.isResponseSuccesfull()
                    && pistasResponse.isResponseSuccesfull()
                )
            {
                _lecturaExitosa = true;
                Niveles = nivelesResponse.Response;
                RelacionesNivelItem = relacionesResponse.Response;
                ItemsTotales = itemsResponse.Response;
                PistasTotales = pistasResponse.Response;
            }
            else
            {
                _lecturaExitosa = false;
                InitialLoadStatus = Constantes.INTERNALERRORSTATUS;
            }
        }

        //Método que se llama después del OnInitialized. 
        protected override void OnParametersSet()
        {
            IsLoading = false;
            if (_lecturaExitosa)
            {
                _snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopCenter;
                InitialLoadStatus = Constantes.OKSTATUS;
                ShowNotification("Se leyeron los niveles de la base de datos exitosamente", Severity.Success);
            }
            else
            {
                ShowNotification("Hubo un error al cargar los niveles de la base de datos.", Severity.Error);
            }                            
        }

        // Devuelve los items por nivel
        private List<ItemModel> GetItems(NivelModel n)
        {
            List<ItemModel> NivelItems = new();
            if (n != null)
            { 
                var ItemIds = RelacionesNivelItem.Where(r => r.IdNivel == n.Id).Select(r => r.IdItem);
                NivelItems = ItemsTotales.Where(i => ItemIds.Contains(i.Id)).ToList();            
            }
            return NivelItems;
        }

        private int GetMaxItems(NivelModel n)
        {
            return n.NumeroDeItems;
        }

        public void ShowNotification(string mensaje, Severity s)
        {
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

        // Abre el dialogo para actualizar los datos del nivel.
        public void ShowUpdateNivel()
        {
            ShowUpdateLevelDialog = true;
        }

        // Cierra el dialogo para actualizar los datos del nivel.
        protected void CloseUpdateDialog()
        {
            ShowUpdateLevelDialog = false;
        }

        protected async Task CreateLevel(NivelModel n)
        {
            ShowCreateLevelDialog = false;
            var response = await _nivelService.PostAsync(n);
            if (response.isResponseSuccesfull())
            {
                ShowNotification("¡Se creó el nivel exitosamente!", Severity.Success);
            }
            else
            {
                ShowNotification("Hubo un error al crear el nivel.", Severity.Error);
            }
            await CargarDatos();
        }

        protected async Task DeleteLevel(NivelModel n)
        {
            ShowDeleteLevelDialog = false;
            var response = await _nivelService.DeleteAsync(n);
            if (response.isResponseSuccesfull())
            {
                ShowNotification("¡Se eliminó el nivel exitosamente!", Severity.Success);
            }
            else
            {
                ShowNotification("Hubo un error al eliminar el nivel.", Severity.Error);
            }
            nivelActual = null;
            await CargarDatos();
        }

        protected async Task UpdateLevel(NivelModel n)
        {
            ShowUpdateLevelDialog = false;
            var response = await _nivelService.UpdateAsync(n);
            if (response.isResponseSuccesfull())
            {
                ShowNotification($"¡Se actualizó el nivel exitosamente!", Severity.Success);
            }
            else
            {
                ShowNotification("Hubo un error al actualizar el nivel.", Severity.Error);
            }
            nivelActual = null;
            await CargarDatos();
        }

        protected async Task CreateClue(PistaModel p)
        {
            var response = await _pistaService.PostAsync(p);
            await CargarDatos();
        }

        protected async Task CreateItem(ItemModel i)
        {
            var response = await _itemService.PostAsync(i);
            await CargarDatos();
        }

        protected async Task UpdateItem(ItemModel i)
        {
            var response = await _itemService.UpdateAsync(i);
            if (response.isResponseSuccesfull())
            {
                ShowNotification($"¡Se actualizó el item!", Severity.Success);
            }
            else
            {
                ShowNotification("Hubo un error al actualizar el item.", Severity.Error);
            }
            await CargarDatos();
        }

        protected async Task DeleteItem(ItemModel i)
        {
            var response = await _itemService.DeleteAsync(i);
            if (response.isResponseSuccesfull())
            {
                ShowNotification($"¡Se eliminó el item exitosamente!", Severity.Success);
            }
            else
            {
                ShowNotification("Hubo un error al borrar el item.", Severity.Error);
            }
            await CargarDatos();
        }

        protected async Task CreateRelation(int id)
        {
            PerteneceAModel p = new();
            p.IdNivel = nivelActual.Id;
            p.IdItem = id;
            var response = await _perteneceAService.PostAsync(p);
            if (response.isResponseSuccesfull())
            {
                ShowNotification($"¡Se añadió el item en el nivel!", Severity.Success);
            }
            else
            {
                ShowNotification("Hubo un error al crear la relación.", Severity.Error);
            }
            await CargarDatos();
        }
    }
}
