using System.Collections.Generic;
using System.Threading.Tasks;
using Horrografia.Shared.Models;
using DataLibrary;
using Microsoft.Extensions.Configuration;
using Horrografia.Server.Data.Repos.Interfaces;
using System.Linq;

namespace Horrografia.Server.Data.Repos.Implementations
{
    public class PerteneceARepository : IPerteneceARepository
    {
        private readonly IDataAccess _dbContext;
        private readonly string ConectionString;

        public PerteneceARepository(IDataAccess dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            ConectionString = configuration.GetConnectionString("DefaultConnection");
        }

        //GET
        public async Task<List<PerteneceAModel>> GetAllAsync()
        {
            string sql = "SELECT * FROM pertenecea";
            var perteneceRelaciones = await _dbContext.LoadData<PerteneceAModel, dynamic>(sql, new { }, ConectionString);
            return perteneceRelaciones;
        }

        //GET/{IdPista}
        public async Task<List<PerteneceAModel>> GetPerteneceAByLevelId(int idNivel)
        {
            string sql = "SELECT * FROM pertenecea WHERE idNivel = @idNivel";
            var perteneceRelaciones = await _dbContext.LoadData<PerteneceAModel, dynamic>(sql, new { idNivel = idNivel }, ConectionString);
            return perteneceRelaciones;
        }

        //POST
        public async Task InsertData(PerteneceAModel p)
        {
            string sql = "insert into pertenecea (idNivel, idItem) values (@idNivel, @idItem);";
            await _dbContext.SaveData(sql, new { idNivel = p.IdNivel, idItem = p.IdItem }, ConectionString);
        }

        //DELETE
        public async Task DeleteRelation(PerteneceAModel p)
        {
            string sql = "delete from pertenecea where idNivel = @idNivel and idItem = @idItem";
            await _dbContext.SaveData(sql, new { idNivel = p.IdNivel, idItem = p.IdItem }, ConectionString);
        }

    }
}
