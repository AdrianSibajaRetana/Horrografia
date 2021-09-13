using Horrografia.Client.Shared.Objects;
using Horrografia.Shared.Models;
using System.Threading.Tasks;

namespace Horrografia.Client.Data.Services.Interfaces
{
    public interface ITagService
    {
        Task<ControllerResponse<TagModel>> GetAsync();
        Task<ControllerResponse<TagModel>> PostAsync(TagModel t);
        Task<ControllerResponse<TagModel>> DeleteAsync(TagModel t);
        Task<ControllerResponse<TagModel>> PostRelationAync(int itemId, int tagId);
        Task<ControllerResponse<TagModel>> DeleteRelationAync(int itemId, int tagId);
        Task<ControllerResponse<ItemTagModel>> GetRelationsAync();
    }
}
