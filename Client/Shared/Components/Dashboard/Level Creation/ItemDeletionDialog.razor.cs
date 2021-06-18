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
    public partial class ItemDeletionDialog
    {
        [Parameter]
        public bool ShowDialogue { get; set; }

        [Parameter]
        public ItemModel itemToChange { get; set; }

        [Parameter]
        public EventCallback OnDialogClosed { get; set; }

        [Parameter]
        public EventCallback<ItemModel> OnItemDeletion { get; set; }

        [Parameter]
        public EventCallback<ItemModel> OnRelationDeletion { get; set; }

        [Parameter]
        public List<PistaModel> PistasTotales { get; set; }

        private ClientItemModel _model { get; set; }
        private int _selectedOption { get; set; } = 0;

        public ItemDeletionDialog()
        {
            _model = new();
        }

        protected override void OnParametersSet()
        {
            if (itemToChange != null)
            {
                generateClientModel();
            }
        }

        private void generateClientModel()
        {
            _model.SetDataFromModel(itemToChange);
            _model.Pista = GetPistaById(_model.Pistaid);
        }

        private string GetPistaById(int id)
        {
            return PistasTotales.FirstOrDefault(p => p.Id == id).Pista;
        }

        private async Task CloseDialog()
        {
            await OnDialogClosed.InvokeAsync();
        }

        private async Task DeleteItem()
        {
            //Borrar el item del nivel seleccionado.
            if (_selectedOption == 0)
            {
                await OnRelationDeletion.InvokeAsync(itemToChange);
                await CloseDialog();
            }
            //Borrar el item de todos los niveles.
            else
            {
                await OnItemDeletion.InvokeAsync(itemToChange);
                await CloseDialog();
            }
        }

    }
}
