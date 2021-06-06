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
    public partial class ItemCreationDialog
    {
        [Parameter]
        public bool ShowDialogue { get; set; }

        [Parameter]
        public List<ItemModel> ItemsTotales{ get; set;}

        [Parameter]
        public EventCallback OnDialogClosed { get; set; }

        [Parameter]
        public EventCallback<NivelModel> OnLevelCreation { get; set; }

        private ClientItemModel model { get; set; }

        public ItemCreationDialog()
        {
            model = new();
        }

        private async Task<IEnumerable<string>> BuscarItemsExistentes(string value)
        {
            // Retraso sutil para la interfaz de creación. 
            await Task.Delay(5);

            // Si la persona no ha puesto un input, devuelva toda la lista de los nombres.
            if (string.IsNullOrEmpty(value))
            { 
                var allItems = ItemsTotales.Select(i => i.FormaCorrecta).ToList();
                return allItems;
            }

            var filteredList = ItemsTotales.Where(i => i.FormaCorrecta.Contains(value, StringComparison.InvariantCultureIgnoreCase));
            var filteredItems = filteredList.Select(i => i.FormaCorrecta).ToList();
            return filteredItems;
        }

        private async Task CreateItem()
        {
            await OnDialogClosed.InvokeAsync();
        }

        public async Task CloseDialog()
        {
            await OnDialogClosed.InvokeAsync();
        }
    }
}
