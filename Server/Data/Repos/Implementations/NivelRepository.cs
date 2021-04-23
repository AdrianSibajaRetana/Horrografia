using System.Collections.Generic;
using System.Threading.Tasks;
using Horrografia.Shared.Models;
using DataLibrary;
using Microsoft.Extensions.Configuration;
using Horrografia.Server.Data.Repos.Interfaces;

namespace Horrografia.Server.Data.Repos.Implementations
{
    public class NivelRepository : INivelRepository
    {
        private readonly IDataAccess _dbContext;
        private readonly string ConectionString;

        public NivelRepository(IDataAccess dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            ConectionString = configuration.GetConnectionString("DefaultConnection");
        }

        //GET
        public async Task<List<NivelModel>> GetAllAsync()
        {
            string sql = "SELECT * FROM nivel";
            var niveles = await _dbContext.LoadData<NivelModel, dynamic>(sql, new { }, ConectionString);
            return niveles;
        }

        //GET/{IdPista}
        public async Task<NivelModel> GetNivelById(int idNivel)
        {
            string sql = "SELECT * FROM nivel WHERE id = @idNivel";
            var nivel = await _dbContext.LoadData<NivelModel, dynamic>(sql, new { idNivel = idNivel }, ConectionString);
            return nivel;
        }

        //POST
        public async Task InsertData(NivelModel n)
        {
            string sql = "insert into nivel (Descripcion, ErroresPermitidos, NumeroDeItems) values (@Descripcion, @ErroresPermitidos, @NumeroDeItems);";
            await _dbContext.SaveData(sql, new { Descripcion = n.Descripcion, ErroresPermitidos = n.ErroresPermitidos, NumeroDeItems = n.NumeroDeItems }, ConectionString);
        }

        //DELETE
        public async Task DeleteNivel(int idNivel)
        {
            string sql = "delete from nivel where id = @idNivel;
            await _dbContext.SaveData(sql, new { idNivel = idNivel }, ConectionString);
        }
    }
