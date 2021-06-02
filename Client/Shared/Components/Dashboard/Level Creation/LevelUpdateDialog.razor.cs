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
    public partial class LevelUpdateDialog
    {
        [Parameter]
        public bool ShowDialogue { get; set; }

        [Parameter]
        public EventCallback OnDialogClosed { get; set; }

        [Parameter]
        public EventCallback<NivelModel> OnLevelUpdate { get; set; }

        [Parameter]
        public NivelModel model { get; set; }

        [Parameter]
        public ClientNivelModel newModel { get; set; }

        private NivelModel _model { get; set; }

        public LevelUpdateDialog()
        {
            newModel = new();            
        }

        protected override void OnInitialized()
        {
            _model = model;
        }

        protected override void OnParametersSet()
        {
            if (_model != model)
            {
                _model = model;
                if (_model != null)
                {
                    newModel.setDataFromModel(_model);
                }                
            }
        }

        public async Task CloseDialog()
        {
            await OnDialogClosed.InvokeAsync();
        }

        public async Task UpdateLevel()
        {
            NivelModel updatedNivelModel = newModel.CreateNivelModel();
            await OnLevelUpdate.InvokeAsync(updatedNivelModel);
        }
    }
}
