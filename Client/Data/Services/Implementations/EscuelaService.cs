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
    public class EscuelaService : IEscuelaService
    {
        private readonly HttpClient _http;
        private readonly ILogger<EscuelaService> _logger;
        public EscuelaService(HttpClient client, ILogger<EscuelaService> logger)
        {
            _http = client;
            _logger = logger;
        }

        public async Task<ControllerResponse<EscuelaModel>> GetAsync()
        {
            ControllerResponse<EscuelaModel> _controllerResponse = new();
            try
            {
                var response = await _http.GetAsync("api/Escuela");
                if (response.IsSuccessStatusCode)
                {
                    var escuelas = await response.Content.ReadFromJsonAsync<List<EscuelaModel>>();
                    _controllerResponse.Status = Constantes.OKSTATUS;
                    _controllerResponse.Response = escuelas;
                    return _controllerResponse;
                }
                _controllerResponse.Status = Constantes.INTERNALERRORSTATUS;
                _controllerResponse.Response = new List<EscuelaModel>();
                return _controllerResponse;
            }
            catch (Exception e)
            {
                _controllerResponse.Status = Constantes.INTERNALERRORSTATUS;
                _controllerResponse.Response = new List<EscuelaModel>();
                _logger.LogError(e, "An error occurred while fetching from db");
                return _controllerResponse;
            }
        }

        public async Task<ControllerResponse<EscuelaModel>> PostAsync(EscuelaModel s)
        {
            ControllerResponse<EscuelaModel> _controllerResponse = new();
            try
            {
                var response = await _http.PostAsJsonAsync("api/Escuela", s);
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

        public async Task<ControllerResponse<bool>> VerificarExistenciaDeEscuela(string schoolId)
        {
            ControllerResponse<bool> _controllerResponse = new();
            try
            {
                var response = await _http.GetAsync($"api/Escuela/check/{schoolId}");
                if (response.IsSuccessStatusCode)
                {
                    var existencia = await response.Content.ReadFromJsonAsync<bool>();
                    List<bool> lista = new List<bool>();
                    lista.Add(existencia);
                    _controllerResponse.Status = Constantes.OKSTATUS;
                    _controllerResponse.Response = lista;
                    return _controllerResponse;
                }
                _controllerResponse.Status = Constantes.INTERNALERRORSTATUS;
                _controllerResponse.Response = new List<bool>();
                return _controllerResponse;
            }
            catch (Exception e)
            {
                _controllerResponse.Status = Constantes.INTERNALERRORSTATUS;
                _controllerResponse.Response = new List<bool>();
                _logger.LogError(e, "An error occurred while fetching from db");
                return _controllerResponse;
            }
        }
    }
}
