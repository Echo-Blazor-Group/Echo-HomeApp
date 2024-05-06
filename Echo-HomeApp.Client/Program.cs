using Microsoft.AspNetCore.Components.WebAssembly.Hosting;


var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Author: Samed
// TODO: Ta bort om ej nödvändigt
HttpClientHandler handler = new HttpClientHandler()
{
    AllowAutoRedirect = false
};

builder.Services.AddScoped(sp => new HttpClient(handler)
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

// TODO: (Samed) remove this, and also from nuget packs and imports - if decided not to use
builder.Services.AddBlazorBootstrap();

await builder.Build().RunAsync();
