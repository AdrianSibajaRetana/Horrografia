using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;

namespace Horrografia.Client.Data.Services.Implementations
{
    public class NivelService
    {
        private readonly HttpClient _client;
        public NivelService(HttpClient client)
        {
            _client = client;
        }

    }
}
