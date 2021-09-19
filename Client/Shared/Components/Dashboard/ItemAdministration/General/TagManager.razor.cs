using System;
using System.Collections.Generic;
using System.Linq;
using Horrografia.Client.Shared.Objects.ClientModels;
using Horrografia.Shared.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Horrografia.Client.Shared.Components.Dashboard.ItemAdministration.General
{
    public partial class TagManager
    {
        [Parameter]
        public List<TagModel> Tags { get; set; }

        [Parameter]
        public EventCallback<TagModel> OnTagCreation { get; set; }

        [Parameter]
        public EventCallback<TagModel> OnTagDeletion { get; set; }

        private bool _showTagCreationDialog { get; set; }

        public TagManager()
        {
            _showTagCreationDialog = false; 
        }

        private void OpenTagCreationDialog()
        {
            _showTagCreationDialog = true; 
        }

        protected void CloseTagCreationDialog()
        {
            _showTagCreationDialog = false; 
        }

        protected async Task CreateTag(TagModel t)
        {
            await OnTagCreation.InvokeAsync(t);
        }

        protected async Task DeleteTag(TagModel t)
        {
            await OnTagDeletion.InvokeAsync(t);
        }
    }
}
