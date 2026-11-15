using System.Text;
using System.Text.Json;
using ConexaoSolidaria.Web.Modelos;
using ConexaoSolidaria.Web.Servicos;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

namespace ConexaoSolidaria.Web.Tests;

// Sessão: lê as claims do JWT guardado e descarta token vencido.
public class SessaoTests : BunitContext
{
    private static string Jwt(Guid sub)
    {
        static string B64(string s) => Convert.ToBase64String(Encoding.UTF8.GetBytes(s)).TrimEnd('=').Replace('+', '-').Replace('/', '_');
        return $"{B64("{\"alg\":\"HS256\"}")}.{B64($"{{\"sub\":\"{sub}\",\"role\":\"Doador\"}}")}.assinatura";
    }

    private Sessao NovaSessao(TokenResponse? guardado)
    {
        var json = guardado is null ? null : JsonSerializer.Serialize(guardado, Api.Json);
        JSInterop.Setup<string?>("cs.ler", "conexao-solidaria:sessao").SetResult(json);
        JSInterop.SetupVoid("cs.apagar", "conexao-solidaria:sessao").SetVoidResult();
        return new Sessao(Services.GetRequiredService<IJSRuntime>());
    }

    [Fact]
    public async Task Token_guardado_valido_vira_usuario_logado_com_perfil()
    {
        var id = Guid.NewGuid();
        var sessao = NovaSessao(new TokenResponse(Jwt(id), "Bearer", DateTime.UtcNow.AddHours(1), "Mariana Albuquerque", "Doador"));

        var estado = await sessao.GetAuthenticationStateAsync();

        Assert.True(sessao.EhDoador);
        Assert.Equal(id, sessao.UsuarioId);
        Assert.True(estado.User.IsInRole("Doador"));
        Assert.Equal("Mariana Albuquerque", estado.User.Identity!.Name);
    }

    [Fact]
    public async Task Token_vencido_e_descartado()
    {
        var sessao = NovaSessao(new TokenResponse(Jwt(Guid.NewGuid()), "Bearer", DateTime.UtcNow.AddMinutes(-1), "X", "Doador"));

        var estado = await sessao.GetAuthenticationStateAsync();

        Assert.False(sessao.Logado);
        Assert.False(estado.User.Identity!.IsAuthenticated);
        JSInterop.VerifyInvoke("cs.apagar");
    }

    [Fact]
    public async Task Sem_token_e_anonimo()
    {
        var sessao = NovaSessao(null);
        Assert.False((await sessao.GetAuthenticationStateAsync()).User.Identity!.IsAuthenticated);
    }
}
