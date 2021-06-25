﻿using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
        [AllowAnonymous]
        [Route("verificar")]
        public async Task<IActionResult> VerifyEmail(string correo)
        {
            try
            {
                var niveles = await _repo.CorreoDisponible(correo);
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
        [AllowAnonymous]
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

    }
}