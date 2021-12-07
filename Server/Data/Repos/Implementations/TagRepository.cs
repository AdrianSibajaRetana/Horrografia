using System.Collections.Generic;
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
        
        public async Task<List<string>> GetFromAllReports()
        {
            string sql =
                "SELECT tag FROM Tag JOIN ItemTag i ON Tag.id = i.idTag JOIN item it ON i.idItem = it.id JOIN contieneerror c ON it.id = c.idItem JOIN reporte r ON c.idReporte = r.id";
            var tags = await _dbContext.LoadData<string, dynamic>(sql, new {}, ConectionString);
            return tags;
        }
        
        public async Task<List<string>> GetFromMonthlyReports(int month, int year)
        {
            string sql =
                "SELECT tag FROM Tag JOIN ItemTag i ON Tag.id = i.idTag JOIN item it ON i.idItem = it.id JOIN contieneerror c ON it.id = c.idItem JOIN reporte r ON c.idReporte = r.id WHERE EXTRACT(MONTH FROM r.Fecha) = @mes AND EXTRACT(YEAR FROM r.Fecha) = @ano";
            var tags = await _dbContext.LoadData<string, dynamic>(sql, new {mes = month, ano = year}, ConectionString);
            return tags;
        }
        
        public async Task<List<string>> GetFromYearlyReports(int year)
        {
            string sql =
                "SELECT tag FROM Tag JOIN ItemTag i ON Tag.id = i.idTag JOIN item it ON i.idItem = it.id JOIN contieneerror c ON it.id = c.idItem JOIN reporte r ON c.idReporte = r.id WHERE EXTRACT(YEAR FROM r.Fecha) = @ano";
            var tags = await _dbContext.LoadData<string, dynamic>(sql, new {ano = year}, ConectionString);
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

        public async Task DeleteRelation(int itemId, int tagId)
        {
            string sql = "delete from ItemTag where idTag = @TagId AND idItem = @ItemId";
            await _dbContext.SaveData(sql, new { TagId = tagId, ItemId = itemId }, ConectionString);
        }

        public async Task<List<ItemTagModel>> GetAllRelationsAsync()
        {
            string sql = "SELECT * FROM ItemTag";
            var tags = await _dbContext.LoadData<ItemTagModel, dynamic>(sql, new { }, ConectionString);
            return tags;
        }

        public async Task<List<string>> GetFromAllReportsFromSchool(string schoolCode)
        {
            string sql =
                "SELECT tag FROM Tag JOIN ItemTag i ON Tag.id = i.idTag JOIN item it ON i.idItem = it.id JOIN contieneerror c ON it.id = c.idItem JOIN reporte r ON c.idReporte = r.id JOIN aspnetusers a ON r.idUsuario = a.Id WHERE a.CodigoEscuela = @code";
            var tags = await _dbContext.LoadData<string, dynamic>(sql, new { code = schoolCode}, ConectionString);
            return tags;
        }

        public async Task<List<string>> GetFromUser(string id)
        {
            string sql =
                "SELECT tag FROM Tag JOIN ItemTag i ON Tag.id = i.idTag JOIN item it ON i.idItem = it.id JOIN contieneerror c ON it.id = c.idItem JOIN reporte r ON c.idReporte = r.id JOIN aspnetusers a ON r.idUsuario = a.Id WHERE a.Id = @studentId";
            var tags = await _dbContext.LoadData<string, dynamic>(sql, new { studentId = id}, ConectionString);
            return tags;
        }
    }
}
