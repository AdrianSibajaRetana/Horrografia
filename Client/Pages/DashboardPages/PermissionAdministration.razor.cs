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
using Horrografia.Client.Shared.Components.Dashboard.Permission_Administration;

namespace Horrografia.Client.Pages.DashboardPages
{
    public partial class PermissionAdministration
    {
        [Inject]
        protected IUserService _userService { get; set; }

        [Inject]
        protected IRolService _rolService { get; set; }

        [Inject]
        protected IEscuelaService _escuelaService { get; set; }

        [Inject]
        protected ISnackbar _snackbar { get; set; }

        // Todos los usuarios de la aplicación
        private List<UsuarioDTO> Usuarios { get; set; }

        //Todos los roles en la aplicación 
        private List<RolModel> Roles { get; set; }

        //Todos los usuarios y sus roles respectivos en la aplicación
        private List<UserRolesModel> UsuariosYRoles { get; set; }

        //Todas las escuelas en la base de datos
        private List<EscuelaModel> Escuelas { get; set; }

        // booleano que me indica que la renderización fue exitosa.
        private bool _lecturaExitosa { get; set; }

        private int InitialLoadStatus { get; set; }

        private bool IsLoading { get; set; }

        //Para el manejo de tabs
        private enum ActiveTabState
        {
            ShowGeneralTab,
            ShowPermissionsTab,
            ShowSchoolsTab,
        }

        private string _generalActiveString { get; set; }
        private string _permissionActiveString { get; set; }
        private string _schoolsActiveString { get; set; }
        private ActiveTabState _activeTab { get; set; }
        private const string ShowActiveState = "is-active";

        public PermissionAdministration()
        {
            IsLoading = true;
            _lecturaExitosa = false;
            _activeTab = ActiveTabState.ShowGeneralTab;
            _generalActiveString = ShowActiveState;
            _permissionActiveString = null;
            _schoolsActiveString = null;
        }

        private void ChangeTabContent(ActiveTabState StateToChange)
        {

            if (StateToChange != _activeTab)
            {
                _activeTab = StateToChange;
                switch (StateToChange)
                {
                    case ActiveTabState.ShowGeneralTab:
                        _generalActiveString = ShowActiveState;
                        _permissionActiveString = null;
                        _schoolsActiveString = null;
                        break;
                    case ActiveTabState.ShowPermissionsTab:
                        _generalActiveString = null;
                        _permissionActiveString = ShowActiveState;
                        _schoolsActiveString = null;
                        break;
                    case ActiveTabState.ShowSchoolsTab:
                        _generalActiveString = null;
                        _permissionActiveString = null;
                        _schoolsActiveString = ShowActiveState;
                        break;
                }
            }
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
                await CargarRoles();
                await CargarRolesYUsuarios();
                await CargarEscuelas();
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

        private async Task CargarRoles()
        {
            var usersResponse = await _rolService.GetRoles();
            if (usersResponse.isResponseSuccesfull())
            {
                Roles = usersResponse.Response;
            }
            else
            {
                string reporteError = "Roles";
                throw new InvalidOperationException($"Error al cargar los {reporteError}.");
            }
        }

        private async Task CargarRolesYUsuarios()
        {
            var usersResponse = await _rolService.GetUsersandRolesRelation();
            if (usersResponse.isResponseSuccesfull())
            {
                UsuariosYRoles = usersResponse.Response;
            }
            else
            {
                string reporteError = "Relaciones de roles";
                throw new InvalidOperationException($"Error al cargar las {reporteError}.");
            }
        }

        private async Task CargarEscuelas()
        {
            var usersResponse = await _escuelaService.GetAsync();
            if (usersResponse.isResponseSuccesfull())
            {
                Escuelas = usersResponse.Response;
            }
            else
            {
                string reporteError = "Escuelas";
                throw new InvalidOperationException($"Error al cargar las {reporteError}.");
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

        protected void ShowError(string message)
        {
            ShowNotification(message, Severity.Error);
        }

        protected async Task AddAdminRol(UsuarioDTO user)
        {
            var response = await _userService.GiveAdministrationRole(user);
            if (response.isResponseSuccesfull())
            {
                ShowNotification("¡Se añadieron permisos de administrador exitosamente!", Severity.Success);
            }
            else
            {
                ShowNotification("Hubo un error al añadir permisos de administrador.", Severity.Error);
            }
            await CargarRolesYUsuarios();
        }

        protected async Task AddProfessorRol(UsuarioDTO user)
        {
            var response = await _userService.GiveProfessorRole(user);
            if (response.isResponseSuccesfull())
            {
                ShowNotification("¡Se añadieron permisos de profesor exitosamente!", Severity.Success);
            }
            else
            {
                ShowNotification("Hubo un error al añadir permisos de profesor.", Severity.Error);
            }
            await CargarRolesYUsuarios();
        }

        protected async Task RemoveAdminRol(UsuarioDTO user)
        {
            var response = await _userService.RemoveAdministrationRole(user);
            if (response.isResponseSuccesfull())
            {
                ShowNotification("¡Se removieron los permisos de administrador exitosamente!", Severity.Success);
            }
            else
            {
                ShowNotification("Hubo un error al remover los permisos de administrador", Severity.Error);
            }
            await CargarRolesYUsuarios();
        }

        protected async Task RemoveProfessorRol(UsuarioDTO user)
        {
            var response = await _userService.RemoveProfessorRole(user);
            if (response.isResponseSuccesfull())
            {
                ShowNotification("¡Se removieron los permisos de profesor exitosamente!", Severity.Success);
            }
            else
            {
                ShowNotification("Hubo un error al remover los permisos de profesor", Severity.Error);
            }
            await CargarRolesYUsuarios();
        }

        protected async Task CreateSchool(EscuelaModel s)
        {
            var response = await _escuelaService.PostAsync(s);
            if (response.isResponseSuccesfull())
            {
                ShowNotification("¡Se creó la escuela exitosamente!", Severity.Success);
            }
            else
            {
                ShowNotification("Hubo un error al crear la escuela", Severity.Error);
            }
            await CargarEscuelas();
        }
    }
}
