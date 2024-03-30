using BeyondComputersNi.Blazor;
using BeyondComputersNi.Blazor.Authentication;
using BeyondComputersNi.Blazor.Interfaces;
using BeyondComputersNi.Blazor.Services;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();

builder.Services.AddHttpClient(builder.Configuration["Api:HttpClient"] ?? "")
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration["Api:Url"] ?? throw new InvalidDataException("Base URL not set.")))
    .AddHttpMessageHandler<AuthenticationHandler>();

builder.Services.AddBlazoredLocalStorageAsSingleton();

builder.Services.AddTransient<AuthenticationHandler>();
builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();

await builder.Build().RunAsync();
