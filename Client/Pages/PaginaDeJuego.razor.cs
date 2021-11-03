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
        protected ISnackbar _snackbar { get; set; }
        
        //Todos los niveles
        private List<NivelModel> Niveles { get; set; }

        private NivelModel NivelSeleccionado { get; set; }
        
        private int _IdNivelEscogido { get; set; }
        
        private int _IdEnSeleccionDePagina
        { 
            get => _IdNivelEscogido;
            set
            {
                _IdNivelEscogido = value;
                NivelSeleccionado = Niveles.FirstOrDefault(n => n.Id == _IdNivelEscogido);
            }
        }
        private const int INVALID_OPTION = -10000; 
        
        private bool IsLoading { get; set; }

        public PaginaDeJuego()
        {
            IsLoading = true; 
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

    }
}