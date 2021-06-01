using Horrografia.Client.Shared.Objects;
using Horrografia.Shared.Models;
using System.Threading.Tasks;

namespace Horrografia.Client.Data.Services.Interfaces
{
    public interface INivelService
    {
        Task<ControllerResponse<NivelModel>> GetAsync();
        Task<ControllerResponse<NivelModel>> PostAsync(NivelModel n);
    }
}