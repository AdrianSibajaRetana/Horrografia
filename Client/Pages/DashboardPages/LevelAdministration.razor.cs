﻿using System;
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

        // Diccionario que se construye a partir de las relaciones existentes. 
        private Dictionary<NivelModel, List<ItemModel>> ItemsPorNivel { get; set; }

        public LevelAdministration()
        {
            ShowCreateLevelDialog = false;
            ShowDeleteLevelDialog = false;
            ShowUpdateLevelDialog = false;
            ItemsPorNivel = new();
        }

        /*Carga los niveles existentes y notifica el estado actual*/
        protected override async Task OnInitializedAsync()
        {
            await CargarDatos(true);
        }

        public async Task CargarDatos(bool primerLoad)
        {
            IsLoading = true;
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
                InitialLoadStatus = Constantes.OKSTATUS;
                Niveles = nivelesResponse.Response;
                RelacionesNivelItem = relacionesResponse.Response;
                ItemsTotales = itemsResponse.Response;
                PistasTotales = pistasResponse.Response;
                GenerarDiccionario();
                ShowNotification("Se leyeron los niveles de la base de datos exitosamente", Severity.Success, primerLoad);
            }
            else
            {
                ShowNotification("Hubo un error al cargar los niveles de la base de datos.", Severity.Error, primerLoad);
            }
            IsLoading = false;
        }

        private void GenerarDiccionario()
        {
            foreach (var nivel in Niveles)
            {
                List<ItemModel> listaDeItems = new();
                ItemsPorNivel.Add(nivel, listaDeItems);
                var relacionesDeNivel = RelacionesNivelItem.Where(i => i.IdNivel == nivel.Id);
                foreach (var relacion in relacionesDeNivel)
                {
                    var item = ItemsTotales.Find(i => i.Id == relacion.IdItem);
                    ItemsPorNivel[nivel].Add(item);
                }
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
                ShowNotification("¡Se creó el nivel exitosamente!", Severity.Success, false);
            }
            else
            {
                ShowNotification("Hubo un error al crear el nivel.", Severity.Error, false);
            }
            await CargarDatos(false);
        }

        protected async Task DeleteLevel(NivelModel n)
        {
            ShowDeleteLevelDialog = false;
            var response = await _nivelService.DeleteAsync(n);
            if (response.isResponseSuccesfull())
            {
                ShowNotification("¡Se eliminó el nivel exitosamente!", Severity.Success, false);
            }
            else
            {
                ShowNotification("Hubo un error al eliminar el nivel.", Severity.Error, false);
            }
            nivelActual = null;
            await CargarDatos(false);
        }

        protected async Task UpdateLevel(NivelModel n)
        {
            ShowUpdateLevelDialog = false;
            var response = await _nivelService.UpdateAsync(n);
            if (response.isResponseSuccesfull())
            {
                ShowNotification($"¡Se actualizó el nivel exitosamente!", Severity.Success, false);
            }
            else
            {
                ShowNotification("Hubo un error al actualizar el nivel.", Severity.Error, false);
            }
            nivelActual = null;
            await CargarDatos(false);
        }

        protected async Task CreateClue(PistaModel p)
        {
            var response = await _pistaService.PostAsync(p);
            if (response.isResponseSuccesfull())
            {
                ShowNotification($"¡Se actualizaron las pistas!", Severity.Success, false);
            }
            else
            {
                ShowNotification("Hubo un error al crear la pista.", Severity.Error, false);
            }
            await CargarDatos(false);
        }

        protected async Task CreateItem(ItemModel i)
        {
            var response = await _itemService.PostAsync(i);
            if (response.isResponseSuccesfull())
            {
                ShowNotification($"¡Se actualizaron creó el item!", Severity.Success, false);
            }
            else
            {
                ShowNotification("Hubo un error al crear el item.", Severity.Error, false);
            }
            await CargarDatos(false);
        }

        protected async Task CreateRelation(int id)
        {
            throw new NotImplementedException();
        }
    }
}
