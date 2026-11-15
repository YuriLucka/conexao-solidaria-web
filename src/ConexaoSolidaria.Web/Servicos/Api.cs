using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using ConexaoSolidaria.Web.Modelos;
using Microsoft.AspNetCore.Components;

namespace ConexaoSolidaria.Web.Servicos;

// Cliente da campanhas-api (pelo Kong). Erros viram Problema com a mensagem em português que a API já manda.
public class Api
{
    public static readonly JsonSerializerOptions Json = new(JsonSerializerDefaults.Web)
    {
        Converters = { new JsonStringEnumConverter() }
    };

    private readonly HttpClient _http;
    private readonly Sessao _sessao;
    private readonly NavigationManager _nav;
    private readonly Avisos _avisos;

    public Api(HttpClient http, Sessao sessao, NavigationManager nav, Avisos avisos)
    {
        _http = http;
        _sessao = sessao;
        _nav = nav;
        _avisos = avisos;
    }

    // Autenticação e cadastro
    public Task<Resultado<TokenResponse>> LoginAsync(LoginRequest r) => Enviar<TokenResponse>(HttpMethod.Post, "api/auth/login", r, autenticado: false);
    public Task<Resultado<DoadorResponse>> CadastrarAsync(CadastrarDoadorRequest r) => Enviar<DoadorResponse>(HttpMethod.Post, "api/doadores", r, autenticado: false);

    // Público
    public Task<Resultado<List<CampanhaAtiva>>> CampanhasAtivasAsync() => Enviar<List<CampanhaAtiva>>(HttpMethod.Get, "api/campanhas/ativas");
    public Task<Resultado<Campanha>> CampanhaAsync(Guid id) => Enviar<Campanha>(HttpMethod.Get, $"api/campanhas/{id}");
    public Task<Resultado<DoacoesDaCampanha>> DoacoesDaCampanhaAsync(Guid id, int quantidade = 8) => Enviar<DoacoesDaCampanha>(HttpMethod.Get, $"api/campanhas/{id}/doacoes-recentes?quantidade={quantidade}");

    // Doador
    public Task<Resultado<Doacao>> DoarAsync(CriarDoacaoRequest r) => Enviar<Doacao>(HttpMethod.Post, "api/doacoes", r);
    public Task<Resultado<Doacao>> DoacaoAsync(Guid id) => Enviar<Doacao>(HttpMethod.Get, $"api/doacoes/{id}");
    public Task<Resultado<List<Doacao>>> MinhasDoacoesAsync() => Enviar<List<Doacao>>(HttpMethod.Get, "api/doacoes/minhas");

    // Gestão
    public Task<Resultado<List<Campanha>>> CampanhasAsync() => Enviar<List<Campanha>>(HttpMethod.Get, "api/campanhas");
    public Task<Resultado<Campanha>> CriarCampanhaAsync(SalvarCampanhaRequest r) => Enviar<Campanha>(HttpMethod.Post, "api/campanhas", r);
    public Task<Resultado<Campanha>> AtualizarCampanhaAsync(Guid id, SalvarCampanhaRequest r) => Enviar<Campanha>(HttpMethod.Put, $"api/campanhas/{id}", r);
    public Task<Resultado<List<DoacaoGestao>>> DoacoesGestaoAsync(Guid? campanha = null, string? status = null, int limite = 200)
        => Enviar<List<DoacaoGestao>>(HttpMethod.Get, $"api/doacoes?limite={limite}{(campanha is null ? "" : $"&idCampanha={campanha}")}{(status is null ? "" : $"&status={status}")}");
    public Task<Resultado<List<DoadorGestao>>> DoadoresAsync() => Enviar<List<DoadorGestao>>(HttpMethod.Get, "api/gestao/doadores");
    public Task<Resultado<ResumoGestao>> ResumoAsync(int dias) => Enviar<ResumoGestao>(HttpMethod.Get, $"api/gestao/resumo?dias={dias}");

    private async Task<Resultado<T>> Enviar<T>(HttpMethod metodo, string rota, object? corpo = null, bool autenticado = true)
    {
        using var req = new HttpRequestMessage(metodo, rota);
        if (corpo is not null) req.Content = JsonContent.Create(corpo, options: Json);
        if (autenticado && _sessao.Logado) req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _sessao.Token);

        HttpResponseMessage resp;
        // TaskCanceledException aqui é o tempo limite do HttpClient (nenhuma chamada passa token de cancelamento).
        try { resp = await _http.SendAsync(req); }
        catch (Exception e) when (e is HttpRequestException or TaskCanceledException) { return Resultado<T>.Falha(Problema.SemConexao()); }

        using (resp)
        {
            if (resp.IsSuccessStatusCode)
            {
                // Resposta 200 que não é JSON (ex.: index.html de um proxy mal configurado) vira erro, não exceção.
                try { return Resultado<T>.De((await resp.Content.ReadFromJsonAsync<T>(Json))!); }
                catch (Exception e) when (e is JsonException or NotSupportedException or TaskCanceledException)
                {
                    return Resultado<T>.Falha(new Problema(502, Padrao(500), new Dictionary<string, string[]>()));
                }
            }

            if (resp.StatusCode == HttpStatusCode.Unauthorized && autenticado && _sessao.Token is not null)
            {
                // Token vencido ou inválido: encerra a sessão e leva para o login, voltando depois para onde estava.
                _sessao.VoltarPara = _nav.ToBaseRelativePath(_nav.Uri);
                await _sessao.SairAsync();
                _avisos.Info("Sua sessão expirou. Entre de novo para continuar.");
                _nav.NavigateTo("entrar");
            }
            return Resultado<T>.Falha(await LerProblemaAsync(resp));
        }
    }

    private static async Task<Problema> LerProblemaAsync(HttpResponseMessage resp)
    {
        var status = (int)resp.StatusCode;
        try
        {
            var json = await resp.Content.ReadFromJsonAsync<JsonElement>();
            var campos = new Dictionary<string, string[]>();
            if (json.TryGetProperty("errors", out var erros) && erros.ValueKind == JsonValueKind.Object)
                foreach (var c in erros.EnumerateObject())
                    campos[c.Name] = c.Value.EnumerateArray().Select(x => x.GetString() ?? "").ToArray();

            var mensagem = json.TryGetProperty("detail", out var d) && d.GetString() is { Length: > 0 } detalhe ? detalhe
                : json.TryGetProperty("title", out var t) && t.GetString() is { Length: > 0 } titulo ? titulo
                : Padrao(status);
            return new Problema(status, mensagem, campos);
        }
        catch (Exception e) when (e is JsonException or NotSupportedException)
        {
            return new Problema(status, Padrao(status), new Dictionary<string, string[]>());
        }
    }

    private static string Padrao(int status) => status switch
    {
        401 => "Entre na sua conta para continuar.",
        403 => "Seu perfil não tem permissão para esta ação.",
        404 => "Não encontramos o que você procurou.",
        >= 500 => "Algo deu errado do nosso lado. Tente de novo em alguns segundos.",
        _ => "Não foi possível concluir a operação. Revise os dados e tente de novo."
    };
}
