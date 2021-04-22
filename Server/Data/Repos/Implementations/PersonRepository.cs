using System.Collections.Generic;
using System.Threading.Tasks;
using Horrografia.Shared.Models;
using DataLibrary;
using Microsoft.Extensions.Configuration;
using Horrografia.Server.Data.Repos.Interfaces;

namespace Horrografia.Server.Data.Repos.Implementations
{
    public class PersonRepository : IPersonRepository
    {   
        private readonly IDataAccess _dbContext;
        private readonly string ConectionString;

        public PersonRepository(IDataAccess dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            ConectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task DeletePersonById(int PersonId)
        {
            string sql = "delete from people where id = @id";
            await _dbContext.SaveData(sql, new { id = PersonId }, ConectionString);
        }

        public async Task<List<PersonModel>> GetAllAsync()
        {
            string sql = "select * from people";
            var people = await _dbContext.LoadData<PersonModel, dynamic>(sql, new { }, ConectionString);
            return people;
        }

        public async Task<PersonModel> GetPersonById(int PersonId)
        {
            var returnedPerson = new PersonModel();
            string sql = $"SELECT * FROM people WHERE id = @id";
            var people = await _dbContext.LoadData<PersonModel, dynamic>(sql, new { id = PersonId }, ConectionString);
            if (people.Count > 0)
            {
                returnedPerson = people[0];
            }
            return returnedPerson;
        }

        public async Task InsertData(PersonModel p)
        {
            string sql = "insert into people (Firstname, Lastname) values (@Firstname, @Lastname);";
            await _dbContext.SaveData(sql, new { FirstName = p.FirstName, Lastname = p.LastName}, ConectionString);
        }

        public async Task UpdatePersonById(int id, string pFirstname, string pLastName)
        {
            string sql = "update people set FirstName = @Firstname where Lastname = @Lastname";
            await _dbContext.SaveData(sql, new { FirstName = pFirstname, Lastname = pLastName }, ConectionString);
        }
    }
}
