using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Horrografia.Client.Shared.Objects.ClientModels;

namespace Horrografia.Client.Shared.Components.UserPages
{
    public partial class LoginUser
    {
        private ClientUserLoginDTO _loginModel { get; set; }

        public LoginUser()
        {
            _loginModel = new();
            _loginModel.RememberMe = false;
        }

        private async Task ProceedToLogin()
        {

        }

    }
}
