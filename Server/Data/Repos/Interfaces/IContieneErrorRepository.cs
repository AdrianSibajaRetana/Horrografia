using Horrografia.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Horrografia.Server.Data.Repos.Interfaces
{
    public interface IContieneErrorRepository
    {
        Task<List<ContieneErrorModel>> GetAllAsync();
        Task<List<ContieneErrorModel>> GetErroresByReporteId(int idReporte);
        Task InsertMultiple(List<ContieneErrorModel> c);
    }
}