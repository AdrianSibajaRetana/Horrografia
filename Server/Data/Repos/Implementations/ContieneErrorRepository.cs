using System.Collections.Generic;
using System.Threading.Tasks;
using Horrografia.Shared.Models;
using DataLibrary;
using Microsoft.Extensions.Configuration;
using Horrografia.Server.Data.Repos.Interfaces;
using System.Linq;

namespace Horrografia.Server.Data.Repos.Implementations
{
    public class ContieneErrorRepository : IContieneErrorRepository
    {
        private readonly IDataAccess _dbContext;
        private readonly string ConectionString;

        public ContieneErrorRepository(IDataAccess dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            ConectionString = configuration.GetConnectionString("DefaultConnection");
        }

        //GET
        public async Task<List<ContieneErrorModel>> GetAllAsync()
        {
            string sql = "SELECT * FROM contieneerror";
            var errores = await _dbContext.LoadData<ContieneErrorModel, dynamic>(sql, new { }, ConectionString);
            return errores;
        }

        //GET/{IdReporte}
        public async Task<List<ContieneErrorModel>> GetErroresByReporteId(int idReporte)
        {
            string sql = "SELECT * FROM contieneerror WHERE idReporte = @idReporte";
            var errores = await _dbContext.LoadData<ContieneErrorModel, dynamic>(sql, new { idReporte = idReporte }, ConectionString);
            return errores;
        }

        //POST
        //CONSIDERAR BULK INSERT
        public async Task InsertData(ContieneErrorModel error)
        {
            string sql = "insert into contieneerror (idReporte, idItem, respuesta) values (@idReporte, @idItem, @respuesta);";
            await _dbContext.SaveData(sql, new { idReporte = error.idReporte, idItem = error.idItem, respuesta = error.respuesta }, ConectionString);
        }

    }
}
