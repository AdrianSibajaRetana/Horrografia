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
    public class PerteneceAController : ControllerBase
    {
        /*Controlador de la tabla Nivel
            Métodos: 
                    - Get All
                    - Get by Id
                    - Insert Item
                    - Delete Item 
         */
        private readonly IPerteneceARepository _repo;
        private readonly ILogger<PerteneceAController> _logger;
        public PerteneceAController(IPerteneceARepository repo, ILogger<PerteneceAController> logger)
        {
            _repo = repo;
            _logger = logger; 
        }

        // GET: api/<PerteneceAController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var relations = await _repo.GetAllAsync();
                return Ok(relations);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while fetching from db");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        // GET api/<PerteneceAController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var levelRelations = await _repo.GetPerteneceAByLevelId(id);
                if (levelRelations == null)
                {
                    return NotFound();
                }
                return Ok(levelRelations);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while fetching from db");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        // POST api/<PerteneceAController>
        [HttpPost]
        public async Task<IActionResult> Post(PerteneceAModel p)
        {
            try
            {
                await _repo.InsertData(p);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while posting to db");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        // DELETE api/<PerteneceAController>/idNivel/idItem
        [HttpDelete("{idNivel}/{idItem}")]
        public async Task<IActionResult> Delete(int idNivel, int idItem)
        {
            try
            {
                PerteneceAModel p = new();
                p.IdItem = idItem;
                p.IdNivel = idNivel;
                await _repo.DeleteRelation(p);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while updating to db");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }   
        }
    }
}
