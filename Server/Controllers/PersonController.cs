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
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository _repo;
        private readonly ILogger<PersonController> _logger;
        public PersonController(IPersonRepository repo)
        {
            _repo = repo;
        }

        // GET: api/Person
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var persons = _repo.GetAllAsync().Result;
                return Ok(persons);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while fetching from db");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        // GET: api/Person/{int id}
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var person = _repo.GetPersonById(id).Result;
                if (person == null)
                {
                    return NotFound();
                }
                return Ok(person);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while fetching from db");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        // POST api/Person/{Person p}
        [HttpPost]
        public IActionResult Post(PersonModel val)
        {
            try
            {
                _repo.InsertData(val);                
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while posting to db");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            
        }

        // PUT api/<PersonController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, PersonModel p)
        {
            try
            {
                string FirstName = p.FirstName;
                string LastName = p.LastName;
                _repo.UpdatePersonById(id, FirstName, LastName);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while updating to db");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        // DELETE api/<PersonController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _repo.DeletePersonById(id);
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
