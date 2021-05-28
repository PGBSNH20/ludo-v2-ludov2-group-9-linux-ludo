using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using LinuxLudo.Web.Domain.Services;
using LinuxLudo.Web.Hubs;
using LinuxLudo.Web.Services;

namespace LinuxLudo.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri(builder.Configuration["API_URL"]),
            });
            builder.Services.AddScoped<BrowserService>();
            builder.Services.AddScoped<IGameService, GameService>();
            builder.Services.AddScoped<HubController>();
            builder.Services.AddBlazoredLocalStorage();

            await builder.Build().RunAsync();
        }
    }
}
