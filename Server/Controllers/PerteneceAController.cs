using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Horrografia.Server.Data.Repos.Interfaces;
using Horrografia.Shared.Models;


namespace Horrografia.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerteneceAController : ControllerBase
    {
        private readonly IPerteneceARepository _repo;
        public PerteneceAController(IPerteneceARepository repo)
        {
            _repo = repo;
        }

        // GET: api/<PerteneceAController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<PerteneceAController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PerteneceAController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PerteneceAController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PerteneceAController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
