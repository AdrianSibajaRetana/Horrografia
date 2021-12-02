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
        Task<ControllerResponse<TagModel>> PostRelationAync(ItemTagModel i);
        Task<ControllerResponse<TagModel>> DeleteRelationAync(ItemTagModel i);
        Task<ControllerResponse<ItemTagModel>> GetRelationsAync();
        
        Task<ControllerResponse<string>> GetTagsFromAllReports();
        Task<ControllerResponse<string>> GetTagsFromMonthlyReports(int month, int year);
        Task<ControllerResponse<string>> GetTagsFromYearlyReports(int year);
    }
}
