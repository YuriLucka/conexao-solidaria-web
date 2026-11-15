using System.Security.Claims;
using System.Text;
using System.Text.Json;
using ConexaoSolidaria.Web.Modelos;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace ConexaoSolidaria.Web.Servicos;

// Sessão do usuário: o JWT fica no localStorage e as claims são lidas do próprio token.
// A autorização de verdade é da API (401/403); aqui é só para mostrar ou esconder telas.
public class Sessao : AuthenticationStateProvider
{
    private const string Chave = "conexao-solidaria:sessao";
    private static readonly AuthenticationState Anonimo = new(new ClaimsPrincipal(new ClaimsIdentity()));

    private readonly IJSRuntime _js;
    private bool _carregada;
    private Timer? _expiracao;

    public Sessao(IJSRuntime js) => _js = js;

    public string? Token { get; private set; }
    public string? Nome { get; private set; }
    public string? Perfil { get; private set; }
    public Guid? UsuarioId { get; private set; }
    public DateTime ExpiraEm { get; private set; }

    public bool Logado => Token is not null && ExpiraEm > DateTime.UtcNow;
    public bool EhGestor => Logado && Perfil == "GestorONG";
    public bool EhDoador => Logado && Perfil == "Doador";

    // Para onde voltar depois do login (ex.: a campanha em que a pessoa clicou em "Entrar para doar").
    public string? VoltarPara { get; set; }

    public async Task CarregarAsync()
    {
        if (_carregada) return;
        _carregada = true;
        var json = await _js.InvokeAsync<string?>("cs.ler", Chave);
        if (json is null) return;
        try
        {
            var t = JsonSerializer.Deserialize<TokenResponse>(json, Api.Json);
            if (t is not null && t.ExpiraEm > DateTime.UtcNow) Aplicar(t);
            else await _js.InvokeVoidAsync("cs.apagar", Chave);
        }
        catch (JsonException) { await _js.InvokeVoidAsync("cs.apagar", Chave); }
    }

    public async Task EntrarAsync(TokenResponse token)
    {
        Aplicar(token);
        await _js.InvokeVoidAsync("cs.gravar", Chave, JsonSerializer.Serialize(token, Api.Json));
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task SairAsync()
    {
        Token = Nome = Perfil = null;
        UsuarioId = null;
        _expiracao?.Dispose();
        await _js.InvokeVoidAsync("cs.apagar", Chave);
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
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

    private void Aplicar(TokenResponse t)
    {
        Token = t.AccessToken;
        Nome = t.Nome;
        Perfil = t.Perfil;
        ExpiraEm = t.ExpiraEm;
        UsuarioId = LerSub(t.AccessToken);

        // No vencimento do token a interface volta para "anônimo" sem esperar o próximo 401.
        _expiracao?.Dispose();
        var falta = ExpiraEm - DateTime.UtcNow;
        if (falta > TimeSpan.Zero && falta < TimeSpan.FromDays(20))
            _expiracao = new Timer(_ => NotifyAuthenticationStateChanged(GetAuthenticationStateAsync()), null, falta + TimeSpan.FromSeconds(1), Timeout.InfiniteTimeSpan);
    }

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
