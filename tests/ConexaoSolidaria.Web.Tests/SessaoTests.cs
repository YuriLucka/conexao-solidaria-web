using System.Net;
using System.Text;
using System.Text.Json;
using ConexaoSolidaria.Web.Modelos;
using ConexaoSolidaria.Web.Servicos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

namespace ConexaoSolidaria.Web.Tests;

// Sessão: JWT só em memória, restaurado e renovado pelo refresh token (cookie) em POST /api/auth/refresh.
public class SessaoTests : BunitContext
{
    private readonly ServidorFalso _servidor = new();

    public SessaoTests()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        Services.AddSingleton(new HttpClient(_servidor) { BaseAddress = new Uri("http://api.teste/") });
        Services.AddScoped<Sessao>();
        Services.AddScoped<Avisos>();
        Services.AddScoped<Api>();
    }

    private static string Jwt(Guid sub)
    {
        static string B64(string s) => Convert.ToBase64String(Encoding.UTF8.GetBytes(s)).TrimEnd('=').Replace('+', '-').Replace('/', '_');
        return $"{B64("{\"alg\":\"HS256\"}")}.{B64($"{{\"sub\":\"{sub}\",\"role\":\"Doador\"}}")}.assinatura";
    }

    private static string Token(Guid sub, string nome = "Mariana Albuquerque", int minutos = 15)
        => JsonSerializer.Serialize(new TokenResponse(Jwt(sub), "Bearer", DateTime.UtcNow.AddMinutes(minutos), nome, "Doador"), Api.Json);

    private Sessao Sessao => Services.GetRequiredService<Sessao>();

    [Fact]
    public async Task Abrir_o_site_com_cookie_valido_restaura_a_sessao_sem_localStorage()
    {
        var id = Guid.NewGuid();
        _servidor.Em("api/auth/refresh", HttpStatusCode.OK, Token(id));

        var estado = await Sessao.GetAuthenticationStateAsync();

        Assert.True(Sessao.EhDoador);
        Assert.Equal(id, Sessao.UsuarioId);
        Assert.True(estado.User.IsInRole("Doador"));
        Assert.Equal("Mariana Albuquerque", estado.User.Identity!.Name);
        // A chave da versão antiga (JWT no localStorage) é apagada; nada é gravado.
        JSInterop.VerifyInvoke("cs.apagar");
        JSInterop.VerifyNotInvoke("cs.gravar");
    }

    [Fact]
    public async Task Sem_cookie_valido_e_anonimo()
    {
        _servidor.Em("api/auth/refresh", HttpStatusCode.Unauthorized, "{}");

        var estado = await Sessao.GetAuthenticationStateAsync();

        Assert.False(Sessao.Logado);
        Assert.False(estado.User.Identity!.IsAuthenticated);
    }

    [Fact]
    public async Task Sair_revoga_no_servidor_e_limpa_a_memoria()
    {
        _servidor.Em("api/auth/logout", HttpStatusCode.NoContent, "");
        await Sessao.EntrarAsync(JsonSerializer.Deserialize<TokenResponse>(Token(Guid.NewGuid()), Api.Json)!);

        await Sessao.SairAsync();

        Assert.False(Sessao.Logado);
        Assert.Null(Sessao.Token);
        Assert.Contains("api/auth/logout", _servidor.Chamadas);
    }

    [Fact]
    public async Task Api_com_401_renova_o_token_e_repete_a_chamada()
    {
        await Sessao.EntrarAsync(JsonSerializer.Deserialize<TokenResponse>(Token(Guid.NewGuid(), "Antigo"), Api.Json)!);
        var tokenAntigo = Sessao.Token;
        _servidor.Em("api/auth/refresh", HttpStatusCode.OK, Token(Guid.NewGuid(), "Renovado"));
        // Primeira chamada recusada (JWT vencido no servidor); a repetida, com o token novo, passa.
        _servidor.Em("api/doacoes/1b2c3d4e-0000-0000-0000-000000000001", HttpStatusCode.Unauthorized, "{}",
            depois: (HttpStatusCode.OK, """{"id":"1b2c3d4e-0000-0000-0000-000000000001","idCampanha":"1b2c3d4e-0000-0000-0000-000000000002","valorDoacao":10,"status":"Processada","criadaEm":"2026-11-15T12:00:00Z"}"""));

        var r = await Services.GetRequiredService<Api>().DoacaoAsync(Guid.Parse("1b2c3d4e-0000-0000-0000-000000000001"));

        Assert.True(r.Ok);
        Assert.Equal(StatusDoacao.Processada, r.Valor!.Status);
        Assert.Equal("Renovado", Sessao.Nome);
        Assert.NotEqual(tokenAntigo, Sessao.Token);
        Assert.Equal(["api/doacoes/1b2c3d4e-0000-0000-0000-000000000001", "api/auth/refresh", "api/doacoes/1b2c3d4e-0000-0000-0000-000000000001"], _servidor.Chamadas);
    }

    // Respostas fixas por rota; "depois" é a resposta a partir da segunda chamada.
    private sealed class ServidorFalso : HttpMessageHandler
    {
        private readonly Dictionary<string, Queue<(HttpStatusCode, string)>> _respostas = [];
        public List<string> Chamadas { get; } = [];

        public void Em(string rota, HttpStatusCode status, string corpo, (HttpStatusCode, string)? depois = null)
        {
            var fila = new Queue<(HttpStatusCode, string)>();
            fila.Enqueue((status, corpo));
            if (depois is { } d) fila.Enqueue(d);
            _respostas[rota] = fila;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken ct)
        {
            var rota = request.RequestUri!.AbsolutePath.TrimStart('/');
            Chamadas.Add(rota);
            var (status, corpo) = _respostas.TryGetValue(rota, out var fila)
                ? (fila.Count > 1 ? fila.Dequeue() : fila.Peek())
                : (HttpStatusCode.NotFound, "{}");
            return Task.FromResult(new HttpResponseMessage(status) { Content = new StringContent(corpo, Encoding.UTF8, "application/json") });
        }
    }
}
