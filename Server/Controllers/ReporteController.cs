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
    public class ReporteController : ControllerBase
    {
        private readonly IReporteRepository _repo;
        public ReporteController(IReporteRepository repo)
        {
            _repo = repo;
        }

        // GET: api/<ReporteController>
        [HttpGet]
        public List<ReporteModel> Get()
        {
            return _repo.GetAllAsync().Result;
        }

        // GET api/<ReporteController>/5
        [HttpGet("{id}")]
        public List<ReporteModel> Get(int id)
        {
            return _repo.GetUserReports(id).Result;
        }

        // POST api/<ReporteController>
        [HttpPost]
        public void Post(ReporteModel r)
        {
            _repo.InsertData(r);
        }

        // DELETE api/<ReporteController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _repo.DeleteReporteById(id);
        }
    }
}
