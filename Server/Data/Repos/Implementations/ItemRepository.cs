﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Horrografia.Shared.Models;
using DataLibrary;
using Microsoft.Extensions.Configuration;
using Horrografia.Server.Data.Repos.Interfaces;
using System.Linq;

namespace Horrografia.Server.Data.Repos.Implementations
{
    public class ItemRepository : IItemRepository
    {
        private readonly IDataAccess _dbContext;
        private readonly string ConectionString;

        public ItemRepository(IDataAccess dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            ConectionString = configuration.GetConnectionString("DefaultConnection");
        }

        //GET
        public async Task<List<ItemModel>> GetAllAsync()
        {
            string sql = "SELECT * FROM item";
            var items = await _dbContext.LoadData<ItemModel, dynamic>(sql, new { }, ConectionString);
            return items;
        }

        //GET/{IdPista}
        public async Task<ItemModel> GetItemById(int idItem)
        {
            string sql = "SELECT * FROM item WHERE id = @idItem";
            var nivel = await _dbContext.LoadData<ItemModel, dynamic>(sql, new { idItem = idItem }, ConectionString);
            return nivel.FirstOrDefault();
        }

        //POST
        public async Task InsertData(ItemModel i)
        {
            string sql = "insert into item (FormaCorrecta, FormaIncorrecta, PistaId) values (@FormaCorrecta, @FormaIncorrecta, @PistaId);";
            await _dbContext.SaveData(sql, new { FormaCorrecta = i.FormaCorrecta, FormaIncorrecta = i.FormaIncorrecta, PistaId = i.PistaId }, ConectionString);
        }

        //DELETE
        public async Task DeleteItem(int idItem)
        {
            string sql = "delete from item where id = @idItem;
            await _dbContext.SaveData(sql, new { idItem = idItem }, ConectionString);
        }
    }
}