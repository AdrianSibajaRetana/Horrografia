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
        public List<PistaModel> PistasTotales { get; set; }

        [Parameter]
        public EventCallback OnDialogClosed { get; set; }

        [Parameter]
        public EventCallback<ItemModel> OnItemCreation { get; set; }

        [Parameter]
        public EventCallback<PistaModel> OnClueCreation { get; set; }

        [Parameter]
        public EventCallback<int> OnRelationCreation { get; set; }

        private ClientItemModel _model { get; set; }
        private ClientPistaModel _pista { get; set; }

        public ItemCreationDialog()
        {
            _model = new();
            _pista = new();
        }

        public async Task CloseDialog()
        {
            await OnDialogClosed.InvokeAsync();
        }

        /*Función de búsqueda en el autocompletar de Formas Correctas*/
        private async Task<IEnumerable<string>> BuscarFormasCorrectasExistentes(string value)
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

        /*Función de búsqueda en el autocompletar de Formas Incorrectas*/
        private async Task<IEnumerable<string>> BuscarFormasIncorrectasExistentes(string value)
        {
            // Retraso sutil para la interfaz de creación. 
            await Task.Delay(5);

            // Si la persona no ha puesto un input, devuelva toda la lista de los nombres.
            if (string.IsNullOrEmpty(value))
            {
                var allItems = ItemsTotales.Select(i => i.FormaIncorrecta).ToList();
                return allItems;
            }

            var filteredList = ItemsTotales.Where(i => i.FormaIncorrecta.Contains(value, StringComparison.InvariantCultureIgnoreCase));
            var filteredItems = filteredList.Select(i => i.FormaIncorrecta).ToList();
            return filteredItems;
        }

        /*Función de búsqueda en el autocompletar de Pistas*/
        private async Task<IEnumerable<string>> BuscarPistasExistentes(string value)
        {
            // Retraso sutil para la interfaz de creación. 
            await Task.Delay(5);

            // Si la persona no ha puesto un input, devuelva toda la lista de los nombres.
            if (string.IsNullOrEmpty(value))
            {                
                var Pistas = PistasTotales.Select(p => p.Pista).ToList();
                return Pistas;
            }

            var ListaFriltrada = PistasTotales.Where(p => p.Pista.Contains(value, StringComparison.InvariantCultureIgnoreCase));
            var PistasFiltradas = ListaFriltrada.Select(p => p.Pista).ToList();

            return PistasFiltradas;
        }

        //Método que se llama cuando al crear un item en la forma.
        private async Task CreateItem()
        {

            await VerificarCreacionDePista();
            await VerificarExistenciaDeItem();
            //await CrearRelacion();
        }

        
        //Si existe la pista se crea, si no no se hace nada.
        private async Task VerificarCreacionDePista()
        {
            var PistaExistente = PistasTotales.Where(p => p.Pista == _pista.Pista).FirstOrDefault();
            if (PistaExistente == null)
            {
                PistaModel p = new();
                p.Pista = _pista.Pista;
                await CrearPista(p);
            }
        }

        //Se invoca el método para crear una pista.
        private async Task CrearPista(PistaModel p)
        {
            await OnClueCreation.InvokeAsync(p);
        }

        //Si existe el item se crea, si no no se hace nada.
        private async Task VerificarExistenciaDeItem()
        {            
            var PistaExistente = PistasTotales.Where(p => p.Pista == _pista.Pista).FirstOrDefault();
            var ItemExistente = ItemsTotales.Where(i => i.FormaCorrecta == _model.FormaCorrecta &&
                                                        i.FormaIncorrecta == _model.FormaIncorrecta &&
                                                        i.PistaId == PistaExistente.Id)
                                                        .FirstOrDefault();            
            if (ItemExistente == null)
            {
                ItemModel i = new();
                i.FormaCorrecta = _model.FormaCorrecta;
                i.FormaIncorrecta = _model.FormaIncorrecta;
                i.PistaId = PistaExistente.Id;
                await CrearItem(i);
            }            
        }

        //Se invoca el método para crear un item.
        private async Task CrearItem(ItemModel i)
        {
            await OnItemCreation.InvokeAsync(i);
        }

        //Se crea la relación con el item y la pista.
        private async Task CrearRelacion()
        {
            var PistaExistente = PistasTotales.Where(p => p.Pista == _pista.Pista).FirstOrDefault();
            var ItemExistente = ItemsTotales.Where(i => i.FormaCorrecta == _model.FormaCorrecta &&
                                                        i.FormaIncorrecta == _model.FormaIncorrecta &&
                                                        i.PistaId == PistaExistente.Id)
                                                        .FirstOrDefault();
            await GenerarRelacion(ItemExistente.Id);
        }

        //Se crea la relación
        private async Task GenerarRelacion(int ItemId)
        {
            await OnRelationCreation.InvokeAsync(ItemId);
        }
    }
}
