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
    public partial class ItemCreationDialog
    {
        [Parameter]
        public bool ShowDialogue { get; set; }

        [Parameter]
        public List<ItemModel> ItemsTotales{ get; set;}

        [Parameter]
        public List<PistaModel> PistasTotales { get; set; }

        [Parameter]
        public List<FormaIncorrectaModel> FormasIncorrectas { get; set; }

        [Parameter]
        public EventCallback OnDialogClosed { get; set; }

        [Parameter]
        public EventCallback<ItemModel> OnItemCreation { get; set; }

        [Parameter]
        public EventCallback<PistaModel> OnClueCreation { get; set; }

        [Parameter]
        public EventCallback<List<FormaIncorrectaModel>> OnFormaIncorrectaCreation { get; set; }

        [Parameter]
        public EventCallback<ItemModel> OnRelationCreation { get; set; }

        [Parameter]
        public EventCallback OnErrorOcurred { get; set; }

        private ClientItemModel _model { get; set; }
        private bool _isCreatingItem { get; set; }
        private string _loadingStatus { get; set; }


        public ItemCreationDialog()
        {
            _model = new();
            _model.FormasIncorrectas.Add("");
            _isCreatingItem = false;
        }

        public async Task CloseDialog()
        {
            _model = new();
            _model.FormasIncorrectas.Add("");
            await OnDialogClosed.InvokeAsync();
        }

        private void insertarFormaIncorrecta()
        {
            _model.FormasIncorrectas.Add("");
            StateHasChanged();
        }

        private void borrarUltimaForma()
        {
            var count = _model.FormasIncorrectas.Count() - 1;
            _model.FormasIncorrectas.RemoveAt(count);
        }

        /*Función de búsqueda en el autocompletar de Formas Correctas*/
        private async Task<IEnumerable<string>> BuscarFormasCorrectasExistentes(string value)
        {
            // Retraso sutil para la interfaz de creación. 
            await Task.Delay(5);

            // Si la persona no ha puesto un input, no muestre nada
            if (string.IsNullOrEmpty(value))
            {
                return Array.Empty<string>();
            }

            var filteredList = ItemsTotales.Where(i => i.FormaCorrecta.Contains(value, StringComparison.InvariantCultureIgnoreCase));
            var filteredItems = filteredList.Select(i => i.FormaCorrecta).Distinct().ToList();
            return filteredItems;
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

        //Método que se llama cuando al crear un item en la forma.
        private async Task CreateItem(EditContext context)
        {
            try
            {
                if (!VerificarFormasVacias())
                { 
                    _isCreatingItem = true;
                    _loadingStatus = "Creando la pista del item.";
                    await VerificarCreacionDePista();
                    _loadingStatus = "Creando el item.";
                    await CrearItem();
                    _loadingStatus = "Adjuntando formas incorrectas al item";
                    await AdjuntarFormasIncorrectas();
                    _loadingStatus = "Generando la relación en la base de datos.";
                    await CrearRelacion();
                    _isCreatingItem = false;
                    await CloseDialog();                
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
            var tieneFormasVacias = _model.FormasIncorrectas.Any(f => f == "");
            return tieneFormasVacias;
        }

        //Si existe la pista se crea, si no no se hace nada.
        private async Task VerificarCreacionDePista()
        {
            if (!string.IsNullOrEmpty(_model.Pista))
            { 
                var PistaExistente = PistasTotales.Where(p => p.Pista == _model.Pista).FirstOrDefault();
                if (PistaExistente == null)
                {
                    PistaModel p = new();
                    p.Pista = _model.Pista;
                    await CrearPista(p);
                }                        
            }
        }

        //Se invoca el método para crear una pista.
        private async Task CrearPista(PistaModel p)
        {
            await OnClueCreation.InvokeAsync(p);
        }


        //Se invoca el método para crear un item.
        private async Task CrearItem()
        {
            var PistaExistente = PistasTotales.Where(p => p.Pista == _model.Pista).FirstOrDefault();
            ItemModel i = new();
            i.FormaCorrecta = _model.FormaCorrecta;
            if (PistaExistente != null)
            { 
                i.PistaId = PistaExistente.Id;
            }
            await OnItemCreation.InvokeAsync(i);
        }

        //Se adjuntan las formas incorrectas al item
        private async Task AdjuntarFormasIncorrectas()
        {
            // Se consigue el itemModel recien creado
            var PistaExistente = PistasTotales.Where(p => p.Pista == _model.Pista).FirstOrDefault();

            ItemModel ItemExistente = new();
            if (PistaExistente != null)
            {
                ItemExistente = ItemsTotales.Where(i => i.FormaCorrecta == _model.FormaCorrecta &&
                                                        i.PistaId == PistaExistente.Id)
                                                        .FirstOrDefault();
            }
            else
            {
                ItemExistente = ItemsTotales.Where(i => i.FormaCorrecta == _model.FormaCorrecta
                                                        && !i.PistaId.HasValue)
                                                        .FirstOrDefault();
            }

            // Se extrae su ID
            var itemID = ItemExistente.Id;

            //Se crea la lista de modelos para generar en la base de datos. 
            List<FormaIncorrectaModel> formasIncorrectasList = new();
            foreach (string forma in _model.FormasIncorrectas)
            {
                FormaIncorrectaModel f = new();
                f.Forma = forma;
                f.Itemid = itemID;
                formasIncorrectasList.Add(f);
            }
            await OnFormaIncorrectaCreation.InvokeAsync(formasIncorrectasList);
        }

        //Se crea la relación con el item y la pista.
        private async Task CrearRelacion()
        {
            var PistaExistente = PistasTotales.Where(p => p.Pista == _model.Pista).FirstOrDefault();
            ItemModel ItemExistente = new();
            if (PistaExistente != null)
            {
                ItemExistente = ItemsTotales.Where(i => i.FormaCorrecta == _model.FormaCorrecta &&
                                                        i.PistaId == PistaExistente.Id)
                                                        .FirstOrDefault();
            }
            else
            {
                ItemExistente = ItemsTotales.Where(i => i.FormaCorrecta == _model.FormaCorrecta
                                                        && !i.PistaId.HasValue)
                                                        .FirstOrDefault();
            }
            await GenerarRelacion(ItemExistente);
        }

        //Se crea la relación
        private async Task GenerarRelacion(ItemModel i)
        {
            await OnRelationCreation.InvokeAsync(i);
        }
    }
}
