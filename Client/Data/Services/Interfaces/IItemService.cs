using Horrografia.Client.Shared.Objects;
using Horrografia.Shared.Models;
using System.Threading.Tasks;

namespace Horrografia.Client.Data.Services.Interfaces
{
    public interface IItemService
    {
        Task<ControllerResponse<ItemModel>> GetAsync();
        Task<ControllerResponse<ItemModel>> GetAsyncByLevelId(int id);
        Task<ControllerResponse<ItemModel>> PostAsync(ItemModel i);
        Task<ControllerResponse<ItemModel>> DeleteAsync(ItemModel i);
        Task<ControllerResponse<ItemModel>> UpdateAsync(ItemModel i);

    }
}
