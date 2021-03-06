using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Horrografia.Client.Shared.Objects.ClientModels;
using Horrografia.Shared.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace Horrografia.Client.Shared.Components.Dashboard.Level_Creation.Level_General
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
        public List<FormaIncorrectaModel> FormasIncorrectas { get; set; }

        [Parameter]
        public EventCallback<ItemModel> OnItemCreationRequest { get; set; }

        [Parameter]
        public EventCallback<ItemModel> OnItemUpdateRequest { get; set; }

        [Parameter]
        public EventCallback<ItemModel> OnItemDeletionRequest { get; set; }

        [Parameter]
        public EventCallback<PistaModel> OnClueCreationRequest { get; set; }

        [Parameter]
        public EventCallback<List<FormaIncorrectaModel>> OnIncorrectFormCreationRequest { get; set; }

        [Parameter]
        public EventCallback<List<FormaIncorrectaModel>> OnIncorrectFormDeletionnRequest { get; set; }

        [Parameter]
        public EventCallback NotifyError { get; set; }

        [Parameter]
        public int ItemsMaximos { get; set; }

        private bool _showItemCreationDialog { get; set; }
        private bool _showItemEditionDialog { get; set; }
        private bool _showItemDeletionDialog { get; set; }
        private ItemModel _itemSelectedForCrudAction { get; set; }
        private bool _isLoading { get; set; }
        private List<ItemModel> _itemList { get; set; } = new();
        private List<PistaModel> _pistaList { get; set; } = new();
        private List<FormaIncorrectaModel> _formaIncorrectaList { get; set; } = new();
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
            if (_formaIncorrectaList != FormasIncorrectas)
            {
                _formaIncorrectaList = FormasIncorrectas;
            }
            _isLoading = false;
        }

        private string GetFormasIncorrectasById(int id)
        {            
            var formasList = _formaIncorrectaList.Where(f => f.Itemid == id).ToList().Select(f => f.Forma);
            var formasString = string.Join(", ", formasList);
            return formasString;
        }

        private string GetPistaById(int? id)
        {
            var pistaString = "";
            if (id.HasValue)
            { 
                pistaString = _pistaList.FirstOrDefault(p => p.Id == id).Pista;
            }
            return pistaString;
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

        protected async Task ItemDeletionRequest(ItemModel i)
        {
            await OnItemDeletionRequest.InvokeAsync(i);
        }

        protected async Task ClueCreationRequest(PistaModel p)
        {
            await OnClueCreationRequest.InvokeAsync(p);
        }

        protected async Task IncorrectFormCreationRequest(List<FormaIncorrectaModel> f)
        {
            await OnIncorrectFormCreationRequest.InvokeAsync(f);
        }

        protected async Task IncorrectFormDeletionRequest(List<FormaIncorrectaModel> f)
        {
            await OnIncorrectFormDeletionnRequest.InvokeAsync(f);
        }

        protected async Task NotifyErrorToPage()
        {
            await NotifyError.InvokeAsync();
        }
    }
}
