using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using Horrografia.Client.Shared.Objects;
using Horrografia.Shared.Models;
using Microsoft.Extensions.Logging;
using Horrografia.Client.Shared.Constants;
using Horrografia.Client.Data.Services.Interfaces;

namespace Horrografia.Client.Data.Services.Implementations
{
    public class ItemService : IItemService
    {
        private readonly HttpClient _http;
        private readonly HttpClient _anonymousHttpClient;
        private readonly ILogger<ItemService> _logger;
        public ItemService(IHttpClientFactory httpClientFactory, ILogger<ItemService> logger)
        {
            _http = httpClientFactory.CreateClient("Horrografia.ServerAPI");
            _anonymousHttpClient = httpClientFactory.CreateClient("Horrografia.ServerAPI.Anonymous");
            _logger = logger;
        }

        public async Task<ControllerResponse<ItemModel>> GetAsync()
        {
            ControllerResponse<ItemModel> _controllerResponse = new();
            try
            {
                var response = await _http.GetAsync("api/Item");
                if (response.IsSuccessStatusCode)
                {
                    var items = await response.Content.ReadFromJsonAsync<List<ItemModel>>();
                    _controllerResponse.Status = Constantes.OKSTATUS;
                    _controllerResponse.Response = items;
                    return _controllerResponse;
                }
                _controllerResponse.Status = Constantes.INTERNALERRORSTATUS;
                _controllerResponse.Response = new List<ItemModel>();
                return _controllerResponse;
            }
            catch (Exception e)
            {
                _controllerResponse.Status = Constantes.INTERNALERRORSTATUS;
                _controllerResponse.Response = new List<ItemModel>();
                _logger.LogError(e, "An error occurred while fetching from db");
                return _controllerResponse;
            }
        }

        public async Task<ControllerResponse<ItemModel>> GetAsyncByLevelId(int id)
        {
            ControllerResponse<ItemModel> _controllerResponse = new();
            try
            {
                var response = await _anonymousHttpClient.GetAsync($"api/Item/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var items = await response.Content.ReadFromJsonAsync<List<ItemModel>>();
                    _controllerResponse.Status = Constantes.OKSTATUS;
                    _controllerResponse.Response = items;
                    return _controllerResponse;
                }
                _controllerResponse.Status = Constantes.INTERNALERRORSTATUS;
                _controllerResponse.Response = new List<ItemModel>();
                return _controllerResponse;
            }
            catch (Exception e)
            {
                _controllerResponse.Status = Constantes.INTERNALERRORSTATUS;
                _controllerResponse.Response = new List<ItemModel>();
                _logger.LogError(e, "An error occurred while fetching from db");
                return _controllerResponse;
            }
        }

        public async Task<ControllerResponse<ItemModel>> PostAsync(ItemModel i) 
        {
            ControllerResponse<ItemModel> _controllerResponse = new ();
            try
            {
                var response = await _http.PostAsJsonAsync("api/Item", i);
                if (response.IsSuccessStatusCode)
                {
                    _controllerResponse.Status = Constantes.OKSTATUS;
                    return _controllerResponse;
                }
                _controllerResponse.Status = Constantes.INTERNALERRORSTATUS;
                return _controllerResponse;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while fetching from db");
                _controllerResponse.Status = Constantes.INTERNALERRORSTATUS;
                return _controllerResponse;
            }
        }

        public async Task<ControllerResponse<ItemModel>> DeleteAsync(ItemModel i)
        {
            ControllerResponse<ItemModel> _controllerResponse = new();
            try
            {

                var response = await _http.DeleteAsync($"api/Item/{i.Id}");
                if (response.IsSuccessStatusCode)
                {
                    _controllerResponse.Status = Constantes.OKSTATUS;
                    return _controllerResponse;
                }
                _controllerResponse.Status = Constantes.INTERNALERRORSTATUS;
                return _controllerResponse;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while fetching from db");
                _controllerResponse.Status = Constantes.INTERNALERRORSTATUS;
                return _controllerResponse;
            }
        }

        public async Task<ControllerResponse<ItemModel>> UpdateAsync(ItemModel i)
        {
            ControllerResponse<ItemModel> _controllerResponse = new();
            try
            {
                var response = await _http.PutAsJsonAsync("api/Item", i);
                if (response.IsSuccessStatusCode)
                {
                    _controllerResponse.Status = Constantes.OKSTATUS;
                    return _controllerResponse;
                }
                _controllerResponse.Status = Constantes.INTERNALERRORSTATUS;
                return _controllerResponse;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while fetching from db");
                _controllerResponse.Status = Constantes.INTERNALERRORSTATUS;
                return _controllerResponse;
            }
        }

    }
}
