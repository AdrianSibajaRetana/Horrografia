using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Horrografia.Client.Shared.Components.Dashboard.Level_Creation
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
        public EventCallback OnLevelCreation { get; set; }

        public async Task CloseDialog()
        {
            await OnDialogClosed.InvokeAsync();
        }

        public async Task CreateLevel()
        {
            await OnLevelCreation.InvokeAsync();
            CloseDialog();
        }
    }
}
