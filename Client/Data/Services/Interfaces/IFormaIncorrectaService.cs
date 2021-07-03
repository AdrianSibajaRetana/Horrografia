using Horrografia.Client.Shared.Objects;
using Horrografia.Shared.Models;
using System.Threading.Tasks;

namespace Horrografia.Client.Data.Services.Interfaces
{
    public interface IFormaIncorrectaService
    {
        Task<ControllerResponse<FormaIncorrecta>> GetAsync();
        Task<ControllerResponse<FormaIncorrecta>> GetFormasIncorrectasFromLevelId(int nivelid);
        Task<ControllerResponse<FormaIncorrecta>> PostAsync(FormaIncorrecta f);
        Task<ControllerResponse<FormaIncorrecta>> DeleteAsync(FormaIncorrecta f);
    }
}
