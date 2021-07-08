using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Horrografia.Client.Shared.Objects.ClientModels;
using Horrografia.Shared.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace Horrografia.Client.Shared.Components.Dashboard.Level_Creation.Level_Administration
{
    public partial class LevelCreationDialog
    {
        [Parameter]
        public bool ShowDialogue { get; set; }

        [Parameter]
        public bool ExistenNiveles { get; set; }

        [Parameter]
        public EventCallback OnDialogClosed { get; set; }

        [Parameter]
        public EventCallback<NivelModel> OnLevelCreation { get; set; }

        public ClientNivelModel model { get; set; }

        public LevelCreationDialog()
        {
            model = new();
        }
        public async Task CloseDialog()
        {
            await OnDialogClosed.InvokeAsync();
        }

        public async Task CreateLevel(EditContext context)
        {
            NivelModel _nivelModel = model.CreateNivelModel();
            model = new(); 
            await OnLevelCreation.InvokeAsync(_nivelModel);
        }

    }
}
