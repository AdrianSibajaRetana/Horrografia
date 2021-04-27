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
        /*Controlador de la tabla Nivel
            Métodos: 
                    - Get All
                    - Get by Id
                    - Insert Item
                    - Delete Item 
         */
        private readonly IPerteneceARepository _repo;
        public PerteneceAController(IPerteneceARepository repo)
        {
            _repo = repo;
        }

        // GET: api/<PerteneceAController>
        [HttpGet]
        public List<PerteneceAModel> Get()
        {
            return _repo.GetAllAsync().Result;
        }

        // GET api/<PerteneceAController>/5
        [HttpGet("{id}")]
        public List<PerteneceAModel> Get(int id)
        {
            return _repo.GetPerteneceAByLevelId(id).Result;
        }

        // POST api/<PerteneceAController>
        [HttpPost]
        public void Post(PerteneceAModel p)
        {
            _repo.InsertData(p);
        }

        // DELETE api/<PerteneceAController>/model p
        [HttpDelete]
        public void Delete(PerteneceAModel p)
        {
            _repo.DeleteRelation(p);
        }
    }
}
