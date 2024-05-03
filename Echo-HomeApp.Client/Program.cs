using Microsoft.AspNetCore.Components.WebAssembly.Hosting;


var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

// TODO: (Samed) remove this, anad also from nuget packs and imports - if decided not to use
builder.Services.AddBlazorBootstrap();

await builder.Build().RunAsync();
