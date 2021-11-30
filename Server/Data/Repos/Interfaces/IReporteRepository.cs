using System;
using Horrografia.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Horrografia.Server.Data.Repos.Interfaces
{
    public interface IReporteRepository
    {
        Task DeleteReporteById(int ReporteId);
        Task<List<ReporteModel>> GetAllAsync();
        Task<List<ReporteModel>> GetUserReports(string IdUsuario);
        Task InsertData(ReporteModel r);
        Task<DateTime> GetReportDate(int reportID);
        Task<bool> CheckIfIDIsAvailable(int ID);
    }
}