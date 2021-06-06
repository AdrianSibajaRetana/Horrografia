using Horrografia.Client.Shared.Objects;
using Horrografia.Shared.Models;
using System.Threading.Tasks;

namespace Horrografia.Client.Data.Services.Interfaces
{
    public interface IPistaService
    {
        Task<ControllerResponse<PistaModel>> GetAsync();
        Task<ControllerResponse<PistaModel>> PostAsync(PistaModel p);
        Task<ControllerResponse<PistaModel>> DeleteAsync(PistaModel p);
        Task<ControllerResponse<PistaModel>> UpdateAsync(PistaModel p);
    }
}
