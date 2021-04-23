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
    public class PistaController : ControllerBase
    {
        private readonly IPistaRepository _repo;
        public PistaController(IPistaRepository repo)
        {
            _repo = repo;
        }

        // GET: api/<PistaController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<PistaController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PistaController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PistaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PistaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
