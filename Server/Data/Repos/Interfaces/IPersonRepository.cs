using System.Collections.Generic;
using System.Threading.Tasks;
using Horrografia.Shared.Models;

namespace Horrografia.Server.Data.Repos.Interfaces
{
    public interface IPersonRepository
    {
        Task<List<PersonModel>> GetAllAsync();
        Task<PersonModel> GetPersonById(int PersonId);
        Task DeletePersonById(int PersonId);
        Task UpdatePersonById(int PersonId, string Firstname, string LastName);
        Task InsertData(PersonModel p);
    }
}


