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
    public class PistaController : ControllerBase
    {
        private readonly IPistaRepository _repo;
        private readonly ILogger<PistaController> _logger;
        public PistaController(IPistaRepository repo, ILogger<PistaController> logger)
        {
            _repo = repo;
            _logger = logger; 
        }

        // GET: api/<PistaController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var pistas = await _repo.GetAllAsync();
                return Ok(pistas);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while fetching from db");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        // GET api/<PistaController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var pista = await _repo.GetPistaById(id);
                if (pista == null)
                {
                    return NotFound();
                }
                return Ok(pista);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while fetching from db");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        // POST api/<PistaController>
        [HttpPost]
        public async Task<IActionResult> Post(PistaModel p)
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

        // DELETE api/<PistaController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _repo.DeletePistaById(id);
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
