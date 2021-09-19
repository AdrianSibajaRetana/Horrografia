using Horrografia.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Horrografia.Client.Shared.Objects.ClientModels;

namespace Horrografia.Client.Shared.Components.Dashboard.ItemAdministration.TagAdministration
{
    public partial class TagCreationDialog
    {
        [Parameter]
        public bool ShowDialogue { get; set; }

        [Parameter]
        public EventCallback OnDialogClosed { get; set; }

        [Parameter]
        public EventCallback<TagModel> OnTagCreation { get; set; }

        private ClientTagModel _model { get; set; }

        public TagCreationDialog()
        {
            _model = new();
        }

        private async Task CloseDialog()
        {
            _model = new(); 
            await OnDialogClosed.InvokeAsync();
        }

        private async Task CreateTag()
        {
            TagModel t = _model.generarTagModel();
            await OnTagCreation.InvokeAsync(t);
            await CloseDialog(); 
        }
    }
}
