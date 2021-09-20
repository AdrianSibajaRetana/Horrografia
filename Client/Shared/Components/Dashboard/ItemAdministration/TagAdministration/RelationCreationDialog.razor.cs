using Horrografia.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Horrografia.Client.Shared.Components.Dashboard.ItemAdministration.TagAdministration
{
    public partial class RelationCreationDialog
    {
        [Parameter]
        public bool ShowDialog { get; set; }

        [Parameter]
        public ItemModel ItemReceived { get; set; }

        [Parameter]
        public List<TagModel> Tags { get; set; }

        [Parameter]
        public EventCallback<ItemTagModel> OnRelationCreation { get; set; }

        [Parameter]
        public EventCallback OnDialogClose { get; set; }

        private TagModel _model { get; set; }

        public RelationCreationDialog()
        {
            _model = new(); 
        }

        /*Función de búsqueda en el autocompletar de Formas Correctas*/
        private async Task<IEnumerable<string>> BuscarTagsExistentes(string value)
        {
            // Retraso sutil para la interfaz de creación. 
            await Task.Delay(5);

            // Si la persona no ha puesto un input, devuelva todos los tags
            if (string.IsNullOrEmpty(value))
            {
                return Tags.Select(t => t.Tag).ToList(); 
            }

            var filteredList = Tags.Where(i => i.Tag.Contains(value, StringComparison.InvariantCultureIgnoreCase));
            var filteredItems = filteredList.Select(i => i.Tag).Distinct().ToList();
            return filteredItems;
        }

        private async Task CloseDialog()
        {
            _model = new(); 
            await OnDialogClose.InvokeAsync(); 
        }

        private async Task CreateRelation()
        {
            if (!string.IsNullOrEmpty(_model.Tag))
            { 
                var selectedTag = Tags.Where(t => t.Tag == _model.Tag).FirstOrDefault();
                ItemTagModel relationToCreate = new();
                relationToCreate.idTag = selectedTag.id;
                relationToCreate.idItem = ItemReceived.Id;
                await OnRelationCreation.InvokeAsync(relationToCreate);
                await CloseDialog();             
            }
        }

    }
}
