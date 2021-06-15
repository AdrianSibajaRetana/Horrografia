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
    public partial class ItemEditionDialog
    {
        [Parameter]
        public bool ShowDialogue { get; set; }

        [Parameter]
        public ItemModel ItemToChange { get; set; }

        [Parameter]
        public EventCallback OnDialogClosed { get; set; }

        [Parameter]
        public EventCallback<ItemModel> OnItemUpdate { get; set; }

        [Parameter]
        public EventCallback<PistaModel> OnPistaUpdate { get; set; }

        [Parameter]
        public string PistaOnString { get; set; }

        [Parameter]
        public List<PistaModel> Pistas { get; set; }

        private ClientItemModel _newModel { get; set; }
        private ItemModel _model { get; set; }
        private string _pistaString { get; set; }

        public ItemEditionDialog()
        {
            _model = new();
            _newModel = new();
        }

        protected override void OnParametersSet()
        {
            if (_pistaString != PistaOnString)
            {
                _pistaString = PistaOnString;
            }
            if (_model != ItemToChange)
            {
                _model = ItemToChange;
                if (_model != null)
                {
                    _newModel.SetDataFromModel(_model);
                    _newModel.Pista = _pistaString;
                }
            }
        }

        public async Task CloseDialog()
        {
            await OnDialogClosed.InvokeAsync();
        }

        public async Task UpdateItem()
        {
            ItemModel updatedItemModel = _newModel.GetItemModel();
            await OnItemUpdate.InvokeAsync(updatedItemModel);
        }

    }
}
