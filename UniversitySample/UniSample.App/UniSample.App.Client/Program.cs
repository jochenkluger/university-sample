using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using UniSample.App.Client;
using UniSample.App.Client.Services;
using UniSample.App.Client.Shared;
using UniSample.App.Client.Utils;
using UniSample.App.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var baseUrl = builder.Configuration["LocalUrl"];
var apiUrl = baseUrl;

builder.Services.AddMudServices();
builder.Services.AddSingleton<StudentDataService>();
builder.Services.AddScoped<CustomAuthorizationHeaderHandler>();

builder.Services
    .AddHttpClient("WebAPI", client => client.BaseAddress = new Uri(baseUrl))
    .AddHttpMessageHandler<CustomAuthorizationHeaderHandler>();
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
    .CreateClient("WebAPI"));

builder.Services.AddScoped(typeof(AccountClaimsPrincipalFactory<RemoteUserAccount>), typeof(CustomAccountFactory));

builder.Services.AddScoped<CoursesClient>(sp =>
{
    var client = sp.GetRequiredService<HttpClient>();
    return new CoursesClient(apiUrl, client);
});

builder.Services.AddScoped<LibraryClient>(sp =>
{
    var client = sp.GetRequiredService<HttpClient>();
    return new LibraryClient(apiUrl, client);
});

builder.Services.AddScoped<StudentsClient>(sp =>
{
    var client = sp.GetRequiredService<HttpClient>();
    return new StudentsClient(apiUrl, client);
});

builder.Services.AddCustomAuthentication(options =>
{
    builder.Configuration.Bind("Oidc", options.ProviderOptions);
    options.UserOptions.RoleClaim = "roles";
});

builder.Services.AddApiAuthorization();

await builder.Build().RunAsync();
