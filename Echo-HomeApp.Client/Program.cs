using Blazored.SessionStorage;
using Handlers;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Services;


var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Author: Samed

// Add IHttpClientFactory for use in singleton services
builder.Services.AddHttpClient().AddTransient<HttpMessageHandler, AuthenticationHandler>();
// AuthenticationHandler passes JWT back to API along with HttpClient-requests
builder.Services.AddTransient<AuthenticationHandler>();
builder.Services.AddTransient(sp => new HttpClient()
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
}).AddTransient<HttpMessageHandler, AuthenticationHandler>();

// Services for authentication as singletons for longer lifespan
builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();
builder.Services.AddBlazoredSessionStorageAsSingleton();

builder.Services.AddBlazorBootstrap();

await builder.Build().RunAsync();
