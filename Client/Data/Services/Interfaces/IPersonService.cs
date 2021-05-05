using Horrografia.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Horrografia.Client.Data.Services.Interfaces
{
    public interface IPersonService
    {
        Task<List<PersonModel>> GetAsync();
    }
}