using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Horrografia.Shared.Models;
using Horrografia.Client.Data.Services.Interfaces;
using MudBlazor;

namespace Horrografia.Client.Pages
{
    public partial class PaginaDeJuego
    {
        [Inject]
        protected INivelService _nivelService { get; set; }
        
        [Inject]
        protected IItemService _itemService { get; set; }

        [Inject]
        protected IPistaService _pistaService { get; set; }

        [Inject]
        protected IFormaIncorrectaService _formaIncorrectaService {get; set;}
        
        [Inject]
        protected ISnackbar _snackbar { get; set; }
        
        //Todos los niveles
        private List<NivelModel> Niveles { get; set; }

        private NivelModel NivelSeleccionado { get; set; }
        
        private int _IdNivelEscogido { get; set; }
        
        //Para el nivel
        //Todos los items.
        private List<ItemModel> ItemsTotales { get; set; }
        // Todas las pistas
        private List<PistaModel> PistasTotales { get; set; }
        // Todas las formas incorrectas
        private List<FormaIncorrectaModel> FormasIncorrectasTotales { get; set; }
        
        private int _IdEnSeleccionDePagina
        { 
            get => _IdNivelEscogido;
            set
            {
                _IdNivelEscogido = value;
                NivelSeleccionado = Niveles.FirstOrDefault(n => n.Id == _IdNivelEscogido);
            }
        }
        
        private bool IsLoading { get; set; }

        private bool IsChoosingLevel { get; set;}

        private bool IsLoadingGame { get; set; }

        private string LoadingStatus { get; set; }

        private bool SuccesfulGameLoad { get; set; }

        public PaginaDeJuego()
        {
            IsLoading = true;
            IsChoosingLevel = true;
            IsLoadingGame = true;
            SuccesfulGameLoad = false;
        }

        protected override async Task OnInitializedAsync()
        {
            _snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopCenter;
            await CargarNiveles();
        }
        
        private async Task CargarNiveles()
        {
            var nivelesResponse = await _nivelService.GetAsync();
            if (nivelesResponse.isResponseSuccesfull())
            {
                Niveles = nivelesResponse.Response;
            }
            else
            {
                string reporteError = "Error al cargar los niveles.";
                ShowNotification(reporteError, Severity.Error);
            }
        }
        
        private void ShowNotification(string mensaje, Severity s)
        {
            _snackbar.Add(mensaje, s);
        }

        protected override void OnParametersSet()
        {
            IsLoading = false;
        }
        
        private async Task BuscarDatosDeNivel()
        {
            if(NivelSeleccionado != null)
            {
                IsChoosingLevel = false;
                try
                {
                    LoadingStatus = "Cargando las palabras del nivel...";
                    await CargarItems();
                    LoadingStatus = "Consiguiendo las posibles pistas...";
                    await CargarPistas();
                    LoadingStatus = "Buscando las formas incorrectas...";
                    await CargarFormasIncorrectas();
                    SuccesfulGameLoad = true;
                }
                catch(InvalidOperationException e)
                {
                    ShowNotification($"{e.Message}", Severity.Error);
                }
                IsLoadingGame = false;    
            }
            else
            {
                ShowNotification("Es necesario escoger un nivel antes de jugar.", Severity.Error);
            }
        }
        
        private async Task CargarItems()
        {
            var itemsResponse = await _itemService.GetAsyncByLevelId(NivelSeleccionado.Id);
            if (itemsResponse.isResponseSuccesfull())
            {
                ItemsTotales = itemsResponse.Response;
            }
            else
            {
                string reporteError = "items";
                throw new InvalidOperationException($"Error al cargar los {reporteError}.");
            }
        }
        
        private async Task CargarPistas()
        {
            var pistasResponse = await _pistaService.GetAsync();
            if (pistasResponse.isResponseSuccesfull())
            {
                PistasTotales = pistasResponse.Response;
            }
            else
            {
                string reporteError = "Pistas";
                throw new InvalidOperationException($"Error al cargar las {reporteError}.");
            }
        }
        
        private async Task CargarFormasIncorrectas()
        {
            var formasIncorrectas = await _formaIncorrectaService.GetFormasIncorrectasFromLevelId(NivelSeleccionado.Id);
            if (formasIncorrectas.isResponseSuccesfull())
            {
                FormasIncorrectasTotales = formasIncorrectas.Response;
            }
            else
            {
                string reporteError = "Formas Incorrectas";
                throw new InvalidOperationException($"Error al cargar las {reporteError}.");
            }
        }

    }
}