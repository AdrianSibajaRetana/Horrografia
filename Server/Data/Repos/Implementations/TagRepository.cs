﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Horrografia.Shared.Models;
using DataLibrary;
using Microsoft.Extensions.Configuration;
using Horrografia.Server.Data.Repos.Interfaces;
using System.Linq;

namespace Horrografia.Server.Data.Repos.Implementations
{
    public class TagRepository : ITagRepository
    {
        private readonly IDataAccess _dbContext;
        private readonly string ConectionString;

        public TagRepository(IDataAccess dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            ConectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<TagModel>> GetAllAsync()
        {
            string sql = "SELECT * FROM Tag";
            var tags = await _dbContext.LoadData<TagModel, dynamic>(sql, new { }, ConectionString);
            return tags;
        }

        public async Task InsertData(TagModel t)
        {
            string sql = "insert into Tag (Tag) values (@TagContent);";
            await _dbContext.SaveData(sql, new { TagContent = t.Tag}, ConectionString);
        }

        public async Task DeleteById(int TagId)
        {
            string sql = "delete from Tag where id = @idTag";
            await _dbContext.SaveData(sql, new { idTag = TagId }, ConectionString);
        }

        public async Task AddTagToitem(int itemId, int tagId)
        {
            string sql = "insert into ItemTag (idTag, idItem) values (@TagId, @ItemId);";
            await _dbContext.SaveData(sql, new { TagId = tagId, ItemId = itemId }, ConectionString);
        }

    }
}
