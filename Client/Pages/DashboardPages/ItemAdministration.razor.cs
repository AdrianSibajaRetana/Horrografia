using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Horrografia.Client.Data.Services.Interfaces;
using Horrografia.Client.Shared.Constants;
using Horrografia.Shared.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Horrografia.Client.Pages.DashboardPages
{
    public partial class ItemAdministration
    {
        [Inject]
        protected ISnackbar _snackbar { get; set; }

        [Inject]
        protected INivelService _nivelService { get; set; }

        [Inject]
        protected IItemService _itemService { get; set; }

        [Inject]
        protected ITagService _tagService { get; set; }

        //Para el manejo de tabs
        private enum ActiveTabState
        { 
            ShowTagsTab,
            ShowItemTab
        }

        //Para recargar entidades
        private enum ReloadFlag
        { 
            Tags,
            Relaciones
        }

        private string _tagsActiveString { get; set; }
        private string _itemsActiveString { get; set; } 
        private ActiveTabState _activeTab { get; set; }
        private const string ShowActiveState = "is-active";

        //Para cargar los datos
        private bool _isLoading { get; set; }
        private int InitialLoadStatus { get; set; }

        // Indica si el cargado de items fue exitoso
        private bool _lecturaExitosa { get; set; }

        //Todos los niveles
        private List<NivelModel> Niveles { get; set; }

        // Todos los items
        private List<ItemModel> Items { get; set; }

        // Todos los tags
        private List<TagModel> Tags { get; set; }

        // Relaciones itemTag
        private List<ItemTagModel> RelacionesItemTag { get; set; }

        public ItemAdministration()
        {
            _isLoading = true; 
            _activeTab = ActiveTabState.ShowTagsTab;
            _tagsActiveString = ShowActiveState;
            _itemsActiveString = null;
            _lecturaExitosa = false; 
        }

        /*Carga los niveles existentes y notifica el estado actual*/
        protected override async Task OnInitializedAsync()
        {
            await CargarDatos();
        }

        private async Task CargarDatos()
        {
            try
            {
                await CargarNiveles();
                await CargarItems();
                await CargarTags();
                await CargarRelaciones();
                _lecturaExitosa = true;
            }
            catch (InvalidOperationException e)
            {
                _lecturaExitosa = false;
                InitialLoadStatus = Constantes.INTERNALERRORSTATUS;
                ShowNotification($"{e.Message}", Severity.Error);
            }
        }

        private async Task CargarNiveles()
        {
            var nivelesResponse = await _nivelService.GetAsync();
            if (nivelesResponse.isResponseSuccesfull())
            {
                Niveles = nivelesResponse.Response;
                Console.WriteLine("Se cambiaron los niveles");
            }
            else
            {
                string reporteError = "niveles";
                throw new InvalidOperationException($"Error al cargar los {reporteError}.");
            }
        }

        private async Task CargarItems()
        {
            var itemsResponse = await _itemService.GetAsync();
            if (itemsResponse.isResponseSuccesfull())
            {
                Items = itemsResponse.Response;
            }
            else
            {
                string reporteError = "items";
                throw new InvalidOperationException($"Error al cargar los {reporteError}.");
            }
        }

        private async Task CargarTags()
        {
            var itemsResponse = await _tagService.GetAsync();
            if (itemsResponse.isResponseSuccesfull())
            {
                Tags = itemsResponse.Response;
            }
            else
            {
                string reporteError = "tags";
                throw new InvalidOperationException($"Error al cargar los {reporteError}.");
            }
        }

        private async Task CargarRelaciones()
        {
            var itemsResponse = await _tagService.GetRelationsAync();
            if (itemsResponse.isResponseSuccesfull())
            {
                RelacionesItemTag = itemsResponse.Response;
            }
            else
            {
                string reporteError = "relaciones";
                throw new InvalidOperationException($"Error al cargar las {reporteError}.");
            }
        }

        //Método que se llama después del OnInitialized. 
        protected override void OnParametersSet()
        {
            _isLoading = false;
            if (_lecturaExitosa)
            {
                _snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopCenter;
                InitialLoadStatus = Constantes.OKSTATUS;
                ShowNotification("Se leyeron los datos de la base de datos exitosamente", Severity.Success);
            }
            else
            {
                ShowNotification("Hubo un error al cargar los datos de la base de datos.", Severity.Error);
            }
        }

        public void ShowNotification(string mensaje, Severity s)
        {
            //Muestra la notifiación
            _snackbar.Add(mensaje, s);
        }

        private void ChangeTabContent(ActiveTabState StateToChange)
        {
            
            if (StateToChange != _activeTab)
            {
                _activeTab = StateToChange;
                switch (StateToChange)
                {
                    case ActiveTabState.ShowItemTab:
                        _itemsActiveString = ShowActiveState;
                        _tagsActiveString = null;
                        break;
                    case ActiveTabState.ShowTagsTab:
                        _itemsActiveString = null;
                        _tagsActiveString = ShowActiveState;
                        break;
                }                
            }
        }

        private async Task RecargarDatos(ReloadFlag flag)
        {
            try
            {
                switch (flag)
                {
                    case ReloadFlag.Tags:
                        await CargarTags();
                        break;
                    case ReloadFlag.Relaciones:
                        await CargarRelaciones();
                        break;
                    default:
                        throw new InvalidOperationException("Parámetro no reconocido.");
                }
            }
            catch (Exception e)
            {
                ShowNotification($"{e.Message}", Severity.Error);
            }
        }

        protected async Task CreateTag(TagModel t)
        {
            var response = await _tagService.PostAsync(t);
            if (response.isResponseSuccesfull())
            {
                ShowNotification($"¡Se creó el tag!", Severity.Success);
            }
            else
            {
                ShowNotification("Hubo un error al crear el tag", Severity.Error);
            }
            await RecargarDatos(ReloadFlag.Tags);
        }

        protected async Task DeleteTag(TagModel t)
        {
            var response = await _tagService.DeleteAsync(t);
            if (response.isResponseSuccesfull())
            {
                ShowNotification($"¡Se borró el tag!", Severity.Success);
            }
            else
            {
                ShowNotification("Hubo un error al borrar el tag", Severity.Error);
            }
            await RecargarDatos(ReloadFlag.Tags);
        }

        protected async Task CreateRelation(ItemTagModel i)
        {
            if (RelacionesItemTag.Exists(r => r.idItem == i.idItem && r.idTag == i.idTag))
            {
                ShowNotification("El item ya se encuentra relacionado al tag.", Severity.Warning);
            }
            else
            { 
                var response = await _tagService.PostRelationAync(i);
                if (response.isResponseSuccesfull())
                {
                    ShowNotification($"¡Se agregó el tag al item!", Severity.Success);
                }
                else
                {
                    ShowNotification("Hubo un error al agregar el tag al item", Severity.Error);
                }
                await RecargarDatos(ReloadFlag.Relaciones);            
            }
        }

        protected async Task DeleteRelation(ItemTagModel i)
        {
            var response = await _tagService.DeleteRelationAync(i);
            if (response.isResponseSuccesfull())
            {
                ShowNotification($"¡Se borró el tag del item!", Severity.Success);
            }
            else
            {
                ShowNotification("Hubo un error al borrar el tag al item", Severity.Error);
            }
            await RecargarDatos(ReloadFlag.Relaciones);
        }

    }
}
