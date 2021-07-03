using Horrografia.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Horrografia.Server.Data.Repos.Interfaces
{
    public interface IFormaIncorrectaRepository
    {
        Task DeleteForma(FormaIncorrectaModel f);
        Task<List<FormaIncorrectaModel>> GetAllAsync();
        Task<List<FormaIncorrectaModel>> GetFormasByLevelId(int idNivel);
        Task InsertData(FormaIncorrectaModel f);
    }
}
