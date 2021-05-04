using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Horrografia.Server.Data.Repos.Interfaces;
using Horrografia.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Horrografia.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NivelController : ControllerBase
    {
        /*Controlador de la tabla Nivel
            Métodos: 
                    - Get All
                    - Insert Item
                    - Update Item
                    - Delete Item 
         */
        private readonly INivelRepository _repo;
        private readonly ILogger<NivelController> _logger;
        public NivelController(INivelRepository repo, ILogger<NivelController> logger)
        {
            _repo = repo;
            _logger = logger;

        }

        // GET: api/Nivel
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var niveles = await _repo.GetAllAsync();
                return Ok(niveles);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while fetching from db");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        // POST api/<NivelController>
        [HttpPost]
        public async Task<IActionResult> Post(NivelModel n)
        {
            try
            {
                await _repo.InsertData(n);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while writing to db");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        // PUT api/<NivelController>/5
        [HttpPut]
        public async Task<IActionResult> Put(NivelModel n)
        {
            try
            {
                await _repo.UpdateData(n);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while updating to db");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        // DELETE api/<NivelController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _repo.DeleteNivel(id);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while deleting from db");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

        }
    }
}
