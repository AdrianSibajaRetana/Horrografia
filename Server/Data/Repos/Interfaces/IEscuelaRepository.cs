using Horrografia.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Horrografia.Server.Data.Repos.Interfaces
{
    public interface IEscuelaRepository
    {
        Task<List<EscuelaModel>> GetAllAsync();
        Task InsertData(EscuelaModel school);
        Task<bool> CheckSchoolExistance(string schoolId);
        //Task InsertUserToSchool(string userId, string schoolid);
    }
}
