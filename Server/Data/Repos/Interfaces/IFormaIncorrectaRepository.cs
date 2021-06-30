using Horrografia.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Horrografia.Server.Data.Repos.Interfaces
{
    public interface IFormaIncorrectaRepository
    {
        Task DeleteForma(FormaIncorrecta f);
        Task<List<FormaIncorrecta>> GetAllAsync();
        Task<List<FormaIncorrecta>> GetFormasByLevelId(int idNivel);
        Task InsertData(FormaIncorrecta f);
    }
}
