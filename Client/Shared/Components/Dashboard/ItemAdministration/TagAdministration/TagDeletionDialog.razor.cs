using Horrografia.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Horrografia.Client.Shared.Components.Dashboard.ItemAdministration.TagAdministration
{
    public partial class TagDeletionDialog
    {
        [Parameter]
        public bool ShowDialogue { get; set; }

        [Parameter]
        public EventCallback OnDialogClosed { get; set; }

        [Parameter]
        public EventCallback<TagModel> OnTagDeletion { get; set; }

        [Parameter]
        public TagModel tagToDelete { get; set; }

        private async Task CloseDialog()
        {
            await OnDialogClosed.InvokeAsync();
        }

        private async Task DeleteTag()
        {
            await OnTagDeletion.InvokeAsync(tagToDelete);
            await CloseDialog();
        }
    }
}
