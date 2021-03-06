using System.Collections.Generic;
using System.Threading.Tasks;
using Horrografia.Shared.Models;
using DataLibrary;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Horrografia.Server.Data.Repos.Interfaces;

namespace Horrografia.Server.Data.Repos.Implementations
{
    public class PistaRepository : IPistaRepository
    {
        private readonly IDataAccess _dbContext;
        private readonly string ConectionString;

        public PistaRepository(IDataAccess dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            ConectionString = configuration.GetConnectionString("DefaultConnection");
        }

        //GET
        public async Task<List<PistaModel>> GetAllAsync()
        {
            string sql = "select * from pista";
            var pistas = await _dbContext.LoadData<PistaModel, dynamic>(sql, new { }, ConectionString);
            return pistas;
        }

        //POST
        public async Task InsertData(PistaModel p)
        {
            string sql = "insert into pista (Pista) values (@Pista);";
            await _dbContext.SaveData(sql, new { Pista = p.Pista }, ConectionString);
        }

        //DELETE
        public async Task DeletePistaById(int PistaId)
        {
            string sql = "delete from pista where id = @id";
            await _dbContext.SaveData(sql, new { id = PistaId }, ConectionString);
        }

        public async Task UpdateData(PistaModel p)
        {   
            string sql = "update pista set Pista = @Pista where id = @id";
            await _dbContext.SaveData(sql, new { Pista = p.Pista, id = p.Id}, ConectionString);
        }
    }
}
