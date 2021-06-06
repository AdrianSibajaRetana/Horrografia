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

        private bool _showItemCreationDialog { get; set; }

        public LevelItemTable()
        {
            _showItemCreationDialog = false;
        }

        private string GetPistaById(int id)
        {
            return PistasTotales.FirstOrDefault(p => p.Id == id).Pista;
        }

        private void openItemCreationDialog()
        {
            _showItemCreationDialog = true;
        }

        protected void closeItemCreationDialog()
        {
            _showItemCreationDialog = false;
        }

    }
}
