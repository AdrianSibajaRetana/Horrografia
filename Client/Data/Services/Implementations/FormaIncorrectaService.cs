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

    public class FormaIncorrectaService : IFormaIncorrectaService
    {
        private readonly HttpClient _http;
        private readonly ILogger<FormaIncorrectaService> _logger;
        public FormaIncorrectaService(HttpClient client, ILogger<FormaIncorrectaService> logger)
        {
            _http = client;
            _logger = logger;
        }

        public async Task<ControllerResponse<FormaIncorrecta>> GetAsync()
        {
            ControllerResponse<FormaIncorrecta> _controllerResponse = new();
            try
            {
                var response = await _http.GetAsync("api/Formaincorrecta");
                if (response.IsSuccessStatusCode)
                {
                    var formasIncorrectas = await response.Content.ReadFromJsonAsync<List<FormaIncorrecta>>();
                    _controllerResponse.Status = Constantes.OKSTATUS;
                    _controllerResponse.Response = formasIncorrectas;
                    return _controllerResponse;
                }
                _controllerResponse.Status = Constantes.INTERNALERRORSTATUS;
                _controllerResponse.Response = new List<FormaIncorrecta>();
                return _controllerResponse;
            }
            catch (Exception e)
            {
                _controllerResponse.Status = Constantes.INTERNALERRORSTATUS;
                _controllerResponse.Response = new List<FormaIncorrecta>();
                _logger.LogError(e, "An error occurred while fetching from db");
                return _controllerResponse;
            }
        }

        public async Task<ControllerResponse<FormaIncorrecta>> GetFormasIncorrectasFromLevelId(int nivelid)
        {
            ControllerResponse<FormaIncorrecta> _controllerResponse = new();
            try
            {
                var response = await _http.GetAsync($"api/Formaincorrecta/{nivelid}");
                if (response.IsSuccessStatusCode)
                {
                    var formasIncorrectas = await response.Content.ReadFromJsonAsync<List<FormaIncorrecta>>();
                    _controllerResponse.Status = Constantes.OKSTATUS;
                    _controllerResponse.Response = formasIncorrectas;
                    return _controllerResponse;
                }
                _controllerResponse.Status = Constantes.INTERNALERRORSTATUS;
                _controllerResponse.Response = new List<FormaIncorrecta>();
                return _controllerResponse;
            }
            catch (Exception e)
            {
                _controllerResponse.Status = Constantes.INTERNALERRORSTATUS;
                _controllerResponse.Response = new List<FormaIncorrecta>();
                _logger.LogError(e, "An error occurred while fetching from db");
                return _controllerResponse;
            }
        }

        public async Task<ControllerResponse<FormaIncorrecta>> PostAsync(FormaIncorrecta f)
        {
            ControllerResponse<FormaIncorrecta> _controllerResponse = new();
            try
            {
                var response = await _http.PostAsJsonAsync("api/Formaincorrecta", f);
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

        public async Task<ControllerResponse<FormaIncorrecta>> DeleteAsync(FormaIncorrecta f)
        {
            ControllerResponse<FormaIncorrecta> _controllerResponse = new();
            try
            {
                var response = await _http.DeleteAsync($"api/PerteneceA/{f.Itemid}/{f.Forma}");
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