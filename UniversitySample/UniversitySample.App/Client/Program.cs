using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;
using System.Net.NetworkInformation;
using UniversitySample.App.Client;
using UniversitySample.App.Client.ApiClient;
using UniversitySample.App.Client.Shared;
using UniversitySample.App.Shared;
using Microsoft.AspNetCore.Components.Authorization;

namespace UniversitySample.App.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            var studentServiceUrl = builder.Configuration["StudentServiceUrl"];
            var audience = builder.Configuration["Oidc:ClientId"];

            builder.Services.AddScoped<CustomAuthorizationHeaderHandler>();
            builder.Services
                .AddHttpClient("WebAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<CustomAuthorizationHeaderHandler>();
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
                .CreateClient("WebAPI"));

            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            //builder.Services.AddScoped<IClaimsTransformation>(_ => new KeycloakRolesClaimsHelper("roles", audience));
            builder.Services.AddScoped(typeof(AccountClaimsPrincipalFactory<RemoteUserAccount>), typeof(CustomAccountFactory));

            builder.Services.AddScoped<StudentServiceClient>(provider =>
            {
                var httpClient = provider.GetRequiredService<HttpClient>();
                return new StudentServiceClient(studentServiceUrl, httpClient);
            });

            builder.Services.AddMudServices(config =>
            {
                config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopCenter;
                config.SnackbarConfiguration.PreventDuplicates = true;
                config.SnackbarConfiguration.NewestOnTop = true;
                config.SnackbarConfiguration.ShowCloseIcon = true;
                config.SnackbarConfiguration.VisibleStateDuration = 20000;
                config.SnackbarConfiguration.HideTransitionDuration = 500;
                config.SnackbarConfiguration.ShowTransitionDuration = 500;
                config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
            });

            builder.Services.AddCustomAuthentication(options =>
            {
                builder.Configuration.Bind("Oidc", options.ProviderOptions);
                options.UserOptions.RoleClaim = "roles";
            });

            builder.Services.AddApiAuthorization();

            await builder.Build().RunAsync();
        }
    }
}