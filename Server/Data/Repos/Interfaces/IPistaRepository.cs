using Horrografia.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Horrografia.Server.Data.Repos.Interfaces
{
    public interface IPistaRepository
    {
        Task DeletePistaById(int PistaId);
        Task<List<PistaModel>> GetAllAsync();
        Task InsertData(PistaModel p);
        Task UpdateData(PistaModel p);
    }
}