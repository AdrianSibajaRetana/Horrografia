using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Horrografia.Client.Data.Services.Implementations;
using Horrografia.Client.Data.Services.Interfaces;
using MudBlazor.Services;
namespace Horrografia.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddHttpClient("Horrografia.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            // Supply HttpClient instances that include access tokens when making requests to the server project
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("Horrografia.ServerAPI"));

            builder.Services.AddApiAuthorization();
            builder.Services.AddMudServices();
            //Mis servicios
            builder.Services.AddTransient<IPersonService, PersonService>();
            builder.Services.AddTransient<INivelService, NivelService>();

            await builder.Build().RunAsync();
        }
    }
}
