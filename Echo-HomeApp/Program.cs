using AuthState;
using BlazorBootstrap;
using Blazored.SessionStorage;
using Echo_HomeApp.Components;
using Handlers;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Options;
using Services;

namespace Echo_HomeApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // AuthenticationHandler passes JWT back to API along with HttpClient-requests
            builder.Services.AddTransient<AuthenticationHandler>();

            // Add IHttpClientFactory for use in singleton services
            builder.Services.AddHttpClient("ServerApi", client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["ServerApi : BaseAddress"]);
            }).AddHttpMessageHandler<AuthenticationHandler>();

            // Services for authentication as singletons for longer lifespan
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
            builder.Services.AddBlazoredSessionStorage();
            builder.Services.AddRazorComponents().AddInteractiveWebAssemblyComponents();

            builder.Services.AddAuthorization();
            builder.Services.AddBlazorBootstrap();

            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
 
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            
            
            app.UseStaticFiles();
            app.UseAntiforgery();
           

            app.MapRazorComponents<App>()
                .AddInteractiveWebAssemblyRenderMode()
                .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);


            app.Run();
        }
    }
}
