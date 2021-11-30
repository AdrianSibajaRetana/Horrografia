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
    public class ContieneErrorService : IContieneErrorService
    {
        private readonly HttpClient _http;
        private readonly HttpClient _anonymousHttpClient;
        private readonly ILogger<ContieneErrorService> _logger;
        
        public ContieneErrorService(IHttpClientFactory httpClientFactory, ILogger<ContieneErrorService> logger)
        {
            _http = httpClientFactory.CreateClient("Horrografia.ServerAPI");
            _anonymousHttpClient = httpClientFactory.CreateClient("Horrografia.ServerAPI.Anonymous");
            _logger = logger;
        }
        
        public async Task<ControllerResponse<ContieneErrorModel>> GetAsync()
        {
            ControllerResponse<ContieneErrorModel> controllerResponse = new();
            try
            {
                var response = await _anonymousHttpClient.GetAsync("api/ContieneError");
                if (response.IsSuccessStatusCode)
                {
                    var errores = await response.Content.ReadFromJsonAsync<List<ContieneErrorModel>>();
                    controllerResponse.Status = Constantes.OKSTATUS;
                    controllerResponse.Response = errores;
                    return controllerResponse;
                }
                controllerResponse.Status = Constantes.INTERNALERRORSTATUS;
                controllerResponse.Response = new List<ContieneErrorModel>();
                return controllerResponse;
            }
            catch (Exception e)
            {
                controllerResponse.Status = Constantes.INTERNALERRORSTATUS;
                controllerResponse.Response = new List<ContieneErrorModel>();
                _logger.LogError(e, "An error occurred while fetching from db");
                return controllerResponse;
            }
        }

        public async Task<ControllerResponse<ContieneErrorModel>> GetByReporteIDAsync(int ID)
        {
            ControllerResponse<ContieneErrorModel> controllerResponse = new();
            try
            {
                var response = await _anonymousHttpClient.GetAsync($"api/ContieneError/{ID}");
                if (response.IsSuccessStatusCode)
                {
                    var errores = await response.Content.ReadFromJsonAsync<List<ContieneErrorModel>>();
                    controllerResponse.Status = Constantes.OKSTATUS;
                    controllerResponse.Response = errores;
                    return controllerResponse;
                }
                controllerResponse.Status = Constantes.INTERNALERRORSTATUS;
                controllerResponse.Response = new List<ContieneErrorModel>();
                return controllerResponse;
            }
            catch (Exception e)
            {
                controllerResponse.Status = Constantes.INTERNALERRORSTATUS;
                controllerResponse.Response = new List<ContieneErrorModel>();
                _logger.LogError(e, "An error occurred while fetching from db");
                return controllerResponse;
            }
        }

        public async Task<ControllerResponse<ContieneErrorModel>> PostAsync(ContieneErrorModel error)
        {
            ControllerResponse<ContieneErrorModel> controllerResponse = new ();
            try
            {
                var response = await _anonymousHttpClient.PostAsJsonAsync("api/ContieneError", error);
                if (response.IsSuccessStatusCode)
                {
                    controllerResponse.Status = Constantes.OKSTATUS;
                    return controllerResponse;
                }
                controllerResponse.Status = Constantes.INTERNALERRORSTATUS;
                return controllerResponse;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while fetching from db");
                controllerResponse.Status = Constantes.INTERNALERRORSTATUS;
                return controllerResponse;
            }
        }
        
    }
}