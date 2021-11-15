using System.Collections.Generic;
using System.Threading.Tasks;
using Horrografia.Shared.Models;
using DataLibrary;
using Microsoft.Extensions.Configuration;
using Horrografia.Server.Data.Repos.Interfaces;

namespace Horrografia.Server.Data.Repos.Implementations
{
    public class ReporteRepository : IReporteRepository
    {
        private readonly IDataAccess _dbContext;
        private readonly string ConectionString;

        public ReporteRepository(IDataAccess dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            ConectionString = configuration.GetConnectionString("DefaultConnection");
        }

        //GET
        public async Task<List<ReporteModel>> GetAllAsync()
        {
            string sql = "select * from reporte";
            var reportes = await _dbContext.LoadData<ReporteModel, dynamic>(sql, new { }, ConectionString);
            return reportes;
        }

        //GET/{IdUsuario}
        public async Task<List<ReporteModel>> GetUserReports(string IdUsuario)
        {
            string sql = "SELECT * FROM reporte WHERE IdUsuario = @id";
            var reportes = await _dbContext.LoadData<ReporteModel, dynamic>(sql, new { id = IdUsuario }, ConectionString);
            return reportes;
        }

        //POST
        public async Task InsertData(ReporteModel r)
        {
            string sql = "insert into reporte (IdUsuario, IdNivel, CantidadErrores, Puntuacion) values (@IdUsuario, @IdNivel, @CantidadErrores, @Puntuacion);";
            await _dbContext.SaveData(sql, new { IdUsuario = r.IdUsuario, IdNivel = r.IdNivel, CantidadErrores = r.CantidadErrores, Puntuacion = r.Puntuacion }, ConectionString);
        }

        //DELETE
        public async Task DeleteReporteById(int ReporteId)
        {
            string sql = "delete from reporte where id = @id";
            await _dbContext.SaveData(sql, new { id = ReporteId }, ConectionString);
        }
    }
}
