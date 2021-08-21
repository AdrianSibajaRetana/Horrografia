using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Horrografia.Shared.Models;
using Horrografia.Client.Data.Services.Interfaces;

namespace Horrografia.Client.Pages
{


    public partial class Perfil
    {
        [Inject]
        protected AuthenticationStateProvider authProvider { get; set; }

        [Inject]
        protected IUserService _userService { get; set; }

        private bool _isLoading { get; set; }
        private bool _lecturaExitosa { get; set; }
        private UsuarioDTO User { get; set; }

        public Perfil()
        {
            _isLoading = true;
            _lecturaExitosa = false;
        }

        protected override async Task OnInitializedAsync()
        {
            await CargarData();
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
                _lecturaExitosa = true;
            }
        }
    }
}
