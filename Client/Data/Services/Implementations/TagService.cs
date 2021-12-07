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
    public class TagService  : ITagService
    {
        private readonly HttpClient _http;
        private readonly ILogger<TagService> _logger;
        public TagService(HttpClient client, ILogger<TagService> logger)
        {
            _http = client;
            _logger = logger;
        }

        public async Task<ControllerResponse<TagModel>> GetAsync()
        {
            ControllerResponse<TagModel> _controllerResponse = new();
            try
            {
                var response = await _http.GetAsync("api/Tag");
                if (response.IsSuccessStatusCode)
                {
                    var tags = await response.Content.ReadFromJsonAsync<List<TagModel>>();
                    _controllerResponse.Status = Constantes.OKSTATUS;
                    _controllerResponse.Response = tags;
                    return _controllerResponse;
                }
                _controllerResponse.Status = Constantes.INTERNALERRORSTATUS;
                _controllerResponse.Response = new List<TagModel>();
                return _controllerResponse;
            }
            catch (Exception e)
            {
                _controllerResponse.Status = Constantes.INTERNALERRORSTATUS;
                _controllerResponse.Response = new List<TagModel>();
                _logger.LogError(e, "An error occurred while fetching from db");
                return _controllerResponse;
            }
        }

        public async Task<ControllerResponse<TagModel>> PostAsync(TagModel t)
        {
            ControllerResponse<TagModel> _controllerResponse = new();
            try
            {
                var response = await _http.PostAsJsonAsync("api/Tag", t);
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

        public async Task<ControllerResponse<TagModel>> DeleteAsync(TagModel t)
        {
            ControllerResponse<TagModel> _controllerResponse = new();
            try
            {
                var response = await _http.DeleteAsync($"api/Tag/{t.id}");
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

        public async Task<ControllerResponse<TagModel>> PostRelationAync(ItemTagModel i)
        {
            ControllerResponse<TagModel> _controllerResponse = new();
            try
            {

                var response = await _http.PostAsync($"api/Tag/{i.idItem}/{i.idTag}", null);
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

        public async Task<ControllerResponse<TagModel>> DeleteRelationAync(ItemTagModel i)
        {
            ControllerResponse<TagModel> _controllerResponse = new();
            try
            {
                var response = await _http.DeleteAsync($"api/Tag/{i.idItem}/{i.idTag}");
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

        public async Task<ControllerResponse<ItemTagModel>> GetRelationsAync()
        {
            ControllerResponse<ItemTagModel> _controllerResponse = new();
            try
            {
                var response = await _http.GetAsync("api/Tag/relations");
                if (response.IsSuccessStatusCode)
                {
                    var tags = await response.Content.ReadFromJsonAsync<List<ItemTagModel>>();
                    _controllerResponse.Status = Constantes.OKSTATUS;
                    _controllerResponse.Response = tags;
                    return _controllerResponse;
                }
                _controllerResponse.Status = Constantes.INTERNALERRORSTATUS;
                _controllerResponse.Response = new List<ItemTagModel>();
                return _controllerResponse;
            }
            catch (Exception e)
            {
                _controllerResponse.Status = Constantes.INTERNALERRORSTATUS;
                _controllerResponse.Response = new List<ItemTagModel>();
                _logger.LogError(e, "An error occurred while fetching from db");
                return _controllerResponse;
            }
        }

        public async Task<ControllerResponse<string>> GetTagsFromAllReports()
        {
            ControllerResponse<string> controllerResponse = new();
            try
            {
                var response = await _http.GetAsync("api/Tag/todos");
                if (response.IsSuccessStatusCode)
                {
                    var tags = await response.Content.ReadFromJsonAsync<List<string>>();
                    controllerResponse.Status = Constantes.OKSTATUS;
                    controllerResponse.Response = tags;
                    return controllerResponse;
                }
                controllerResponse.Status = Constantes.INTERNALERRORSTATUS;
                controllerResponse.Response = new List<string>();
                return controllerResponse;
            }
            catch (Exception e)
            {
                controllerResponse.Status = Constantes.INTERNALERRORSTATUS;
                controllerResponse.Response = new List<string>();
                _logger.LogError(e, "An error occurred while fetching from db");
                return controllerResponse;
            }
        }

        public async Task<ControllerResponse<string>> GetTagsFromMonthlyReports(int month, int year)
        {
            ControllerResponse<string> controllerResponse = new();
            try
            {
                var response = await _http.GetAsync($"api/Tag/mensual/{month}/{year}");
                if (response.IsSuccessStatusCode)
                {
                    var tags = await response.Content.ReadFromJsonAsync<List<string>>();
                    controllerResponse.Status = Constantes.OKSTATUS;
                    controllerResponse.Response = tags;
                    return controllerResponse;
                }
                controllerResponse.Status = Constantes.INTERNALERRORSTATUS;
                controllerResponse.Response = new List<string>();
                return controllerResponse;
            }
            catch (Exception e)
            {
                controllerResponse.Status = Constantes.INTERNALERRORSTATUS;
                controllerResponse.Response = new List<string>();
                _logger.LogError(e, "An error occurred while fetching from db");
                return controllerResponse;
            }
        }

        public async Task<ControllerResponse<string>> GetTagsFromYearlyReports(int year)
        {
            ControllerResponse<string> controllerResponse = new();
            try
            {
                var response = await _http.GetAsync($"api/Tag/anual/{year}");
                if (response.IsSuccessStatusCode)
                {
                    var tags = await response.Content.ReadFromJsonAsync<List<string>>();
                    controllerResponse.Status = Constantes.OKSTATUS;
                    controllerResponse.Response = tags;
                    return controllerResponse;
                }
                controllerResponse.Status = Constantes.INTERNALERRORSTATUS;
                controllerResponse.Response = new List<string>();
                return controllerResponse;
            }
            catch (Exception e)
            {
                controllerResponse.Status = Constantes.INTERNALERRORSTATUS;
                controllerResponse.Response = new List<string>();
                _logger.LogError(e, "An error occurred while fetching from db");
                return controllerResponse;
            }
        }

        public async Task<ControllerResponse<string>> GetTagsFromSchool(string schoolCode)
        {
            ControllerResponse<string> controllerResponse = new();
            try
            {
                var response = await _http.GetAsync($"api/Tag/escuela/{schoolCode}");
                if (response.IsSuccessStatusCode)
                {
                    var tags = await response.Content.ReadFromJsonAsync<List<string>>();
                    controllerResponse.Status = Constantes.OKSTATUS;
                    controllerResponse.Response = tags;
                    return controllerResponse;
                }
                controllerResponse.Status = Constantes.INTERNALERRORSTATUS;
                controllerResponse.Response = new List<string>();
                return controllerResponse;
            }
            catch (Exception e)
            {
                controllerResponse.Status = Constantes.INTERNALERRORSTATUS;
                controllerResponse.Response = new List<string>();
                _logger.LogError(e, "An error occurred while fetching from db");
                return controllerResponse;
            }
        }

        public async Task<ControllerResponse<string>> GetTagsFromUser(string id)
        {
            ControllerResponse<string> controllerResponse = new();
            try
            {
                var response = await _http.GetAsync($"api/Tag/usuario/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var tags = await response.Content.ReadFromJsonAsync<List<string>>();
                    controllerResponse.Status = Constantes.OKSTATUS;
                    controllerResponse.Response = tags;
                    return controllerResponse;
                }
                controllerResponse.Status = Constantes.INTERNALERRORSTATUS;
                controllerResponse.Response = new List<string>();
                return controllerResponse;
            }
            catch (Exception e)
            {
                controllerResponse.Status = Constantes.INTERNALERRORSTATUS;
                controllerResponse.Response = new List<string>();
                _logger.LogError(e, "An error occurred while fetching from db");
                return controllerResponse;
            }
        }
    }
}
