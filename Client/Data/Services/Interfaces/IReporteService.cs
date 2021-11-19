using Horrografia.Client.Shared.Objects;
using Horrografia.Shared.Models;
using System.Threading.Tasks;

namespace Horrografia.Client.Data.Services.Interfaces
{
    public interface IReporteService
    {
        Task<ControllerResponse<ReporteModel>> GetAsync();
        
        Task<ControllerResponse<ReporteModel>> GetUserReportsById(string UserID);
        
        Task<ControllerResponse<ReporteModel>> PostAsync(ReporteModel r);
        
        Task<ControllerResponse<ReporteModel>> DeleteAsync(ReporteModel r);
        
        Task<ControllerResponse<bool>> VerifyReportID(int ID);
    }
}