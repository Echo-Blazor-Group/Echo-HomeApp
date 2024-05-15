using Blazored.SessionStorage;
using Handlers;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Services;


var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Author: Samed


// AuthenticationHandler passes JWT back to API along with HttpClient-requests
builder.Services.AddTransient<AuthenticationHandler>();

// Add IHttpClientFactory for use in singleton services
builder.Services.AddHttpClient("ServerApi").
    ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)).AddHttpMessageHandler<AuthenticationHandler>();

builder.Services.AddWMBSC();
// Configure HttpClient that is used for injection in components
builder.Services.AddTransient(sp =>
{
    var handler = new HttpClientHandler();
    var authHandler = sp.GetService<AuthenticationHandler>();
    authHandler.InnerHandler = handler;

    return new HttpClient(authHandler)
    {
        BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
    };
});


// Services for authentication as singletons for longer lifespan
builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();
builder.Services.AddBlazoredSessionStorageAsSingleton();

builder.Services.AddBlazorBootstrap();

await builder.Build().RunAsync();
