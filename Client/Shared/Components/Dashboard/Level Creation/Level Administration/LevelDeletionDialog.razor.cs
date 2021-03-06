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
    public partial class LevelDeletionDialog
    {
        [Parameter]
        public bool ShowDialogue { get; set; }

        [Parameter]
        public EventCallback OnDialogClosed { get; set; }

        [Parameter]
        public EventCallback<NivelModel> OnLevelDeletion { get; set; }

        [Parameter]
        public NivelModel model { get; set; }

        public async Task CloseDialog()
        {
            await OnDialogClosed.InvokeAsync();
        }

        
        public async Task DeleteLevel()
        {
            await OnLevelDeletion.InvokeAsync(model);
        }

    }
}
