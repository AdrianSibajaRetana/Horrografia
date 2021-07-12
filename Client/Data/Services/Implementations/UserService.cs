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
using Horrografia.Client.Shared.Objects.ClientModels;
using Horrografia.Shared;

namespace Horrografia.Client.Data.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly HttpClient _http;
        private readonly ILogger<UserService> _logger;

        public UserService(HttpClient client, ILogger<UserService> logger)
        {
            _http = client;
            _logger = logger;
        }

        /*
         * Verificar Correo
         * CrearUsuario /api/Usuario
         * Iniciar Sesión /api/Usuario/iniciar-sesion
         * Cerrar Sesión /api/Usuario/cerrar-sesion
         */
        public async Task<ControllerResponse<bool>> VerifyEmail(string correo)
        {
            ControllerResponse<bool> _controllerResponse = new();
            try
            {
                var response = await _http.PostAsJsonAsync("api/Usuario/verificar", correo);
                if (response.IsSuccessStatusCode)
                {
                    var respuesta = await response.Content.ReadFromJsonAsync<List<bool>>();
                    _controllerResponse.Status = Constantes.OKSTATUS;
                    _controllerResponse.Response = respuesta;
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

        public async Task<ControllerResponse<SharedConstants.LoginState>> Login(ClientUserLoginDTO modelo)
        {
            ControllerResponse<SharedConstants.LoginState> _controllerResponse = new();
            try
            {
                var response = await _http.PostAsJsonAsync("api/Usuario/iniciar-sesion", modelo);
                if (response.IsSuccessStatusCode)
                {
                    var respuesta = await response.Content.ReadFromJsonAsync<SharedConstants.LoginState>();
                    List <SharedConstants.LoginState> lista = new();
                    lista.Add(respuesta);
                    _controllerResponse.Status = Constantes.OKSTATUS;
                    _controllerResponse.Response = lista;
                    return _controllerResponse;
                }
                _controllerResponse.Status = Constantes.INTERNALERRORSTATUS;
                _controllerResponse.Response = new List<SharedConstants.LoginState>();
                return _controllerResponse;
            }
            catch (Exception e)
            {
                _controllerResponse.Status = Constantes.INTERNALERRORSTATUS;
                _controllerResponse.Response = new List<SharedConstants.LoginState>();
                _logger.LogError(e, "An error occurred while fetching from db");
                return _controllerResponse;
            }
        }

        public async Task<ControllerResponse<bool>> CerrarSesion()
        {
            ControllerResponse<bool> _controllerResponse = new();
            try
            {
                var response = await _http.PostAsync("api/Usuario/cerrar-sesion", null);
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

        public async Task<ControllerResponse<bool>> Register(ClientUserRegisterDTO modelo)
        {
            ControllerResponse<bool> _controllerResponse = new();
            try
            {
                var response = await _http.PostAsJsonAsync("api/Usuario", modelo);
                if (response.IsSuccessStatusCode)
                {
                    var respuesta = await response.Content.ReadFromJsonAsync<List<bool>>();
                    _controllerResponse.Status = Constantes.OKSTATUS;
                    _controllerResponse.Response = respuesta;
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
