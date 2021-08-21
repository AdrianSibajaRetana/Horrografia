using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Horrografia.Client.Pages
{


    public partial class Perfil
    {
        [Inject]
        protected AuthenticationStateProvider authProvider { get; set; }

        private bool _isLoading { get; set; }
        private bool _lecturaExitosa { get; set; }
        private string id { get; set; }
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
            AuthenticationState authState = await authProvider.GetAuthenticationStateAsync();
            var userId = authState.User.Claims.FirstOrDefault(c => c.Type == "sub").Value;
            id = userId;
        }
    }
}
