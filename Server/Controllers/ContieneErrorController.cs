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
    public class ContieneErrorController : ControllerBase
    {

        private readonly IContieneErrorRepository _repo;
        public ContieneErrorController(IContieneErrorRepository repo)
        {
            _repo = repo;
        }

        // GET: api/<ContieneErrorController>
        [HttpGet]
        public List<ContieneErrorModel> Get()
        {
            return _repo.GetAllAsync();
        }

        // GET api/<ContieneErrorController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return _repo.GetErroresByReporteId(id);
        }

        // POST api/<ContieneErrorController>
        [HttpPost]
        public void Post(List<ContieneErrorModel> clist)
        {
           return _repo.InsertMultiple(clist);
        }
    }
}
