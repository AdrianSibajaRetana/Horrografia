using System.Collections.Generic;
using System.Threading.Tasks;
using Horrografia.Shared.Models;
using DataLibrary;
using Microsoft.Extensions.Configuration;
using Horrografia.Server.Data.Repos.Interfaces;
using System.Linq;

namespace Horrografia.Server.Data.Repos.Implementations
{
    public class FormaIncorrectaRepository : IFormaIncorrectaRepository
    {
        private readonly IDataAccess _dbContext;
        private readonly string ConectionString;

        public FormaIncorrectaRepository(IDataAccess dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            ConectionString = configuration.GetConnectionString("DefaultConnection");
        }

        //GET
        public async Task<List<FormaIncorrecta>> GetAllAsync()
        {
            string sql = "SELECT * FROM formaIncorrecta";
            var formas = await _dbContext.LoadData<FormaIncorrecta, dynamic>(sql, new { }, ConectionString);
            return formas;
        }

        //GET/{idNivel}
        public async Task<List<FormaIncorrecta>> GetFormasByLevelId(int idNivel)
        {
            string sql = "SELECT * FROM formaincorrecta JOIN item ON item.id = formaincorrecta.Itemid JOIN pertenecea ON item.id = pertenecea.idItem WHERE pertenecea.idNivel = @idNivel";
            var formas = await _dbContext.LoadData<FormaIncorrecta, dynamic>(sql, new { idNivel = idNivel }, ConectionString);
            return formas;
        }

        //POST
        public async Task InsertData(FormaIncorrecta f)
        {
            string sql = "insert into formaIncorrecta (Forma, Itemid) values (@Forma, @Itemid);";
            await _dbContext.SaveData(sql, new { Forma = f.Forma, Itemid = f.Itemid }, ConectionString);
        }

        //DELETE
        public async Task DeleteForma(FormaIncorrecta f)
        {
            string sql = "delete from formaIncorrecta where Forma = @Forma and Itemid = @Itemid";
            await _dbContext.SaveData(sql, new { Forma = f.Forma, Itemid = f.Itemid }, ConectionString);
        }
    }
}



