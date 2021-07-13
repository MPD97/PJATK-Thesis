using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Thesis.Application;
using Thesis.Infrastructure;
using Thesis.Infrastructure.Identity;
using Thesis.Infrastructure.Presistance;
using Microsoft.AspNetCore.Identity;
using Thesis.WebUI.Client.DataServices;
using Blazored.LocalStorage;
using Blazored.SessionStorage;

namespace Thesis.WebUI.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddHttpClient("Thesis.WebUI.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            // Supply HttpClient instances that include access tokens when making requests to the server project
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("Thesis.WebUI.ServerAPI"));

            builder.Services.AddScoped(sp => new HttpClient{ BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddHttpClient<IRouteServiceHttp, RestRouteServiceHttp>(fact => fact.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

            builder.Services.AddHttpClient<IRunServiceHttp, RestRunServiceHttp>(fact => fact.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
            
            builder.Services.AddBlazoredSessionStorage();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddApiAuthorization();

            await builder.Build().RunAsync();
        }
    }
}
