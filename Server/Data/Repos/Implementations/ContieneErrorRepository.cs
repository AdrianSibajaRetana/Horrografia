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
            string sql = "SELECT * FROM ContieneError";
            var errores = await _dbContext.LoadData<ContieneErrorModel, dynamic>(sql, new { }, ConectionString);
            return errores;
        }

        //GET/{IdReporte}
        public async List<Task<ContieneErrorModel>> GetErroresByReporteId(int idReporte)
        {
            string sql = "SELECT * FROM ContieneError WHERE idReporte = @idReporte";
            var errores = await _dbContext.LoadData<ContieneErrorModel, dynamic>(sql, new { idReporte = idReporte }, ConectionString);
            return errores;
        }

        //POST
        //CONSIDERAR BULK INSERT
        public async Task InsertMultiple(List<ContieneErrorModel> c)
        {
            foreach (ContieneErrorModel error in c)
            {
                string sql = "insert into ContieneError (idReporte, idItem, respuesta) values (@idReporte, @idItem, @respuesta);";
                await _dbContext.SaveData(sql, new { idReporte = error.IdReporte, idItem = error.IdItem, respuesta = error.Respuesta }, ConectionString);
            }
        }

    }
}
