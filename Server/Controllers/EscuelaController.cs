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
    public class EscuelaController : ControllerBase
    {
        private readonly IEscuelaRepository _repo;
        private readonly ILogger<EscuelaController> _logger;
        public EscuelaController(IEscuelaRepository repo, ILogger<EscuelaController> logger)
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
                var items = await _repo.GetAllAsync();
                return Ok(items);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while fetching from db");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post(EscuelaModel s)
        {
            try
            {
                await _repo.InsertData(s);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while writing to db");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Authorize]
        [Route("check/{id}")]
        public async Task<IActionResult> CheckExistence(string id)
        {
            try
            {
                var existence = await _repo.CheckSchoolExistance(id);
                return Ok(existence);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while fetching from db");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetBySchoolCode(string id)
        {
            try
            {
                var items = await _repo.GetSchoolByCode(id);
                return Ok(items);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while fetching from db");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
