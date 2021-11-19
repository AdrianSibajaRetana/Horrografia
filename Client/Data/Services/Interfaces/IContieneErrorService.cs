using Horrografia.Client.Shared.Objects;
using Horrografia.Shared.Models;
using System.Threading.Tasks;

namespace Horrografia.Client.Data.Services.Interfaces
{
    public interface IContieneErrorService
    {
        Task<ControllerResponse<ContieneErrorModel>> GetAsync();
        Task<ControllerResponse<ContieneErrorModel>> GetByReporteIDAsync(int ID);
        Task<ControllerResponse<ContieneErrorModel>> PostAsync(ContieneErrorModel error);
    }
}