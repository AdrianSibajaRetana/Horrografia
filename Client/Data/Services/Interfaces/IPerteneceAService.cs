using Horrografia.Client.Shared.Objects;
using Horrografia.Shared.Models;
using System.Threading.Tasks;

namespace Horrografia.Client.Data.Services.Interfaces
{
    public interface IPerteneceAService
    {
        Task<ControllerResponse<PerteneceAModel>> GetAsync();
        Task<ControllerResponse<PerteneceAModel>> PostAsync(PerteneceAModel p);
        Task<ControllerResponse<PerteneceAModel>> DeleteAsync(PerteneceAModel p);
    }
}
