using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horrografia.Shared
{
    public static class SharedConstants
    {
        public enum LoginState
        {
            EmailNotConfirmed,
            NoPasswordMatch,
            LoginSucess,
            LoginFailure,
        }

        public static List<string> Administradores = new List<string>
            { 
                "Adrian@admin.com",
                "Antonio@admin.com"
            };
    }
}
