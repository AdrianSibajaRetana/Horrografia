using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Horrografia.Shared.Models;

namespace Horrografia.Client.Data.Services.Implementations
{
    public class PersonService
    {
        private readonly HttpClient _client;
        public PersonService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<PersonModel>> getAsync() 
        {
            try
            {
                var forecasts = await _client.GetFromJsonAsync<WeatherForecast[]>("WeatherForecast");
            }
            catch
            { 
            
            }
        }
    }
}