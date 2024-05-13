using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Services;


var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Author: Samed
// TODO: (Samed) Ta bort om ej nödvändigt
HttpClientHandler handler = new HttpClientHandler()
{
    AllowAutoRedirect = false
};

// TODO: Singleton not optimal for HttpClient. Find workaround for AuthenticationService.
builder.Services.AddSingleton(sp => new HttpClient(handler)
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

// Services for authentication as singletons for longer lifespan
builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();
builder.Services.AddBlazoredSessionStorageAsSingleton();

builder.Services.AddBlazorBootstrap();

await builder.Build().RunAsync();
