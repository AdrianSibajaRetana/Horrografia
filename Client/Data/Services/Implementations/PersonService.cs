using System;
using System.Collections.Generic;
using System.Linq;
using Horrografia.Shared.Models;
using Horrografia.Client.Data.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;


namespace Horrografia.Client.Data.Services.Implementations
{
    public class PersonService : IPersonService
    {
        private readonly HttpClient _http;
        private readonly ILogger<PersonService> _logger;
        public PersonService(HttpClient client, ILogger<PersonService> logger)
        {
            _http = client;
            _logger = logger; 
        }

        public async Task<List<PersonModel>> GetAsync()
        {
            try
            {
                var response = await _http.GetAsync("api/Person");
                if (response.IsSuccessStatusCode)
                {
                    var persons = await response.Content.ReadFromJsonAsync<List<PersonModel>>();
                    return persons;
                }
                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while fetching from db");
                return null;
            }
        }
    }
}