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
        public List<PistaModel> Get()
        {
            return _repo.GetAllAsync().Result;
        }

        // GET api/<PistaController>/5
        [HttpGet("{id}")]
        public PistaModel Get(int id)
        {
            return _repo.GetPistaById(id).Result;
        }

        // POST api/<PistaController>
        [HttpPost]
        public void Post(PistaModel p)
        {
            _repo.InsertData(p);
        }

        // DELETE api/<PistaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _repo.DeletePistaById(id);
        }
    }
}
