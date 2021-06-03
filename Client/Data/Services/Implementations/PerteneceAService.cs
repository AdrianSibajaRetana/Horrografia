﻿using System;
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
    public class PerteneceAService : IPerteneceAService
    {
        private readonly HttpClient _http;
        private readonly ILogger<PerteneceAService> _logger;
        public PerteneceAService(HttpClient client, ILogger<PerteneceAService> logger)
        {
            _http = client;
            _logger = logger;
        }

        //REVISAR 
        public async Task<ControllerResponse<PerteneceAModel>> GetAsync()
        {
            ControllerResponse<PerteneceAModel> _controllerResponse = new ();
            try
            {
                var response = await _http.GetAsync("api/PerteneceA");
                if (response.IsSuccessStatusCode)
                {
                    var relaciones = await response.Content.ReadFromJsonAsync<List<PerteneceAModel>>();
                    _controllerResponse.Status = Constantes.OKSTATUS;
                    _controllerResponse.Response = relaciones;
                    return _controllerResponse;
                }
                _controllerResponse.Status = Constantes.INTERNALERRORSTATUS;
                _controllerResponse.Response = new List<PerteneceAModel>();
                return _controllerResponse;
            }
            catch (Exception e)
            {
                _controllerResponse.Status = Constantes.INTERNALERRORSTATUS;
                _controllerResponse.Response = new List<PerteneceAModel>();
                _logger.LogError(e, "An error occurred while fetching from db");
                return _controllerResponse;
            }
        }

        public async Task<ControllerResponse<PerteneceAModel>> PostAsync(PerteneceAModel p)
        {
            ControllerResponse<PerteneceAModel> _controllerResponse = new ();
            try
            {
                var response = await _http.PostAsJsonAsync("api/PerteneceA", p);
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

        public async Task<ControllerResponse<PerteneceAModel>> DeleteAsync(PerteneceAModel p)
        {
            ControllerResponse<PerteneceAModel> _controllerResponse = new();
            try
            {
                var response = await _http.DeleteAsync($"api/PerteneceA/{p.IdNivel}/{p.IdItem}");
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
