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
    }
}