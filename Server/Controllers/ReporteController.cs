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
    public class ReporteController : ControllerBase
    {
        private readonly IReporteRepository _repo;
        private readonly ILogger<ReporteController> _logger;
        public ReporteController(IReporteRepository repo, ILogger<ReporteController> logger)
        {
            _repo = repo;
            _logger = logger; 
        }

        // GET: api/<ReporteController>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var reportes = await _repo.GetAllAsync();
                return Ok(reportes);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while fetching from db");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        // GET api/<ReporteController>/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                var reportes = await _repo.GetUserReports(id);
                if (reportes == null)
                {
                    return NotFound();
                }
                return Ok(reportes);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while fetching from db");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        // POST api/<ReporteController>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post(ReporteModel r)
        {
            try
            {
                await _repo.InsertData(r);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while posting to db");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }    
        }

        // DELETE api/<ReporteController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _repo.DeleteReporteById(id);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while deleting from db");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
        
        [HttpGet("verificar/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyReportID(int id)
        {
            try
            {
                var isAvailable = await _repo.CheckIfIDIsAvailable(id);
                return Ok(isAvailable);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while fetching from db");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
