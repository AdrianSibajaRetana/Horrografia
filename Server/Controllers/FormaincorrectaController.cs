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
    public class FormaincorrectaController : ControllerBase
    {
        private readonly IFormaIncorrectaRepository _repo;
        private readonly ILogger<FormaIncorrecta> _logger;
        public FormaincorrectaController(IFormaIncorrectaRepository repo, ILogger<FormaIncorrecta> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var items = await _repo.GetAllAsync();
                return Ok(items);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while fetching from db");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var item = await _repo.GetFormasByLevelId(id);
                if (item == null)
                {
                    return NotFound();
                }
                return Ok(item);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while fetching from db");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(FormaIncorrecta f)
        {
            try
            {
                await _repo.InsertData(f);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while writing to db");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{idItem}/{forma}")]
        public async Task<IActionResult> Delete(int idItem, string forma)
        {
            try
            {
                FormaIncorrecta f = new();
                f.Itemid = idItem;
                f.Forma = forma;
                await _repo.DeleteForma(f);
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
