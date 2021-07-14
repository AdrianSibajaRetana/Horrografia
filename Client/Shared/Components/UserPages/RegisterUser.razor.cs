using Horrografia.Shared;
using Horrografia.Client.Data.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Horrografia.Client.Shared.Objects;
using Horrografia.Client.Shared.Constants;
using Horrografia.Client.Shared.Objects.ClientModels;
using MudBlazor;
using System.Threading.Tasks;

namespace Horrografia.Client.Shared.Components.UserPages
{
    public partial class RegisterUser
    {
        [Inject]
        protected IUserService _userService { get; set; }

        [Inject]
        protected NavigationManager _navManager { get; set; }

        private ClientUserRegisterDTO _registerModel { get; set; }

        private bool _isCreatingUser { get; set; }

        private bool _isPasswordShown { get; set; }
        private string PasswordInputIcon { get; set; }
        private InputType PasswordInput { get; set; }

        private bool _isConfirmedPasswordShown { get; set; }
        private string ConfirmedPasswordInputIcon { get; set; }
        private InputType ConfirmedPasswordInput { get; set; }

        public RegisterUser()
        {
            _registerModel = new();

            _isCreatingUser = false;

            _isPasswordShown = false;
            PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
            PasswordInput = InputType.Password;

            _isConfirmedPasswordShown = false;
            ConfirmedPasswordInputIcon = Icons.Material.Filled.VisibilityOff;
            ConfirmedPasswordInput = InputType.Password;
        }

        private void ButtonTestclick()
        {
            if (_isPasswordShown)
            {
                _isPasswordShown = false;
                PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
                PasswordInput = InputType.Password;
            }
            else
            {
                _isPasswordShown = true;
                PasswordInputIcon = Icons.Material.Filled.Visibility;
                PasswordInput = InputType.Text;
            }
        }

        private void ButtonTestclickConfirmedPassword()
        {
            if (_isConfirmedPasswordShown)
            {
                _isConfirmedPasswordShown = false;
                ConfirmedPasswordInputIcon = Icons.Material.Filled.VisibilityOff;
                ConfirmedPasswordInput = InputType.Password;
            }
            else
            {
                _isConfirmedPasswordShown = true;
                ConfirmedPasswordInputIcon = Icons.Material.Filled.Visibility;
                ConfirmedPasswordInput = InputType.Text;
            }
        }

        private async Task proceedToRegistration()
        { 
        
        }
    }
}
