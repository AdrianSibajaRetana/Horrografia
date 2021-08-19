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
    public class RolService : IRolService
    {
        private readonly HttpClient _http;
        private readonly ILogger<RolService> _logger;
        public RolService(HttpClient client, ILogger<RolService> logger)
        {
            _http = client;
            _logger = logger;
        }

        public async Task<ControllerResponse<RolModel>> GetRoles()
        {
            ControllerResponse<RolModel> _controllerResponse = new();
            try
            {
                var response = await _http.GetAsync("api/Rol");
                if (response.IsSuccessStatusCode)
                {
                    var roles = await response.Content.ReadFromJsonAsync<List<RolModel>>();
                    _controllerResponse.Status = Constantes.OKSTATUS;
                    _controllerResponse.Response = roles;
                    return _controllerResponse;
                }
                _controllerResponse.Status = Constantes.INTERNALERRORSTATUS;
                _controllerResponse.Response = new List<RolModel>();
                return _controllerResponse;
            }
            catch (Exception e)
            {
                _controllerResponse.Status = Constantes.INTERNALERRORSTATUS;
                _controllerResponse.Response = new List<RolModel>();
                _logger.LogError(e, "An error occurred while fetching from db");
                return _controllerResponse;
            }
        }

        public async Task<ControllerResponse<UserRolesModel>> GetUsersandRolesRelation()
        {
            ControllerResponse<UserRolesModel> _controllerResponse = new();
            try
            {
                var response = await _http.GetAsync("api/Rol/roles");
                if (response.IsSuccessStatusCode)
                {
                    var rolesDeUsuario = await response.Content.ReadFromJsonAsync<List<UserRolesModel>>();
                    _controllerResponse.Status = Constantes.OKSTATUS;
                    _controllerResponse.Response = rolesDeUsuario;
                    return _controllerResponse;
                }
                _controllerResponse.Status = Constantes.INTERNALERRORSTATUS;
                _controllerResponse.Response = new List<UserRolesModel>();
                return _controllerResponse;
            }
            catch (Exception e)
            {
                _controllerResponse.Status = Constantes.INTERNALERRORSTATUS;
                _controllerResponse.Response = new List<UserRolesModel>();
                _logger.LogError(e, "An error occurred while fetching from db");
                return _controllerResponse;
            }
        }
    }
}
