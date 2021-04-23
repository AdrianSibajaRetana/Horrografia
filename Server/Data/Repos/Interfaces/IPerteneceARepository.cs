using Horrografia.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Horrografia.Server.Data.Repos.Interfaces
{
    public interface IPerteneceARepository
    {
        Task DeleteRelation(PerteneceAModel p);
        Task<List<PerteneceAModel>> GetAllAsync();
        Task<List<PerteneceAModel>> GetPerteneceAByLevelId(int idNivel);
        Task InsertData(PerteneceAModel p);
    }
}