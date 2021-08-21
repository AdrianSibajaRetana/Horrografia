using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Horrografia.Server.Data.Repos.Interfaces;
using Horrografia.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Horrografia.Client.Shared.Objects.ClientModels;

namespace Horrografia.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private readonly ILogger<UsuarioController> _logger;
        private readonly IUsuariosRepository _repo;

        public UsuarioController(IUsuariosRepository repo, ILogger<UsuarioController> logger)
        {
            _repo = repo;
            _logger = logger;
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var usuarios = await _repo.GetAllAsync();
                return Ok(usuarios);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while fetching from db");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Authorize]
        [Route("getData/{id}")]
        public async Task<IActionResult> GetbyId(string id)
        {
            try
            {
                var usuario = await _repo.GetUserById(id);
                return Ok(usuario);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while fetching from db");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("verificar")]
        public async Task<IActionResult> VerifyEmail(ClientUserRegisterDTO modelo)
        {
            try
            {                
                var niveles = await _repo.CorreoDisponible(modelo.Email);
                return Ok(niveles);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while fetching from db");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post(ClientUserRegisterDTO usuarioACrear)
        {
            try
            {
                var resultado = await _repo.CrearUsuario(usuarioACrear);
                return Ok(resultado);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while writing to db");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("iniciar-sesion")]
        public async Task<IActionResult> IniciarSesion(ClientUserLoginDTO modeloEnviado)
        {
            try
            {
                var resultado = await _repo.IniciarSesion(modeloEnviado);
                return Ok(resultado);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while writing to db");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("cerrar-sesion")]
        public async Task<IActionResult> CerrarSesion()
        {
            try
            {
                await _repo.CerrarSesion();
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while writing to db");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("permiso-admin")]
        public async Task<IActionResult> OtorgarPermisoAdministrador(UsuarioDTO usuario)
        {
            try
            {
                await _repo.OtorgarPermisoAdministrador(usuario);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while writing to db");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("permiso-profe")]
        public async Task<IActionResult> OtorgarPermisoProfesor(UsuarioDTO usuario)
        {
            try
            {
                await _repo.OtorgarPermisoProfesor(usuario);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while writing to db");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("eliminar-admin")]
        public async Task<IActionResult> RemoverPermisoAdministrador(UsuarioDTO usuario)
        {
            try
            {
                await _repo.RemoverPermisoAdministrador(usuario);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while writing to db");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("eliminar-profe")]
        public async Task<IActionResult> RemoverPermisoProfesor(UsuarioDTO usuario)
        {
            try
            {
                await _repo.RemoverPermisoProfesor(usuario);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while writing to db");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
