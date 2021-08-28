using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Horrografia.Client.Shared.Objects.ClientModels;
using Horrografia.Shared.Models;

namespace Horrografia.Client.Shared.Components.Profile
{
    public partial class SchoolData
    {
        [Parameter]
        public EscuelaModel Escuela { get; set; }

    }
}
