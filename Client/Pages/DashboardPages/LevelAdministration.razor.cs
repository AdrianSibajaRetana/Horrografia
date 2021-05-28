using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Horrografia.Shared.Models;
using Horrografia.Client.Data.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Horrografia.Client.Shared.Objects;


namespace Horrografia.Client.Pages.DashboardPages
{
    public partial class LevelAdministration : IComponent
    {
        [Inject]
        protected INivelService _nivelService { get; set; }
        public List<NivelModel> Niveles { get; set; }
        public bool IsLoading { get; set; }
        public int InitialLoadStatus { get; set; }

        public LevelAdministration()
        {
            IsLoading = true;
        }

        protected override async Task OnInitializedAsync()
        {
            var response = await _nivelService.GetAsync();
            InitialLoadStatus = response.Status;
            Niveles = response.Response;
            IsLoading = false; 
        }
    }
}
