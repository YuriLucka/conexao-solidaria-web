using ConexaoSolidaria.Web;
using ConexaoSolidaria.Web.Servicos;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Endereço da API: vazio = mesma origem (o site é servido atrás do Kong, que encaminha /api).
// Em desenvolvimento aponta para o Kong local (wwwroot/appsettings.Development.json).
var apiBase = builder.Configuration["ApiBase"];
var baseUri = string.IsNullOrWhiteSpace(apiBase) ? new Uri(builder.HostEnvironment.BaseAddress) : new Uri(apiBase);
builder.Services.AddScoped(_ => new HttpClient { BaseAddress = baseUri });

builder.Services.AddMudServices();
builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();

builder.Services.AddScoped<Sessao>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<Sessao>());
builder.Services.AddScoped<Avisos>();
builder.Services.AddScoped<Tema>();
builder.Services.AddScoped<Api>();

await builder.Build().RunAsync();
