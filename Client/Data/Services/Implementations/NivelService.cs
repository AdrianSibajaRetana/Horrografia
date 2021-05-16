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


namespace Horrografia.Client.Data.Services.Implementations
{
    public class NivelService
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
                return _controllerResponse;
            }
            catch (Exception e)
            {
                _controllerResponse.Status = Constantes.INTERNALERRORSTATUS;
                _logger.LogError(e, "An error occurred while fetching from db");
                return _controllerResponse;
            }
        }

        public async Task<int> PostAsync(NivelModel n)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("api/Nivel", n);
                if (response.IsSuccessStatusCode)
                {
                    return Constantes.OKSTATUS;
                }
                return Constantes.INTERNALERRORSTATUS;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while fetching from db");
                return Constantes.INTERNALERRORSTATUS;
            }
        }
    }
}
