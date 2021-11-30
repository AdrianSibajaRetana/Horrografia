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

namespace Horrografia.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContieneErrorController : ControllerBase
    {
        /*Controlador de la relación ContieneError
            Métodos: 
                    - Get All
                    - Get By Reporte Id
                    - Insertar múltiples
         */
        private readonly IContieneErrorRepository _repo;
        private readonly ILogger<ContieneErrorController> _logger;
        public ContieneErrorController(IContieneErrorRepository repo, ILogger<ContieneErrorController> logger)
        {
            _repo = repo;
            _logger = logger; 
        }

        // GET: api/<ContieneErrorController>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var errores = await _repo.GetAllAsync();
                return Ok(errores);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while fetching from db");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        // GET api/<ContieneErrorController>/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var errores = await _repo.GetErroresByReporteId(id);
                if (errores == null)
                {
                    return NotFound();
                }
                return Ok(errores);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while fetching from db");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        // POST api/<ContieneErrorController>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post(ContieneErrorModel c)
        {
            try
            {
                await _repo.InsertData(c);
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
