using Horrografia.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Horrografia.Server.Data.Repos.Interfaces
{
    public interface INivelRepository
    {
        Task DeleteNivel(int idNivel);
        Task<List<NivelModel>> GetAllAsync();
        Task<NivelModel> GetNivelById(int idNivel);
        Task InsertData(NivelModel n);
    }
}