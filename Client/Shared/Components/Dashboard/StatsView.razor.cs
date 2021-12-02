using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Horrografia.Client.Data.Services.Interfaces;
using Horrografia.Client.Shared.Objects.ClientModels;
using Horrografia.Shared.Models;
using MudBlazor;

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
        
        [Inject]
        protected IReporteService _reporteService { get; set; }
        
        [Inject]
        protected ITagService _tagService { get; set; }
        
        [Inject]
        protected ISnackbar _snackbar { get; set; }
        
        private Opciones OpcionEscogida { get; set; }
        private int MesSeleccionado { get; set; }
        private int AñoSeleccionado { get; set; }
        
        private bool IsLoading { get; set; }
        
        private List<ReporteModel> Partidas{ get; set; }
        
        private List<ReporteModel> PartidasLuegoDeSerFiltradas { get; set; }
        
        private Dictionary<string,int> TagCounterDictionary { get; set; }
        
       
        private bool EstaCargandoLasNuevasEstadisticas { get; set; }

        private bool EnseñarEstadísticas { get; set; }
        
        private string TituloDeEstadisticas { get; set; }

        private int AñoActual { get; set;}
        
        private int ErroresCometidos { get; set; }

        public StatsView()
        {
            AñoActual = DateTime.Now.Year;
            IsLoading = true;
            EstaCargandoLasNuevasEstadisticas = false;
            EnseñarEstadísticas = false;
            PartidasLuegoDeSerFiltradas = new();
        }
        
        protected override async Task OnInitializedAsync()
        {
            try
            {
                await CargarPartidas();
                IsLoading = false;
                StateHasChanged();
            }
            catch (Exception e)
            {
                ShowNotification(e.Message, Severity.Error);
            }
        }
        
        public void ShowNotification(string mensaje, Severity s)
        {
            _snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopCenter;
            _snackbar.Add(mensaje, s);
        }
        
        private async Task CargarPartidas()
        {
            var response = await _reporteService.GetAsync();
            if (response.isResponseSuccesfull())
            {
                Partidas = response.Response;
            }
            else
            {
                throw new InvalidOperationException($"Error al cargar partidas.");
            }
        }

        private async Task FiltrarPartidas()
        {
            EnseñarEstadísticas = true;
            EstaCargandoLasNuevasEstadisticas = true;
            try
            {
                switch (OpcionEscogida)
                {
                    case Opciones.TodasLasPartidas:
                        TituloDeEstadisticas = "Información de todas las partidas de Horrografía";
                        DevolverTodas();
                        break;
                    case Opciones.Año:
                        TituloDeEstadisticas = $"Información de todas las partidas jugadas en el año {AñoSeleccionado}";
                        FiltrarPorAño();
                        break;
                    case Opciones.Mes:
                        TituloDeEstadisticas = $"Información de todas las partidas jugadas en {GetMes()} del {AñoActual}";
                        FiltrarPorMes();
                        break;
                }
                ContarErroresCometidos();
                await ConseguirTiposDeErroresCometidos();
                EstaCargandoLasNuevasEstadisticas = false;
            }
            catch (Exception e)
            {
                ShowNotification(e.Message, Severity.Error);
            }
        }

        private void DevolverTodas()
        {
            PartidasLuegoDeSerFiltradas = Partidas;
        }

        private void FiltrarPorAño()
        {
            PartidasLuegoDeSerFiltradas = Partidas.Where(p => p.Fecha.Year == AñoSeleccionado).ToList();
        }

        private void FiltrarPorMes()
        {
            PartidasLuegoDeSerFiltradas =
                Partidas.Where(p => p.Fecha.Month == MesSeleccionado && p.Fecha.Year == AñoActual).ToList();
        }

        private string GetMes()
        {
            string Mes = "";
            switch (MesSeleccionado)
            {
                case 1:
                    Mes = "Enero";
                    break;
                case 2:
                    Mes = "Febrero";
                    break;
                case 3:
                    Mes = "Marzo";
                    break;
                case 4:
                    Mes = "Abril";
                    break;
                case 5:
                    Mes = "Mayo";
                    break;
                case 6:
                    Mes = "Junio";
                    break;
                case 7:
                    Mes = "Julio";
                    break;
                case 8:
                    Mes = "Agosto";
                    break;
                case 9:
                    Mes = "Septiembre";
                    break;
                case 10:
                    Mes = "Octubre";
                    break;
                case 11:
                    Mes = "Noviembre";
                    break;
                case 12:
                    Mes = "Diciembre";
                    break;
            }
            return Mes;
        }

        private void ContarErroresCometidos()
        {
            ErroresCometidos = 0;
            foreach (var reporte in PartidasLuegoDeSerFiltradas)
            {
                ErroresCometidos += reporte.CantidadErrores;
            }
        }

        private async Task ConseguirTiposDeErroresCometidos()
        {
            TagCounterDictionary = new();
            List<string> Tags = new();
            switch (OpcionEscogida)
            {
                case Opciones.TodasLasPartidas:
                    Tags = await ConseguirTodosLosTags();
                    break;
                case Opciones.Año:
                    Tags = await ConseguirTagsPorAño();
                    break;
                case Opciones.Mes:
                    Tags = await ConseguirTagsPorMes();
                    break;
            }
            foreach (var tag in Tags)
            {
                if(TagCounterDictionary.ContainsKey(tag))
                {
                    TagCounterDictionary[tag]++;
                }
                else
                {
                    TagCounterDictionary[tag] = 1;
                }
            }
        }

        private async Task<List<string>> ConseguirTodosLosTags()
        {
            List<string> Tags = new();
            var response = await _tagService.GetTagsFromAllReports();
            if (response.isResponseSuccesfull())
            {
                Tags = response.Response;
            }
            else
            {
                throw new InvalidOperationException($"Error al conseguir todos los tags.");
            }
            return Tags;
        }
        
        private async Task<List<string>> ConseguirTagsPorMes()
        {
            List<string> Tags = new();
            var response = await _tagService.GetTagsFromMonthlyReports(MesSeleccionado, AñoActual);
            if (response.isResponseSuccesfull())
            {
                Tags = response.Response;
            }
            else
            {
                throw new InvalidOperationException($"Error al conseguir los tags del mes.");
            }
            return Tags;
        }
        
        private async Task<List<string>> ConseguirTagsPorAño()
        {
            List<string> Tags = new();
            var response = await _tagService.GetTagsFromYearlyReports(AñoSeleccionado);
            if (response.isResponseSuccesfull())
            {
                Tags = response.Response;
            }
            else
            {
                throw new InvalidOperationException($"Error al conseguir los tags del año.");
            }
            return Tags;
        }
    }
}