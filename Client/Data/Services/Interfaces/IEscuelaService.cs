using Horrografia.Client.Shared.Objects;
using Horrografia.Shared.Models;
using System.Threading.Tasks;


namespace Horrografia.Client.Data.Services.Interfaces
{
    public interface IEscuelaService
    {
        Task<ControllerResponse<EscuelaModel>> GetAsync();
        Task<ControllerResponse<EscuelaModel>> PostAsync(EscuelaModel school);
        Task<ControllerResponse<bool>> VerificarExistenciaDeEscuela(string schoolId);
    }
}
