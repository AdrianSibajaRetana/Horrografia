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
        protected ISnackbar _snackbar { get; set; }

        private bool _isLoading { get; set; }
        private bool _lecturaExitosa { get; set; }
        private bool _isUserInSchool { get; set; }
        private UsuarioDTO User { get; set; }
        private EscuelaModel UserSchool { get; set; }

        public Perfil()
        {
            _isLoading = true;
            _lecturaExitosa = false;
            _isUserInSchool = false;
            UserSchool = new();
        }

        protected override async Task OnInitializedAsync()
        {
            await CargarData();
            if (_isUserInSchool)
            {
                await CargarDataDeEscuela();
            }
            _isLoading = false;
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
                ShowNotification("Error al cargar datos de escuela.", Severity.Error);
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
