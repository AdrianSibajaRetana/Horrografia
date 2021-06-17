using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Horrografia.Client.Shared.Objects.ClientModels;
using Horrografia.Shared.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace Horrografia.Client.Shared.Components.Dashboard.Level_Creation
{
    public partial class LevelItemTable
    {
        [Parameter]
        public List<ItemModel> ItemsDeNivel { get; set;}

        [Parameter]
        public List<ItemModel> ItemsTotales { get; set;}

        [Parameter]
        public List<PistaModel> PistasTotales { get; set; }

        [Parameter]
        public EventCallback<ItemModel> OnItemCreationRequest { get; set; }

        [Parameter]
        public EventCallback<ItemModel> OnItemUpdateRequest { get; set; }

        [Parameter]
        public EventCallback<ItemModel> OnItemDeletionRequest { get; set; }

        [Parameter]
        public EventCallback<ItemModel> OnRelationDeletionRequest { get; set; }

        [Parameter]
        public EventCallback<PistaModel> OnClueCreationRequest { get; set; }

        [Parameter]
        public EventCallback<int> OnRelationCreationRequest { get; set; }


        [Parameter]
        public int ItemsMaximos { get; set; }

        private bool _showItemCreationDialog { get; set; }
        private bool _showItemEditionDialog { get; set; }
        private bool _showItemDeletionDialog { get; set; }
        private ItemModel _itemSelectedForCrudAction { get; set; }
        private bool _isLoading { get; set; }
        private List<ItemModel> _itemList { get; set; } = new();
        private List<PistaModel> _pistaList { get; set; } = new();
        private int _maxitems { get; set; }

        public LevelItemTable()
        {
            _showItemCreationDialog = false;
            _showItemEditionDialog = false;
            _showItemDeletionDialog = false;
        }


        protected override void OnParametersSet()
        {
            _isLoading = true;
            if (_itemList != ItemsDeNivel)
            {
                _itemList = ItemsDeNivel;
            }
            if (_pistaList != PistasTotales)
            {
                _pistaList = PistasTotales;
            }
            if (_maxitems != ItemsMaximos)
            {
                _maxitems = ItemsMaximos;
            }
            _isLoading = false;
        }

        private string GetPistaById(int id)
        {
            return _pistaList.FirstOrDefault(p => p.Id == id).Pista;
        }

        private bool GetCreationButtonState()
        {
            bool creationButtonDisabled = true;
            if (_itemList.Count < _maxitems)
            {
                creationButtonDisabled = false;
            }            
            return creationButtonDisabled;
        }

        private void openItemCreationDialog()
        {
            _showItemCreationDialog = true;
        }

        private void openItemEditionDialog(ItemModel i)
        {
            _itemSelectedForCrudAction = i;
            _showItemEditionDialog = true;
        }

        private void openItemDeletionDialog(ItemModel i)
        {
            _itemSelectedForCrudAction = i;
            _showItemDeletionDialog = true;
        }

        protected void closeItemCreationDialog()
        {
            _showItemCreationDialog = false;
        }

        protected void closeItemEditionDialog()
        {
            _showItemEditionDialog = false;
        }

        protected void closeItemDeletionDialog()
        {
            _showItemDeletionDialog = false;
        }

        protected async Task ItemCreationRequest(ItemModel i)
        {
            await OnItemCreationRequest.InvokeAsync(i);
        }

        protected async Task ItemEditionRequest(ItemModel i)
        {
            await OnItemUpdateRequest.InvokeAsync(i);
        }

        protected async Task ClueCreationRequest(PistaModel p)
        {
            await OnClueCreationRequest.InvokeAsync(p);
        }

        protected async Task RelationCreationRequest(int id)
        {
            await OnRelationCreationRequest.InvokeAsync(id);
        }
    }
}
