using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Services;


var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Author: Samed
// TODO: (Samed) Ta bort om ej n�dv�ndigt
HttpClientHandler handler = new HttpClientHandler()
{
    AllowAutoRedirect = false
};

builder.Services.AddSingleton(sp => new HttpClient(handler)
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

// Services for authentication as singletons for longer lifespan
builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();
builder.Services.AddBlazoredSessionStorageAsSingleton();

builder.Services.AddBlazorBootstrap();

await builder.Build().RunAsync();
