using Horrografia.Client.Shared.Objects;
using Horrografia.Shared.Models;
using System.Threading.Tasks;

namespace Horrografia.Client.Data.Services.Interfaces
{
    public interface IFormaIncorrectaService
    {
        Task<ControllerResponse<FormaIncorrectaModel>> GetAsync();
        Task<ControllerResponse<FormaIncorrectaModel>> GetFormasIncorrectasFromLevelId(int nivelid);
        Task<ControllerResponse<FormaIncorrectaModel>> PostAsync(FormaIncorrectaModel f);
        Task<ControllerResponse<FormaIncorrectaModel>> DeleteAsync(FormaIncorrectaModel f);
    }
}
