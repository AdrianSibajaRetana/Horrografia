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
        private readonly HttpClient _anonymousHttpClient;
        private readonly ILogger<UserService> _logger;

        public UserService(IHttpClientFactory httpClientFactory, ILogger<UserService> logger)
        {
            _http = httpClientFactory.CreateClient("Horrografia.ServerAPI");
            _anonymousHttpClient = httpClientFactory.CreateClient("Horrografia.ServerAPI.Anonymous");
            _logger = logger;
        }


        public async Task<ControllerResponse<UsuarioDTO>> GetAsync()
        {
            ControllerResponse<UsuarioDTO> _controllerResponse = new();
            try
            {
                var response = await _http.GetAsync("api/Usuario");
                if (response.IsSuccessStatusCode)
                {
                    var usuarios = await response.Content.ReadFromJsonAsync<List<UsuarioDTO>>();
                    _controllerResponse.Status = Constantes.OKSTATUS;
                    _controllerResponse.Response = usuarios;
                    return _controllerResponse;
                }
                _controllerResponse.Status = Constantes.INTERNALERRORSTATUS;
                _controllerResponse.Response = new List<UsuarioDTO>();
                return _controllerResponse;
            }
            catch (Exception e)
            {
                _controllerResponse.Status = Constantes.INTERNALERRORSTATUS;
                _controllerResponse.Response = new List<UsuarioDTO>();
                _logger.LogError(e, "An error occurred while fetching from db");
                return _controllerResponse;
            }
        }

        public async Task<ControllerResponse<UsuarioDTO>> GetUserById(string id)
        {
            ControllerResponse<UsuarioDTO> _controllerResponse = new();
            try
            {
                var response = await _http.GetAsync($"api/Usuario/getData/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var usuario = await response.Content.ReadFromJsonAsync<UsuarioDTO>();
                    List<UsuarioDTO> lista = new();
                    lista.Add(usuario);
                    _controllerResponse.Status = Constantes.OKSTATUS;
                    _controllerResponse.Response = lista;
                    return _controllerResponse;
                }
                _controllerResponse.Status = Constantes.INTERNALERRORSTATUS;
                _controllerResponse.Response = new List<UsuarioDTO>();
                return _controllerResponse;
            }
            catch (Exception e)
            {
                _controllerResponse.Status = Constantes.INTERNALERRORSTATUS;
                _controllerResponse.Response = new List<UsuarioDTO>();
                _logger.LogError(e, "An error occurred while fetching from db");
                return _controllerResponse;
            }
        }

        public async Task<ControllerResponse<bool>> VerifyEmail(ClientUserRegisterDTO modelo)
        {
            ControllerResponse<bool> _controllerResponse = new();
            try
            {
                var response = await _anonymousHttpClient.PostAsJsonAsync("api/Usuario/verificar", modelo);
                if (response.IsSuccessStatusCode)
                {
                    var respuesta = await response.Content.ReadFromJsonAsync<bool>();
                    List<bool> lista = new();
                    lista.Add(respuesta);
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

        public async Task<ControllerResponse<SharedConstants.LoginState>> Login(ClientUserLoginDTO modelo)
        {
            ControllerResponse<SharedConstants.LoginState> _controllerResponse = new();
            try
            {
                var response = await _anonymousHttpClient.PostAsJsonAsync("api/Usuario/iniciar-sesion", modelo);
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
                var response = await _anonymousHttpClient.PostAsJsonAsync("api/Usuario", modelo);
                if (response.IsSuccessStatusCode)
                {
                    var respuesta = await response.Content.ReadFromJsonAsync<bool>();
                    List<bool> lista = new();
                    lista.Add(respuesta);
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

        public async Task<ControllerResponse<UsuarioDTO>> GiveAdministrationRole(UsuarioDTO user)
        {
            ControllerResponse<UsuarioDTO> _controllerResponse = new();
            try
            {
                var response = await _http.PostAsJsonAsync("api/Usuario/permiso-admin", user);
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

        public async Task<ControllerResponse<UsuarioDTO>> GiveProfessorRole(UsuarioDTO user)
        {
            ControllerResponse<UsuarioDTO> _controllerResponse = new();
            try
            {
                var response = await _http.PostAsJsonAsync("api/Usuario/permiso-profe", user);
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

        public async Task<ControllerResponse<UsuarioDTO>> RemoveAdministrationRole(UsuarioDTO user)
        {
            ControllerResponse<UsuarioDTO> _controllerResponse = new();
            try
            {
                var response = await _http.PostAsJsonAsync("api/Usuario/eliminar-admin", user);
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

        public async Task<ControllerResponse<UsuarioDTO>> RemoveProfessorRole(UsuarioDTO user)
        {
            ControllerResponse<UsuarioDTO> _controllerResponse = new();
            try
            {
                var response = await _http.PostAsJsonAsync("api/Usuario/eliminar-profe", user);
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

        public async Task<ControllerResponse<UsuarioDTO>> UpdateUser(UsuarioDTO user)
        {
            {
                ControllerResponse<UsuarioDTO> _controllerResponse = new();
                try
                {
                    var response = await _http.PutAsJsonAsync("api/Usuario", user);
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
}
