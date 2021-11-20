using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Horrografia.Shared.Models;
using Horrografia.Client.Data.Services.Interfaces;
using Horrografia.Client.Shared.Objects.ClientModels;
using MudBlazor;

namespace Horrografia.Client.Pages
{


    public partial class Perfil
    {
        [Inject]
        protected AuthenticationStateProvider authProvider { get; set; }

        [Inject]
        protected IUserService _userService { get; set; }

        [Inject]
        protected IEscuelaService _escuelaService { get; set; }
        
        [Inject]
        protected IReporteService _reporteService { get; set; }

        
        [Inject]
        protected ISnackbar _snackbar { get; set; }

        private bool _isLoading { get; set; }
        private bool _lecturaExitosa { get; set; }
        private bool _isUserInSchool { get; set; }
        private UsuarioDTO User { get; set; }
        private EscuelaModel UserSchool { get; set; }
        private List<string> Estudiantes { get; set; }
        private List<string> Profesores { get; set; }

        private List<ReporteModel> PartidasJugadas { get; set; }

        //Para el manejo de tabs
        private enum ActiveTabState
        {
            ShowGeneralTab,
            ShowHistoryTab,
            ShowSchoolTab
        }

        private string _generalActiveString { get; set; }
        private string _historyActiveString { get; set; }
        private string _schoolActiveString { get; set; }
        private ActiveTabState _activeTab { get; set; }
        private const string ShowActiveState = "is-active";

        public Perfil()
        {
            _isLoading = true;
            _lecturaExitosa = false;
            _isUserInSchool = false;
            UserSchool = new();
            PartidasJugadas = new();
            _activeTab = ActiveTabState.ShowGeneralTab;
            _generalActiveString = ShowActiveState;
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
                        _historyActiveString = null;
                        _schoolActiveString = null;
                        break;
                    case ActiveTabState.ShowHistoryTab:
                        _generalActiveString = null;
                        _historyActiveString = ShowActiveState;
                        _schoolActiveString = null;
                        break;
                    case ActiveTabState.ShowSchoolTab:
                        _generalActiveString = null;
                        _historyActiveString = null;
                        _schoolActiveString = ShowActiveState;
                        break;
                }
            }
        }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                await CargarData();
                await CargarHistorialDePartidas();
                if (_isUserInSchool)
                {
                    await CargarDataDeEscuela();
                }
                _isLoading = false;
            }
            catch (Exception e)
            {
                ShowNotification(e.Message, Severity.Error);
            }
        }

        private async Task CargarData()
        {
            //Consigo el id del usuario.
            AuthenticationState authState = await authProvider.GetAuthenticationStateAsync();
            var userId = authState.User.Claims.FirstOrDefault(c => c.Type == "sub").Value;

            //Con el id consigo el resto de datos del usuario
            var response = await _userService.GetUserById(userId);
            if (response.isResponseSuccesfull())
            {
                User = response.Response.FirstOrDefault();
                _isUserInSchool = User.IsUserinSchool();
                _lecturaExitosa = true;
                _snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopCenter;
            }
            else
            {
                throw new InvalidOperationException($"Error al datos de usuario.");
            }
        }

        private async Task CargarHistorialDePartidas()
        {
            var response = await _reporteService.GetUserReportsById(User.id);
            if (response.isResponseSuccesfull())
            {
                PartidasJugadas = response.Response;
            }
            else
            {
                throw new InvalidOperationException($"Error al cargar partidas.");
            }
        }

        private async Task CargarDataDeEscuela()
        {
            var response = await _escuelaService.GetByCode(User.codigoEscuela);
            if (response.isResponseSuccesfull())
            {
                UserSchool = response.Response.FirstOrDefault();
            }
            else
            {
                throw new InvalidOperationException($"Error al cargar datos de escuela.");
            }

            var response2 = await _userService.GetSchoolStudents(User.codigoEscuela);
            if (response2.isResponseSuccesfull())
            {
                Estudiantes = response2.Response;
            }
            else
            {
                throw new InvalidOperationException($"Error al cargar datos de escuela.");
            }

            var response3 = await _userService.GetSchoolProfessors(User.codigoEscuela);
            if (response3.isResponseSuccesfull())
            {
                Profesores = response3.Response;
            }
            else
            {
                throw new InvalidOperationException($"Error al cargar datos de escuela.");
            }
        }

        public void ShowNotification(string mensaje, Severity s)
        {
            //Muestra la notifiación
            _snackbar.Add(mensaje, s);
        }

        protected async Task SearchSchoolExistence(ClientEscuelaVerificationModel escuela)
        {
            var response = await _escuelaService.VerificarExistenciaDeEscuela(escuela.Schoolcode);
            if (response.isResponseSuccesfull())
            {
                escuela.Exists = response.Response.FirstOrDefault();
            }
            else
            {
                ShowNotification("Error al verificar existencia de escuela", Severity.Error);
            }
        }

        protected async Task RegisterUserToSchool(string escuelaId)
        {
            User.codigoEscuela = escuelaId;
            var response = await _userService.UpdateUser(User);
            if (response.isResponseSuccesfull())
            {
                ShowNotification($"¡Se registró exitosamente a la escuela!", Severity.Success);
                _isUserInSchool = true; 
            }
            else
            {
                ShowNotification("Hubo un error al registrarse a la escuela", Severity.Error);
            }
            await CargarDataDeEscuela();
        }
    }
}
