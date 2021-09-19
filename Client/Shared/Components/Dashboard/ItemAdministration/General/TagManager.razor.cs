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
        public List<ItemTagModel> Relations { get; set; }

        [Parameter]
        public EventCallback<TagModel> OnTagCreation { get; set; }

        [Parameter]
        public EventCallback<TagModel> OnTagDeletion { get; set; }

        [Parameter]
        public TagModel TagModelToDelete { get; set; }

        private bool _showTagCreationDialog { get; set; }

        private bool _showTagDeletionDialog { get; set; }

        public TagManager()
        {
            TagModelToDelete = new(); 
            _showTagCreationDialog = false;
        }

        private int getRelationNumber(int tagId)
        {
            return Relations.Where(r => r.idTag == tagId).Count();
        }

        private void OpenTagCreationDialog()
        {
            _showTagCreationDialog = true;
        }

        protected void CloseTagCreationDialog()
        {
            _showTagCreationDialog = false;
        }

        private void OpenDeletionDialog(TagModel t)
        {
            TagModelToDelete = t; 
            _showTagDeletionDialog = true;
        }

        protected void CloseDeletionDialog()
        {
            _showTagDeletionDialog = false; 
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
