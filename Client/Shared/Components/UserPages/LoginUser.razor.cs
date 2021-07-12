using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Horrografia.Shared.Models;
using Horrografia.Shared;
using Horrografia.Client.Data.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Horrografia.Client.Shared.Objects;
using Horrografia.Client.Shared.Constants;
using Horrografia.Client.Shared.Objects.ClientModels;
using MudBlazor;

namespace Horrografia.Client.Shared.Components.UserPages
{
    public partial class LoginUser
    {
        [Inject]
        protected IUserService _userService { get; set; }

        [Inject]
        protected NavigationManager _navManager { get; set; }

        [Inject]
        protected ISnackbar _snackbar { get; set; }

        private ClientUserLoginDTO _loginModel { get; set; }
        private bool _failedLogin { get; set; }
        private bool _isLoginIn { get; set; }
        public LoginUser()
        {
            _loginModel = new();
            _loginModel.RememberMe = false;
            _failedLogin = false;
            _isLoginIn = false;
        }

        protected override void OnParametersSet()
        {
            _snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopCenter;
        }

        private async Task ProceedToLogin()
        {
            _isLoginIn = true;
            var response = await _userService.Login(_loginModel);
            _isLoginIn = false;
            if (response.isResponseSuccesfull())
            {
                if (response.Response.FirstOrDefault() == SharedConstants.LoginState.LoginSucess)
                {
                    _snackbar.Add("Se ha iniciado sesión exitosamente", Severity.Success);                    
                    _navManager.NavigateTo("/", true);
                }
                else
                {
                    _failedLogin = true;
                }
            }
            else
            {
                _snackbar.Add("Error al comunicarse con al base de datos", Severity.Error);
            }
            _isLoginIn = false;
        }
    }
}
