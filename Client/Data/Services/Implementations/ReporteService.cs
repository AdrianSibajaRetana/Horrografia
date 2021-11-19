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
    public class ReporteService : IReporteService
    {
        private readonly HttpClient _http;
        private readonly HttpClient _anonymousHttpClient;
        private readonly ILogger<ReporteService> _logger;
        
        public ReporteService(IHttpClientFactory httpClientFactory, ILogger<ReporteService> logger)
        {
            _http = httpClientFactory.CreateClient("Horrografia.ServerAPI");
            _anonymousHttpClient = httpClientFactory.CreateClient("Horrografia.ServerAPI.Anonymous");
            _logger = logger;
        }
        
        public async Task<ControllerResponse<ReporteModel>> GetAsync()
        {
            ControllerResponse<ReporteModel> controllerResponse = new();
            try
            {
                var response = await _anonymousHttpClient.GetAsync("api/Reporte");
                if (response.IsSuccessStatusCode)
                {
                    var reportes = await response.Content.ReadFromJsonAsync<List<ReporteModel>>();
                    controllerResponse.Status = Constantes.OKSTATUS;
                    controllerResponse.Response = reportes;
                    return controllerResponse;
                }
                controllerResponse.Status = Constantes.INTERNALERRORSTATUS;
                controllerResponse.Response = new List<ReporteModel>();
                return controllerResponse;
            }
            catch (Exception e)
            {
                controllerResponse.Status = Constantes.INTERNALERRORSTATUS;
                controllerResponse.Response = new List<ReporteModel>();
                _logger.LogError(e, "An error occurred while fetching from db");
                return controllerResponse;
            }
        }

        public async Task<ControllerResponse<ReporteModel>> GetUserReportsById(string UserID)
        {
            ControllerResponse<ReporteModel> controllerResponse = new();
            try
            {
                var response = await _anonymousHttpClient.GetAsync($"api/Reporte/{UserID}");
                if (response.IsSuccessStatusCode)
                {
                    var reportes = await response.Content.ReadFromJsonAsync<List<ReporteModel>>();
                    controllerResponse.Status = Constantes.OKSTATUS;
                    controllerResponse.Response = reportes;
                    return controllerResponse;
                }
                controllerResponse.Status = Constantes.INTERNALERRORSTATUS;
                controllerResponse.Response = new List<ReporteModel>();
                return controllerResponse;
            }
            catch (Exception e)
            {
                controllerResponse.Status = Constantes.INTERNALERRORSTATUS;
                controllerResponse.Response = new List<ReporteModel>();
                _logger.LogError(e, "An error occurred while fetching from db");
                return controllerResponse;
            }
        }

        public async Task<ControllerResponse<ReporteModel>> PostAsync(ReporteModel r)
        {
            ControllerResponse<ReporteModel> controllerResponse = new();
            try
            {
                var response = await _http.PostAsJsonAsync("api/Reporte", r);
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

        public async Task<ControllerResponse<ReporteModel>> DeleteAsync(ReporteModel r)
        {
            ControllerResponse<ReporteModel> controllerResponse = new();
            try
            {
                var response = await _http.DeleteAsync($"api/Reporte/{r.Id}");
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

        public async Task<ControllerResponse<bool>> VerifyReportID(int ID)
        {
            ControllerResponse<bool> controllerResponse = new();
            try
            {
                var response = await _anonymousHttpClient.PostAsJsonAsync("api/Usuario/verificar", ID);
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