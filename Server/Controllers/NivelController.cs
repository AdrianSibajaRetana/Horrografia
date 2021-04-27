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
    public class NivelController : ControllerBase
    {
        /*Controlador de la tabla Nivel
            Métodos: 
                    - Get All
                    - Insert Item
                    - Update Item
                    - Delete Item 
         */
        private readonly INivelRepository _repo;
        public NivelController(INivelRepository repo)
        {
            _repo = repo;
        }

        // GET: api/Nivel
        [HttpGet]
        public List<NivelModel> Get()
        {
            return _repo.GetAllAsync().Result;
        }

        // POST api/<NivelController>
        [HttpPost]
        public void Post(NivelModel n)
        {
            _repo.InsertData(n);
        }

        // PUT api/<NivelController>/5
        [HttpPut]
        public void Put(NivelModel n)
        {
            _repo.UpdateData(n);
        }

        // DELETE api/<NivelController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _repo.DeleteNivel(id);
        }
    }
}
