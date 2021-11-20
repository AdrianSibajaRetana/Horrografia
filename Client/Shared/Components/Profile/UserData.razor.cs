using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Horrografia.Client.Shared.Objects.ClientModels;
using Horrografia.Shared.Models;

namespace Horrografia.Client.Shared.Components.Profile
{
    public partial class UserData
    {
        [Parameter]
        public UsuarioDTO User { get; set; }
        
        [Parameter]
        public int NumberOfGames { get; set; }
        
        [Parameter]
        public EscuelaModel UserSchool { get; set; }
        
        [Parameter]
        public bool IsUserInSchool { get; set; }
    }
}
