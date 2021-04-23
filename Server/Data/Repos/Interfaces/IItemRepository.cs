using Horrografia.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Horrografia.Server.Data.Repos.Interfaces
{
    public interface IItemRepository
    {
        Task DeleteItem(int idItem);
        Task<List<ItemModel>> GetAllAsync();
        Task<ItemModel> GetItemById(int idItem);
        Task InsertData(ItemModel i);
    }
}