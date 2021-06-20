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

        [Parameter]
        public EventCallback OnErrorOcurred { get; set; }

        private ClientItemModel _newModel { get; set; }
        private ItemModel _model { get; set; }
        private bool _ActualizandoItem { get; set; }
        private string _EstadoDeActualizacion { get; set; }

        public ItemEditionDialog()
        {
            _newModel = new();
            _model = new();
            _ActualizandoItem = false;

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

        private async Task UpdateItem()
        {
            try 
            { 
                //Se empieza a actualizar el item
                _ActualizandoItem = true;
                _EstadoDeActualizacion = "Actualizando la pista al item.";
                //Se revisa si se tiene que crear una pista nueva
                await VerificarCreacionDePista();
                //Se asigna la pista escrita
                AsignarPista();
                _EstadoDeActualizacion = "Actualizando las formas del item.";
                //Se crea el modelo a partir del modelo en el dominio
                ItemModel updatedItemModel = _newModel.GetItemModel();
                //Se crea el item
                _EstadoDeActualizacion = "Escribiendo el item a la base de datos.";
                await OnItemUpdate.InvokeAsync(updatedItemModel);
                //Se cierra el diálogo
                await CloseDialog();
                //Se termina de actualizar el item
                _ActualizandoItem = false;
            } 
            catch 
            {
                await OnErrorOcurred.InvokeAsync();
                await CloseDialog();
            }
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

        //Si existe la pista se crea, si no no se hace nada.
        private async Task VerificarCreacionDePista()
        {
            var PistaExistente = PistasTotales.Where(p => p.Pista == _newModel.Pista).FirstOrDefault();
            if (PistaExistente == null)
            {
                PistaModel p = new();
                p.Pista = _newModel.Pista;
                await CrearPista(p);                
            }
        }

        private async Task CrearPista(PistaModel p)
        {
            await OnPistaUpdate.InvokeAsync(p);
        }

        private void AsignarPista()
        {
            var PistaExistenteId = PistasTotales.Where(p => p.Pista == _newModel.Pista).FirstOrDefault().Id;
            _newModel.Pistaid = PistaExistenteId;
        }

        private async Task CloseDialog()
        {
            await OnDialogClosed.InvokeAsync();
        }

    }
}
