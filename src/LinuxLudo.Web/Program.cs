using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using LinuxLudo.Web.Hubs;
using LinuxLudo.Web.Services;
using LinuxLudo.Web.Domain.Services;
using Microsoft.AspNetCore.Components.Authorization;
using LinuxLudo.Web.Authentication;

namespace LinuxLudo.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
            builder.Services.AddScoped<BrowserService>();
            builder.Services.AddScoped<HubController>();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddAuthorizationCore();

            await builder.Build().RunAsync();
        }
    }
}
