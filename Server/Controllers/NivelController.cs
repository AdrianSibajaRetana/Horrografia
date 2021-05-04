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
        public IActionResult Get()
        {
            try
            {
                var niveles = _repo.GetAllAsync().Result;
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
        public IActionResult Post(NivelModel n)
        {
            try
            {
                _repo.InsertData(n);
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
        public IActionResult Put(NivelModel n)
        {
            try
            {
                _repo.UpdateData(n);
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
        public IActionResult Delete(int id)
        {
            try
            {
                _repo.DeleteNivel(id);
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
