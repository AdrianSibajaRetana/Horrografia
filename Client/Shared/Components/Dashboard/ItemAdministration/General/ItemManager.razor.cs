using Horrografia.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Horrografia.Client.Shared.Components.Dashboard.ItemAdministration.General
{
    public partial class ItemManager
    {
        [Parameter]
        public List<NivelModel> Niveles { get; set; }

        [Parameter]
        public List<ItemModel> Items { get; set; }

        [Parameter]
        public List<TagModel> Tags { get; set; }

        [Parameter]
        public List<ItemTagModel> Relaciones { get; set; }

        [Parameter]
        public EventCallback<ItemTagModel> OnRelationCreation { get; set; }

        [Parameter]
        public EventCallback<ItemTagModel> OnRelationDeletion { get; set; }

        private List<ItemModel> _itemsDeNivel { get; set; }

        private int _IdNivelEscogido { get; set; }

        private int _IdEnSeleccionDePagina
        { 
            get => _IdNivelEscogido;
            set
            {
                _IdNivelEscogido = value;
                _itemsDeNivel = Items.Where(i => i.NivelId == _IdNivelEscogido).ToList();
            }
        }

        private bool _ShowRelationCreationDialog { get; set; }

        public ItemManager()
        {
            _IdNivelEscogido = -10000;
            _itemsDeNivel = new();
            _ShowRelationCreationDialog = false; 
        }

        private List<string> getItemTags(ItemModel i)
        {
            var itemID = i.Id;
            var query = from tag in Tags
                        join relacion in Relaciones on tag.id equals relacion.idTag
                        where relacion.idItem == itemID
                        select tag.Tag;

            return query.ToList();
        }

        private void ShowRelationCreationDialog()
        {
            _ShowRelationCreationDialog = true; 
        }

    }
}
