using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Horrografia.Shared.Models;
using Horrografia.Client.Data.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Horrografia.Client.Shared.Objects;
using MudBlazor;

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

        public LevelAdministration()
        {
            IsLoading = true;     
        }

        /*Carga los niveles existentes y notifica el estado actual*/
        protected override async Task OnInitializedAsync()
        {
            var response = await _nivelService.GetAsync();
            InitialLoadStatus = response.Status;
            Niveles = response.Response;
            IsLoading = false;
            ShowNotification("Se leyeron los niveles de la base de datos exitosamente", Severity.Success);
        }

        public void ShowNotification(string mensaje, Severity s)
        {
            //Configura la notificación en el centro.
            _snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopCenter;
            //Muestra la notifiación
            _snackbar.Add(mensaje, s);
        }
    }
}
