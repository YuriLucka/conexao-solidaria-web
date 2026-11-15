using System.Net;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using ConexaoSolidaria.Web.Modelos;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.JSInterop;

namespace ConexaoSolidaria.Web.Servicos;

// Sessão do usuário. O JWT (15 min) fica só na memória da aba: não vai para o localStorage, onde um script injetado o leria.
// O refresh token fica num cookie HttpOnly que o JavaScript não enxerga. Ao abrir o site, e um pouco antes de o JWT vencer,
// a sessão é renovada em POST /api/auth/refresh: a pessoa continua logada ao recarregar a página ou voltar dias depois.
// A autorização de verdade é da API (401/403); aqui é só para mostrar ou esconder telas.
public sealed class Sessao : AuthenticationStateProvider, IDisposable
{
    // Chave da versão anterior, que guardava o JWT no localStorage: é apagada na primeira carga.
    private const string ChaveAntiga = "conexao-solidaria:sessao";
    private static readonly TimeSpan Antecedencia = TimeSpan.FromMinutes(1);
    private static readonly TimeSpan NovaTentativa = TimeSpan.FromSeconds(30);
    private static readonly AuthenticationState Anonimo = new(new ClaimsPrincipal(new ClaimsIdentity()));

    private readonly HttpClient _http;
    private readonly IJSRuntime _js;
    private Task? _carga;
    private Task<bool>? _renovacao;
    private Timer? _timer;

    public Sessao(HttpClient http, IJSRuntime js)
    {
        _http = http;
        _js = js;
    }

    public string? Token { get; private set; }
    public string? Nome { get; private set; }
    public string? Perfil { get; private set; }
    public Guid? UsuarioId { get; private set; }
    public DateTime ExpiraEm { get; private set; }

    public bool Logado => Token is not null && ExpiraEm > DateTime.UtcNow;
    public bool EhGestor => Logado && Perfil == Perfis.GestorONG;
    public bool EhDoador => Logado && Perfil == Perfis.Doador;

    // Para onde voltar depois do login (ex.: a campanha em que a pessoa clicou em "Entrar para doar").
    public string? VoltarPara { get; set; }

    // Restaura a sessão pelo cookie uma vez só, ao abrir o site.
    public Task CarregarAsync() => _carga ??= RestaurarAsync();

    public Task EntrarAsync(TokenResponse token)
    {
        // Login feito nesta aba: não há sessão anterior para restaurar.
        _carga ??= Task.CompletedTask;
        Aplicar(token);
        Notificar();
        return Task.CompletedTask;
    }

    public async Task SairAsync()
    {
        _carga ??= Task.CompletedTask;
        // Revoga o refresh token no servidor antes de limpar a tela: assim, recarregar logo em seguida não restaura a sessão.
        // Sem rede, a memória é limpa do mesmo jeito e o cookie expira sozinho.
        await PostarAsync("api/auth/logout");
        Limpar();
        Notificar();
    }

    // Renova o JWT pelo cookie. Chamadas simultâneas (várias requisições com 401) esperam a mesma renovação.
    public Task<bool> RenovarAsync()
    {
        if (_renovacao is { IsCompleted: false })
            return _renovacao;
        return _renovacao = RenovarAgoraAsync();
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        await CarregarAsync();
        if (!Logado) return Anonimo;
        var identidade = new ClaimsIdentity(
        [
            new Claim("sub", UsuarioId?.ToString() ?? ""),
            new Claim("name", Nome ?? ""),
            new Claim("role", Perfil ?? "")
        ], "jwt", "name", "role");
        return new AuthenticationState(new ClaimsPrincipal(identidade));
    }

    public void Dispose() => _timer?.Dispose();

    private async Task RestaurarAsync()
    {
        // Garante que _carga já esteja atribuída antes de qualquer notificação chamar CarregarAsync de novo.
        await Task.Yield();
        try { await _js.InvokeVoidAsync("cs.apagar", ChaveAntiga); }
        catch (JSException) { }
        await RenovarAsync();
    }

    private async Task<bool> RenovarAgoraAsync()
    {
        var (token, recusado) = await PostarAsync("api/auth/refresh");
        if (token is not null)
        {
            Aplicar(token);
            Notificar();
            return true;
        }

        if (recusado)
        {
            // Cookie ausente, vencido ou revogado: a sessão acabou.
            var estavaLogado = Token is not null;
            Limpar();
            if (estavaLogado) Notificar();
        }
        else if (Token is not null)
        {
            // Sem rede: mantém o JWT atual enquanto valer e tenta de novo daqui a pouco.
            Agendar(NovaTentativa);
        }
        return false;
    }

    // Devolve o token (sucesso) ou se o servidor recusou a sessão (401); sem rede, nenhum dos dois.
    private async Task<(TokenResponse? Token, bool Recusado)> PostarAsync(string rota)
    {
        using var req = new HttpRequestMessage(HttpMethod.Post, rota);
        // Em desenvolvimento o site e o Kong estão em portas diferentes: sem isto o navegador não envia o cookie.
        req.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
        try
        {
            using var resp = await _http.SendAsync(req);
            if (resp.StatusCode == HttpStatusCode.Unauthorized) return (null, true);
            if (resp.StatusCode != HttpStatusCode.OK) return (null, false);
            return (await resp.Content.ReadFromJsonAsync<TokenResponse>(Api.Json), false);
        }
        catch (Exception e) when (e is HttpRequestException or TaskCanceledException or JsonException or NotSupportedException)
        {
            return (null, false);
        }
    }

    private void Aplicar(TokenResponse t)
    {
        Token = t.AccessToken;
        Nome = t.Nome;
        Perfil = t.Perfil;
        ExpiraEm = t.ExpiraEm;
        UsuarioId = LerSub(t.AccessToken);
        Agendar(ExpiraEm - DateTime.UtcNow - Antecedencia);
    }

    private void Limpar()
    {
        Token = Nome = Perfil = null;
        UsuarioId = null;
        _timer?.Dispose();
        _timer = null;
    }

    private void Agendar(TimeSpan espera)
    {
        _timer?.Dispose();
        if (espera < TimeSpan.FromSeconds(5)) espera = TimeSpan.FromSeconds(5);
        if (espera > TimeSpan.FromDays(1)) return;
        _timer = new Timer(_ => _ = RenovarAsync(), null, espera, Timeout.InfiniteTimeSpan);
    }

    private void Notificar() => NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());

    // Lê a claim "sub" do payload do JWT (base64url). Sem validar assinatura: quem valida é a API.
    private static Guid? LerSub(string jwt)
    {
        var partes = jwt.Split('.');
        if (partes.Length < 2) return null;
        var b64 = partes[1].Replace('-', '+').Replace('_', '/');
        b64 += new string('=', (4 - b64.Length % 4) % 4);
        try
        {
            using var doc = JsonDocument.Parse(Encoding.UTF8.GetString(Convert.FromBase64String(b64)));
            return doc.RootElement.TryGetProperty("sub", out var sub) && Guid.TryParse(sub.GetString(), out var id) ? id : null;
        }
        catch (Exception e) when (e is FormatException or JsonException) { return null; }
    }
}
