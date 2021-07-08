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
    public partial class ItemEditionDialog
    {
        [Parameter]
        public bool ShowDialogue { get; set; }

        [Parameter]
        public ItemModel ItemToChange { get; set; }

        [Parameter]
        public List<PistaModel> PistasTotales { get; set; }

        [Parameter]
        public List<FormaIncorrectaModel> FormasIncorrectasTotales { get; set; }

        [Parameter]
        public EventCallback OnDialogClosed { get; set; }

        [Parameter]
        public EventCallback<ItemModel> OnItemUpdate { get; set; }

        [Parameter]
        public EventCallback<PistaModel> OnPistaUpdate { get; set; }

        [Parameter]
        public EventCallback<List<FormaIncorrectaModel>> OnFormaIncorrectaCreation { get; set; }

        [Parameter]
        public EventCallback<List<FormaIncorrectaModel>> OnFormaIncorrectaDeletion { get; set; }


        [Parameter]
        public EventCallback OnErrorOcurred { get; set; }

        private ClientItemModel _newModel { get; set; }
        private ItemModel _model { get; set; }
        private bool _ActualizandoItem { get; set; }
        private string _EstadoDeActualizacion { get; set; }
        private List<string> formasIncorrectasOriginales { get; set; }

        public ItemEditionDialog()
        {
            _newModel = new();
            _model = new();
            formasIncorrectasOriginales = new();
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
            _newModel.FormasIncorrectas = getFormasIncorrectas(_newModel.Id);
            formasIncorrectasOriginales = getFormasIncorrectas(_newModel.Id);
        }

        private void insertarFormaIncorrecta()
        {
            _newModel.FormasIncorrectas.Add("");
            StateHasChanged();
        }

        private void borrarUltimaForma()
        {
            var count = _newModel.FormasIncorrectas.Count() - 1;
            _newModel.FormasIncorrectas.RemoveAt(count);
        }

        private List<string> getFormasIncorrectas(int id)
        {
            var formas = FormasIncorrectasTotales.Where(f => f.Itemid == id).Select(f => f.Forma).ToList();
            return formas; 
        }

        private string GetPistaById(int? id)
        {
            string pista = "";
            if (id.HasValue)
            {
                pista = PistasTotales.FirstOrDefault(p => p.Id == id).Pista;
            }
            return pista;
        }

        private async Task UpdateItem()
        {
            try 
            {
                if (!VerificarFormasVacias())
                { 
                    //Se empieza a actualizar el item
                    _ActualizandoItem = true;
                    _EstadoDeActualizacion = "Actualizando las formas incorrectas.";
                    await AdministrarFormasIncorrectas();
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
            } 
            catch 
            {
                await OnErrorOcurred.InvokeAsync();
                await CloseDialog();
            }
        }

        // Como el editform no verifica las formas vacías, se tiene que hacer a mano. 
        private bool VerificarFormasVacias()
        {
            var tieneFormasVacias = _newModel.FormasIncorrectas.Any(f => f == "");
            return tieneFormasVacias;
        }

        private async Task AdministrarFormasIncorrectas()
        {
            //Para borrar: 
            //  Se consiguen las formas que están en la lista original pero no en la nueva.
            var formasABorrar = formasIncorrectasOriginales.Where(f => !_newModel.FormasIncorrectas.Any(f2 => f == f2)).ToList();
            if (formasABorrar.Any())
            { 
                await BorrarFormasIncorrectas(formasABorrar);
            }

            //Para crear: 
            // Se consiguen las formas que están en la lista nueva pero no en la original. 
            var formasACrear = _newModel.FormasIncorrectas.Where(f => !formasIncorrectasOriginales.Any(f2 => f == f2)).ToList();
            if (formasACrear.Any())
            {                
                await CrearFormasIncorrectas(formasACrear);
            }
        }

        // Borra las formas incorrectas no deseadas
        private async Task BorrarFormasIncorrectas(List<string> formasABorrar)
        {
            var formas = generarFormasIncorrectas(formasABorrar);
            await OnFormaIncorrectaDeletion.InvokeAsync(formas);
        }

        // Crea las formas incorrectas deseadas
        private async Task CrearFormasIncorrectas(List<string> formasACrear)
        {
            var formas = generarFormasIncorrectas(formasACrear);
            await OnFormaIncorrectaCreation.InvokeAsync(formas);
        }

        // Devuelve una lista de formas incorrectas a partir de la lista de formas en strings. 
        private List<FormaIncorrectaModel> generarFormasIncorrectas(List<string> formasEnString)
        {
            int id = _newModel.Id;
            List<FormaIncorrectaModel> formasIncorrectas = new();
            foreach (var forma in formasEnString)
            {
                FormaIncorrectaModel f = new();
                f.Forma = forma;
                f.Itemid = id;
                formasIncorrectas.Add(f);
            }
            return formasIncorrectas;
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
            // Si la pista no es vacía
            if (!string.IsNullOrEmpty(_newModel.Pista))
            { 
                var PistaExistente = PistasTotales.Where(p => p.Pista == _newModel.Pista).FirstOrDefault();
                if (PistaExistente == null)
                {
                    PistaModel p = new();
                    p.Pista = _newModel.Pista;
                    await CrearPista(p);                
                }            
            }
        }

        private async Task CrearPista(PistaModel p)
        {
            await OnPistaUpdate.InvokeAsync(p);
        }

        // Se asigna la pista al item
        private void AsignarPista()
        {
            //Si la pista en el EditForm no es vacía
            if (!string.IsNullOrEmpty(_newModel.Pista))
            {
                var PistaExistenteId = PistasTotales.Where(p => p.Pista == _newModel.Pista).FirstOrDefault().Id;
                _newModel.Pistaid = PistaExistenteId;
            }
            else
            {
                _newModel.Pistaid = null;
            }
        }

        private async Task CloseDialog()
        {
            await OnDialogClosed.InvokeAsync();
        }

    }
}
