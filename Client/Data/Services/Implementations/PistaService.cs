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
    public class PistaService : IPistaService
    {
        private readonly HttpClient _http;
        private readonly ILogger<PistaService> _logger;
        public PistaService(HttpClient client, ILogger<PistaService> logger)
        {
            _http = client;
            _logger = logger;
        }

        public async Task<ControllerResponse<PistaModel>> GetAsync()
        {
            ControllerResponse<PistaModel> _controllerResponse = new();
            try
            {
                var response = await _http.GetAsync("api/Pista");
                if (response.IsSuccessStatusCode)
                {
                    var pistas = await response.Content.ReadFromJsonAsync<List<PistaModel>>();
                    _controllerResponse.Status = Constantes.OKSTATUS;
                    _controllerResponse.Response = pistas;
                    return _controllerResponse;
                }
                _controllerResponse.Status = Constantes.INTERNALERRORSTATUS;
                _controllerResponse.Response = new List<PistaModel>();
                return _controllerResponse;
            }
            catch (Exception e)
            {
                _controllerResponse.Status = Constantes.INTERNALERRORSTATUS;
                _controllerResponse.Response = new List<PistaModel>();
                _logger.LogError(e, "An error occurred while fetching from db");
                return _controllerResponse;
            }
        }

        public async Task<ControllerResponse<PistaModel>> DeleteAsync(PistaModel p)
        {
            ControllerResponse<PistaModel> _controllerResponse = new();
            try
            {
                var response = await _http.DeleteAsync($"api/Pista/{p.Id}");
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

        public async Task<ControllerResponse<PistaModel>> PostAsync(PistaModel p)
        {
            ControllerResponse<PistaModel> _controllerResponse = new();
            try
            {
                var response = await _http.PostAsJsonAsync("api/Pista", p);
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

        public async Task<ControllerResponse<PistaModel>> UpdateAsync(PistaModel p)
        {
            ControllerResponse<PistaModel> _controllerResponse = new ();
            try
            {
                var response = await _http.PutAsJsonAsync("api/Pista", p);
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
