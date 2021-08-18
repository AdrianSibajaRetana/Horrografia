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
using Horrografia.Client.Shared.Components.Dashboard.Permission_Administration;

namespace Horrografia.Client.Pages.DashboardPages
{
    public partial class PermissionAdministration
    {
        [Inject]
        protected IUserService _userService { get; set; }

        [Inject]
        protected ISnackbar _snackbar { get; set; }

        // Todos los usuarios de la aplicación
        private List<UsuarioDTO> Usuarios { get; set; }

        // booleano que me indica que la renderización fue exitosa.
        private bool _lecturaExitosa { get; set; }

        private int InitialLoadStatus { get; set; }

        private bool IsLoading { get; set; }

        public PermissionAdministration()
        {
            IsLoading = true;
            _lecturaExitosa = false;
        }

        protected override async Task OnInitializedAsync()
        {
            await CargarDatos();
        }

        public async Task CargarDatos()
        {
            try
            {
                await CargarUsuarios();
                _lecturaExitosa = true;
            }
            catch (InvalidOperationException e)
            {
                _lecturaExitosa = false;
                ShowNotification($"{e.Message}", Severity.Error);
            }
        }

        private async Task CargarUsuarios()
        {
            var usersResponse = await _userService.GetAsync();
            if (usersResponse.isResponseSuccesfull())
            {
                Usuarios = usersResponse.Response;
            }
            else
            {
                string reporteError = "Usuarios";
                throw new InvalidOperationException($"Error al cargar los {reporteError}.");
            }
        }

        protected override void OnParametersSet()
        {
            IsLoading = false;
            if (_lecturaExitosa)
            {
                _snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopCenter;
                InitialLoadStatus = Constantes.OKSTATUS;
                ShowNotification("Se cargaron los datos de la base de datos exitosamente", Severity.Success);
            }
            else
            {
                InitialLoadStatus = Constantes.INTERNALERRORSTATUS;
                ShowNotification("Hubo un error al interactuar con la base de datos.", Severity.Error);
            }
        }

        public void ShowNotification(string mensaje, Severity s)
        {
            //Muestra la notifiación
            _snackbar.Add(mensaje, s);
        }
    }
}