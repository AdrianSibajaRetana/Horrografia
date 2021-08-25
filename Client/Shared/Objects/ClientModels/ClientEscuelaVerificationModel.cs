using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Horrografia.Client.Shared.Objects.ClientModels
{
    public class ClientEscuelaVerificationModel
    {
        public string Schoolcode { get; set; }
        public bool Exists { get; set; }

        public ClientEscuelaVerificationModel(string code)
        {
            Schoolcode = code;
            Exists = false; 
        }
    }
}
