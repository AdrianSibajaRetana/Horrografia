using System;
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
            string sql = "select * from reporte ORDER BY Fecha DESC";
            var reportes = await _dbContext.LoadData<ReporteModel, dynamic>(sql, new { }, ConectionString);
            foreach (var report in reportes)
            {
                report.TransformFechaToString();
            }
            return reportes;
        }

        //GET/{IdUsuario}
        public async Task<List<ReporteModel>> GetUserReports(string IdUsuario)
        {
            string sql = "SELECT * FROM reporte WHERE IdUsuario = @id ORDER BY Fecha DESC";
            var reportes = await _dbContext.LoadData<ReporteModel, dynamic>(sql, new { id = IdUsuario }, ConectionString);
            foreach (var report in reportes)
            {
                report.TransformFechaToString();
            }
            return reportes;
        }
        
        
        public async Task<DateTime> GetReportDate(int reportID)
        {
            string sql = "SELECT Fecha FROM reporte WHERE id = @id";
            var date = await _dbContext.LoadData<DateTime, dynamic>(sql, new { id = reportID }, ConectionString);
            return date.FirstOrDefault();
        }

        //POST
        public async Task InsertData(ReporteModel r)
        {
            string sql = "insert into reporte (id, IdUsuario, IdNivel, CantidadErrores, Puntuacion, Fecha) values (@Id, @IdUsuario, @IdNivel, @CantidadErrores, @Puntuacion, @Fecha);";
            await _dbContext.SaveData(sql, new { Id = r.Id, IdUsuario = r.IdUsuario, IdNivel = r.IdNivel, CantidadErrores = r.CantidadErrores, Puntuacion = r.Puntuacion, Fecha = r.FechaString }, ConectionString);
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
