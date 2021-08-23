using System.Collections.Generic;
using System.Threading.Tasks;
using Horrografia.Shared.Models;
using DataLibrary;
using Microsoft.Extensions.Configuration;
using Horrografia.Server.Data.Repos.Interfaces;
using System.Linq;

namespace Horrografia.Server.Data.Repos.Implementations
{
    public class EscuelaRepository : IEscuelaRepository
    {
        private readonly IDataAccess _dbContext;
        private readonly string ConectionString;

        public EscuelaRepository(IDataAccess dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            ConectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<EscuelaModel>> GetAllAsync()
        {
            string sql = "SELECT * FROM Escuela";
            var escuelas = await _dbContext.LoadData<EscuelaModel, dynamic>(sql, new { }, ConectionString);
            return escuelas;
        }

        public async Task InsertData(EscuelaModel s)
        {
            string sql = "insert into Escuela (Nombre, Codigo) values (@Nombre, @Codigo);";
            await _dbContext.SaveData(sql, new { Nombre = s.Nombre, Codigo = s.Codigo }, ConectionString);
        }

        public async Task<bool> CheckSchoolExistance(string schoolId)
        {
            bool exists = false; 
            string sql = "SELECT * FROM Escuela WHERE Codigo = @Code";
            var schools = await _dbContext.LoadData<ItemModel, dynamic>(sql, new { Code = schoolId }, ConectionString);
            if (schools.Any())
            {
                exists = true;
            }
            return exists;
        }

        //public async Task InsertUserToSchool(string userId, string schoolid) { }
    }
}
