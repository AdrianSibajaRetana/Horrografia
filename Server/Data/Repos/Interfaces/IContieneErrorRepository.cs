using Horrografia.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Horrografia.Server.Data.Repos.Interfaces
{
    public interface IContieneErrorRepository
    {
        Task<List<ContieneErrorModel>> GetAllAsync();
        List<Task<ContieneErrorModel>> GetErroresByReporteId(int idReporte);
        Task InsertData(ContieneErrorModel c);
        Task InsertMultiple(List<ContieneErrorModel> c);
    }
}