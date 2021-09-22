using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Horrografia.Client.Shared.Objects.ClientModels;
using Horrografia.Shared.Models;

namespace Horrografia.Client.Shared.Components.Profile
{
    public partial class UserSchool
    {
        private string _codigoEscuelaIntroducido { get; set; }

        [Parameter]
        public EventCallback<ClientEscuelaVerificationModel> OnSchoolVerification { get; set; }

        [Parameter]
        public EventCallback<string> OnUserSchoolRegistration{ get; set; }


        private bool _showSchoolNotFound { get; set; }

        private string _buttonLoading { get; set; }

        public UserSchool()
        {
            _showSchoolNotFound = false;
            _buttonLoading = null; 
        }

        private async Task RegisterUserToSchool()
        {
            _buttonLoading = "is-loading";
            _showSchoolNotFound = false;
            var verificacion = new ClientEscuelaVerificationModel(_codigoEscuelaIntroducido);
            await OnSchoolVerification.InvokeAsync(verificacion);
            if (verificacion.Exists)
            {
                await OnUserSchoolRegistration.InvokeAsync(_codigoEscuelaIntroducido);
            }
            else
            {
                _showSchoolNotFound = true; 
            }
            _buttonLoading = null;
        }
    }
}
