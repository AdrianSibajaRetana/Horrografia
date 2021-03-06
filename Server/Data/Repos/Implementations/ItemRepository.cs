using System.Collections.Generic;
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

        //GET/{idNivel}
        public async Task<List<ItemModel>> GetItemsByLevelId(int idNivel)
        {
            string sql = "SELECT * FROM Item where NivelId = @idNivel";            
            var items = await _dbContext.LoadData<ItemModel, dynamic>(sql, new { idNivel = idNivel }, ConectionString);
            return items;
        }

        //POST
        public async Task InsertData(ItemModel i)
        {
            string sql = "insert into item (FormaCorrecta, PistaId, NivelId) values (@FormaCorrecta, @PistaId, @IdNivel);";
            await _dbContext.SaveData(sql, new { FormaCorrecta = i.FormaCorrecta, PistaId = i.PistaId, IdNivel = i.NivelId }, ConectionString);
        }

        //DELETE
        public async Task DeleteItem(int idItem)
        {
            string sql = "delete from item where id = @idItem";
            await _dbContext.SaveData(sql, new { idItem = idItem }, ConectionString);
        }

        //UPDATE
        public async Task UpdateData(ItemModel i)
        {
            string sql = "update item set FormaCorrecta = @FormaCorrecta, PistaId = @PistaId where id = @id";
            await _dbContext.SaveData(sql, new { FormaCorrecta = i.FormaCorrecta, PistaId = i.PistaId, id = i.Id }, ConectionString);
        }
    }
}