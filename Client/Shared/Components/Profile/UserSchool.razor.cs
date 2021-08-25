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

        private bool _isLoading { get; set; }

        private bool _showSchoolNotFound { get; set; }


        public UserSchool()
        {
            _showSchoolNotFound = false;
            _isLoading = false; 
        }

        private async Task RegisterUserToSchool()
        {
            _isLoading = true;
            _showSchoolNotFound = false;
            var verificacion = new ClientEscuelaVerificationModel(_codigoEscuelaIntroducido);
            await OnSchoolVerification.InvokeAsync(verificacion);
            if (verificacion.Exists)
            {
                
            }
            else
            {
                _showSchoolNotFound = true; 
            }
            _isLoading = false;
        }
    }
}
