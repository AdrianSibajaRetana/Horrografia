using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Horrografia.Shared.Models;
using System.Net.Http.Json;
using Horrografia.Client.Data.Services.Interfaces;

namespace Horrografia.Client.Data.Services.Implementations
{
    public class PersonService : IPersonService
    {
        private readonly HttpClient _client;
        public PersonService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<PersonModel>> GetAsync()
        {
            try
            {
                var response = await _client.GetAsync("api/Person");
                if (response.IsSuccessStatusCode)
                {
                    var persons = await response.Content.ReadFromJsonAsync<List<PersonModel>>();
                    return persons;
                }
            }
            catch (Exception e)
            {
                return null;
            }
            return null;
        }
    }
}