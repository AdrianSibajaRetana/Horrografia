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
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository _repo;
        public PersonController(IPersonRepository repo)
        {
            _repo = repo;
        }

        // GET: api/Person
        [HttpGet]
        public List<PersonModel> Get()
        {
            return _repo.GetAllAsync().Result;
        }

        // GET: api/Person/{int id}
        [HttpGet("{id}")]
        public PersonModel Get(int id)
        {
            return _repo.GetPersonById(id).Result;
        }

        // POST api/Person/{Person p}
        [HttpPost]
        public void Post(PersonModel val)
        {
            _repo.InsertData(val);
        }

        // PUT api/<PersonController>/5
        [HttpPut("{id}")]
        public void Put(int id, PersonModel p)
        {
            string FirstName = p.FirstName;
            string LastName = p.LastName;
            _repo.UpdatePersonById(id, FirstName, LastName);
        }

        // DELETE api/<PersonController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _repo.DeletePersonById(id);
        }
    }
}
