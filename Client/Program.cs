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
using MudBlazor;

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

            //Para notificaciones tipo snackbar
            builder.Services.AddMudServices(config =>
            {
                config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;

                config.SnackbarConfiguration.PreventDuplicates = false;
                config.SnackbarConfiguration.NewestOnTop = false;
                config.SnackbarConfiguration.ShowCloseIcon = true;
                config.SnackbarConfiguration.VisibleStateDuration = 10000;
                config.SnackbarConfiguration.HideTransitionDuration = 500;
                config.SnackbarConfiguration.ShowTransitionDuration = 500;
                config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
            });

            //Mis servicios
            builder.Services.AddTransient<INivelService, NivelService>();
            builder.Services.AddTransient<IPerteneceAService, PerteneceAService>();
            builder.Services.AddTransient<IItemService, ItemService>();
            builder.Services.AddTransient<IPistaService, PistaService>();
            builder.Services.AddTransient<IFormaIncorrectaService, FormaIncorrectaService>();

            await builder.Build().RunAsync();
        }
    }
}
