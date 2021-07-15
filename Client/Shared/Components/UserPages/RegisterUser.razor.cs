using Horrografia.Shared;
using Horrografia.Client.Data.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Horrografia.Client.Shared.Objects;
using Horrografia.Client.Shared.Constants;
using Horrografia.Client.Shared.Objects.ClientModels;
using MudBlazor;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace Horrografia.Client.Shared.Components.UserPages
{
    public partial class RegisterUser
    {
        [Inject]
        protected IUserService _userService { get; set; }

        [Inject]
        protected NavigationManager _navManager { get; set; }

        [Inject]
        protected ISnackbar _snackbar { get; set; }

        private ClientUserRegisterDTO _registerModel { get; set; }

        private bool _isCreatingUser { get; set; }
        private bool _isRegistrationSuccesfull { get; set; }
        private string _errorMessage { get; set; }

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
            _isRegistrationSuccesfull = true;

            _isPasswordShown = false;
            PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
            PasswordInput = InputType.Password;

            _isConfirmedPasswordShown = false;
            ConfirmedPasswordInputIcon = Icons.Material.Filled.VisibilityOff;
            ConfirmedPasswordInput = InputType.Password;
        }

        protected override void OnParametersSet()
        {
            _snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopCenter;
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
            try
            {
                _isCreatingUser = true;
                bool isEmailAvailabe = await VerifyEmail();
                if (isEmailAvailabe)
                {
                    bool isCreationSuccesfull = await CreateUser();
                    if (isCreationSuccesfull)
                    {
                        _navManager.NavigateTo("/", true);
                    }
                    else
                    {
                        _isRegistrationSuccesfull = false;
                        _errorMessage = "Error inesperado encontrado a la hora de crear el usuario.";
                    }
                }
                else
                {
                    _isRegistrationSuccesfull = false;
                    _errorMessage = "El correo ya se encuentra registrado.";
                }
                _isCreatingUser = false;
            }
            catch
            {
                _isCreatingUser = false;
                _snackbar.Add("Error al comunicarse con al base de datos", Severity.Error);
            }
        }

        private async Task<bool> VerifyEmail()
        {
            bool isEmailAvailable;
            var response = await _userService.VerifyEmail(_registerModel);
            if (response.isResponseSuccesfull())
            {
                isEmailAvailable = response.Response.FirstOrDefault();
            }
            else
            {
                throw new InvalidOperationException();
            }
            return isEmailAvailable;
        }

        private async Task<bool> CreateUser()
        {
            bool isCreationSuccesfull;
            var response = await _userService.Register(_registerModel);
            if (response.isResponseSuccesfull())
            {
                isCreationSuccesfull = response.Response.FirstOrDefault();
            }
            else
            {
                throw new InvalidOperationException();
            }
            return isCreationSuccesfull;
        }
    }
}
