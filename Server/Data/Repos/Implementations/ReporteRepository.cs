using System.Collections.Generic;
using System.Linq;
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
            string sql = "insert into reporte (id, IdUsuario, IdNivel, CantidadErrores, Puntuacion, Fecha) values (@Id, @IdUsuario, @IdNivel, @CantidadErrores, @Puntuacion, @Fecha);";
            await _dbContext.SaveData(sql, new { Id = r.Id, IdUsuario = r.IdUsuario, IdNivel = r.IdNivel, CantidadErrores = r.CantidadErrores, Puntuacion = r.Puntuacion, Fecha = r.Fecha }, ConectionString);
        }

        //DELETE
        public async Task DeleteReporteById(int ReporteId)
        {
            string sql = "delete from reporte where id = @id";
            await _dbContext.SaveData(sql, new { id = ReporteId }, ConectionString);
        }
        
        //POST
        public async Task<bool> CheckIfIDIsAvailable(int ID)
        {
            string sql = "SELECT * FROM reporte WHERE id = @reportId";
            var reportes = await _dbContext.LoadData<ReporteModel, dynamic>(sql, new { reportId = ID }, ConectionString);
            return reportes.Any();
        }
    }
}
