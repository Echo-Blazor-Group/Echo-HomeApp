using Microsoft.AspNetCore.Components.WebAssembly.Hosting;


var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Author: Samed
// TODO: (Samed) Ta bort om ej nödvändigt
HttpClientHandler handler = new HttpClientHandler()
{
    AllowAutoRedirect = false
};

builder.Services.AddScoped(sp => new HttpClient(handler)
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

builder.Services.AddBlazorBootstrap();

await builder.Build().RunAsync();
