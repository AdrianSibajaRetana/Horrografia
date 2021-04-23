using Horrografia.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Horrografia.Server.Data.Repos.Interfaces
{
    public interface IPistaRepository
    {
        Task DeletePistaById(int PistaId);
        Task<List<PistaModel>> GetAllAsync();
        Task<PistaModel> GetPistaById(int idPista);
        Task InsertData(PistaModel p);
    }
}