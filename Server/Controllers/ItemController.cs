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
    public class ItemController : ControllerBase
    {
        /*Controlador de la tabla Item
            Métodos: 
                    - Get All
                    - Get Item By Id
                    - Insert Item
                    - Update Item
                    - Delete Item 
         */
        private readonly IItemRepository _repo;
        public ItemController(IItemRepository repo)
        {
            _repo = repo;
        }

        // GET: api/Item
        [HttpGet]
        public List<ItemModel> Get()
        {
            return _repo.GetAllAsync().Result;
        }

        // GET api/<ItemController>/5
        [HttpGet("{id}")]
        public ItemModel Get(int id)
        {
            return _repo.GetItemById(id).Result;
        }

        // POST api/<ItemController>
        [HttpPost]
        public void Post(ItemModel p)
        {
            _repo.InsertData(p);
        }

        // PUT api/<ItemController>/5
        [HttpPut("{id}")]
        public void Put(ItemModel i)
        {
            _repo.UpdateData(i);
        }

        // DELETE api/<ItemController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _repo.DeleteItem(id);
        }
    }
}
