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
    public class NivelService : INivelService
    {
        private readonly HttpClient _http;
        private readonly ILogger<NivelService> _logger;
        public NivelService(HttpClient client, ILogger<NivelService> logger)
        {
            _http = client;
            _logger = logger;
        }

        public async Task<ControllerResponse<NivelModel>> GetAsync()
        {
            ControllerResponse<NivelModel> _controllerResponse = new ControllerResponse<NivelModel>();
            try
            {
                var response = await _http.GetAsync("api/Nivel");
                if (response.IsSuccessStatusCode)
                {
                    var niveles = await response.Content.ReadFromJsonAsync<List<NivelModel>>();
                    _controllerResponse.Status = Constantes.OKSTATUS;
                    _controllerResponse.Response = niveles;
                    return _controllerResponse;
                }
                _controllerResponse.Status = Constantes.INTERNALERRORSTATUS;
                _controllerResponse.Response = new List<NivelModel>();
                return _controllerResponse;
            }
            catch (Exception e)
            {
                _controllerResponse.Status = Constantes.INTERNALERRORSTATUS;
                _controllerResponse.Response = new List<NivelModel>();
                _logger.LogError(e, "An error occurred while fetching from db");
                return _controllerResponse;
            }
        }

        public async Task<ControllerResponse<NivelModel>> PostAsync(NivelModel n)
        {
            ControllerResponse<NivelModel> _controllerResponse = new ControllerResponse<NivelModel>();
            try
            {
                var response = await _http.PostAsJsonAsync("api/Nivel", n);
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

        public async Task<ControllerResponse<NivelModel>> DeleteAsync(NivelModel n)
        {
            ControllerResponse<NivelModel> _controllerResponse = new ControllerResponse<NivelModel>();
            try
            {

                var response = await _http.DeleteAsync($"api/Nivel/{n.Id}");
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
