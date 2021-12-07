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
    public class TagController : ControllerBase
    {
        private readonly ITagRepository _repo;
        private readonly ILogger<TagController> _logger;

        public TagController(ITagRepository repo, ILogger<TagController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var tags = await _repo.GetAllAsync();
                return Ok(tags);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while fetching from db");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
        
        [HttpGet("todos")]
        public async Task<IActionResult> GetTagsFromReportId()
        {
            try
            {
                var tags = await _repo.GetFromAllReports();
                return Ok(tags);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while fetching from db");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
        
        [HttpGet("mensual/{month:int}/{year:int}")]
        public async Task<IActionResult> GetTagsFromMonth(int month, int year)
        {
            try
            {
                var tags = await _repo.GetFromMonthlyReports(month, year);
                return Ok(tags);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while fetching from db");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
        
        [HttpGet("anual/{year:int}")]
        public async Task<IActionResult> GetTagsFromYear(int year)
        {
            try
            {
                var tags = await _repo.GetFromYearlyReports(year);
                return Ok(tags);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while fetching from db");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
        
        [HttpGet("escuela/{schoolCode}")]
        public async Task<IActionResult> GetTagsFromSchool(string schoolCode)
        {
            try
            {
                var tags = await _repo.GetFromAllReportsFromSchool(schoolCode);
                return Ok(tags);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while fetching from db");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
        
        [HttpGet("usuario/{id}")]
        public async Task<IActionResult> GetTagsFromUser(string id)
        {
            try
            {
                var tags = await _repo.GetFromUser(id);
                return Ok(tags);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while fetching from db");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _repo.DeleteById(id);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while deleting from db");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{itemId}/{tagId}")]
        public async Task<IActionResult> DeleteRelation(int itemId, int tagId)
        {
            try
            {
                await _repo.DeleteRelation(itemId, tagId);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while deleting from db");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("{itemId}/{tagId}")]
        public async Task<IActionResult> PostRelation(int itemId, int tagId)
        {
            try
            {
                await _repo.AddTagToitem(itemId, tagId);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while fetching from db");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("relations")]
        public async Task<IActionResult> GetRelations()
        {
            try
            {
                var tags = await _repo.GetAllRelationsAsync();
                return Ok(tags);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while fetching from db");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
