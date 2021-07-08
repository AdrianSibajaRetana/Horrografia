using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Horrografia.Client.Shared.Objects.ClientModels;
using Horrografia.Shared.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace Horrografia.Client.Shared.Components.Dashboard.Level_Creation.Item_Administration
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
        public List<PistaModel> PistasTotales { get; set; }

        [Parameter]
        public List<FormaIncorrectaModel> FormasIncorrectasTotales { get; set; } 

        private ClientItemModel _model { get; set; }

        private int _selectedOption { get; set; } = 0;

        private bool _isDeletingItem { get; set; }

        private string _deletingStatus { get; set; }

        public ItemDeletionDialog()
        {
            _model = new();
            _isDeletingItem = false;
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
            _model.FormasIncorrectas = GetFormasIncorrectas();
            _model.Pista = GetPistaById(_model.Pistaid);
        }

        private string GetPistaById(int? id)
        {
            string pista = "Item sin pista";
            if (id.HasValue)
            {
                pista = PistasTotales.FirstOrDefault(p => p.Id == id).Pista;
            }
            return pista;
        }

        private List<string> GetFormasIncorrectas()
        {
            var formas = FormasIncorrectasTotales.Where(f => f.Itemid == _model.Id).Select(f => f.Forma).ToList();
            return formas; 
        }

        private async Task CloseDialog()
        {
            await OnDialogClosed.InvokeAsync();
        }

        private async Task DeleteItem()
        {
            _isDeletingItem = true;
            _deletingStatus = "Borrando el item del nivel.";
            await OnItemDeletion.InvokeAsync(itemToChange);
            _isDeletingItem = false;
            await CloseDialog();            
        }

    }
}
