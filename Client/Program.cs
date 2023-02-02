using BlazorCrudPersonasSql.Client;
using BlazorCrudPersonasSql.Client.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// para usar authenticacion
builder.Services.AddAuthorizationCore();
//agregar mi clase de autenticacion
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProviderFalso>();  

await builder.Build().RunAsync();
