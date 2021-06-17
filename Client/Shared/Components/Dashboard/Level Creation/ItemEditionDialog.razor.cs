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
        public List<PistaModel> PistasTotales { get; set; }

        [Parameter]
        public EventCallback OnDialogClosed { get; set; }

        [Parameter]
        public EventCallback<ItemModel> OnItemUpdate { get; set; }

        [Parameter]
        public EventCallback<PistaModel> OnPistaUpdate { get; set; }

        private ClientItemModel _newModel { get; set; }
        private ItemModel _model { get; set; }

        public ItemEditionDialog()
        {
            _newModel = new();
            _model = new();
        }

        protected override void OnParametersSet()
        {
            if (_model != ItemToChange && ItemToChange != null)
            {
                _model = ItemToChange;
                generateClientModel();
            }
        }

        private void generateClientModel()
        {
            _newModel.SetDataFromModel(_model);
            _newModel.Pista = GetPistaById(_newModel.Pistaid);
        }

        private string GetPistaById(int id)
        {
            return PistasTotales.FirstOrDefault(p => p.Id == id).Pista;
        }

        /*Función de búsqueda en el autocompletar de Pistas*/
        private async Task<IEnumerable<string>> BuscarPistasExistentes(string value)
        {
            // Retraso sutil para la interfaz de creación. 
            await Task.Delay(5);

            // Si la persona no ha puesto un input, no muestre nada
            if (string.IsNullOrEmpty(value))
            {
                return Array.Empty<string>();
            }

            var ListaFriltrada = PistasTotales.Where(p => p.Pista.Contains(value, StringComparison.InvariantCultureIgnoreCase));
            var PistasFiltradas = ListaFriltrada.Select(p => p.Pista).Distinct().ToList();
            return PistasFiltradas;
        }

        public async Task CloseDialog()
        {
            await OnDialogClosed.InvokeAsync();
        }

        public async Task UpdateItem()
        {
            ItemModel updatedItemModel = _newModel.GetItemModel();
            await OnItemUpdate.InvokeAsync(updatedItemModel);
            await OnDialogClosed.InvokeAsync();
        }

    }
}
