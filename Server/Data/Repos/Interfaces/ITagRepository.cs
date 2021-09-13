using Horrografia.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Horrografia.Server.Data.Repos.Interfaces
{
    public interface ITagRepository
    {
        Task<List<TagModel>> GetAllAsync();
        Task InsertData(TagModel t);
        Task DeleteById(int TagId);
        Task AddTagToitem(int itemId, int tagId);
        Task DeleteRelation(int itemId, int tagId);
        Task<List<ItemTagModel>> GetAllRelationsAsync();
    }
}
