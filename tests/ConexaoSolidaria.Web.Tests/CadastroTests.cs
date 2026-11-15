using System.Net;
using System.Text;
using ConexaoSolidaria.Web.Pages;
using ConexaoSolidaria.Web.Servicos;
using Microsoft.Extensions.DependencyInjection;

namespace ConexaoSolidaria.Web.Tests;

// Tela de cadastro: validação antes de chamar a API e erro da API (409) no campo certo.
public class CadastroTests : BunitContext
{
    private readonly ApiFalsa _api = new();

    public CadastroTests()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        Services.AddSingleton(new HttpClient(_api) { BaseAddress = new Uri("http://api.teste/") });
        Services.AddScoped<Sessao>();
        Services.AddScoped<Avisos>();
        Services.AddScoped<Api>();
    }

    private IRenderedComponent<Cadastro> Preencher(string nome, string email, string cpf, string senha, string confirmacao, bool termos)
    {
        var cut = Render<Cadastro>();
        cut.Find("#c-nome").Change(nome);
        cut.Find("#c-email").Change(email);
        cut.Find("#c-cpf").Input(cpf);
        cut.Find("#c-senha").Input(senha);
        cut.Find("#c-conf").Change(confirmacao);
        if (termos) cut.Find("#c-termos").Change(true);
        return cut;
    }

    [Fact]
    public void Formulario_vazio_mostra_os_erros_e_nao_chama_a_API()
    {
        var cut = Render<Cadastro>();

        cut.Find("form").Submit();

        Assert.NotEmpty(cut.Find("#err-c-nome").TextContent);
        Assert.Equal("Informe um e-mail válido.", cut.Find("#err-c-email").TextContent);
        Assert.Equal("CPF inválido. Confira os números.", cut.Find("#err-c-cpf").TextContent);
        Assert.NotEmpty(cut.Find("#err-c-termos").TextContent);
        Assert.Equal(0, _api.Chamadas);
    }

    [Fact]
    public void Cpf_ganha_mascara_ao_digitar()
    {
        var cut = Render<Cadastro>();
        cut.Find("#c-cpf").Input("52998224725");

        Assert.Equal("529.982.247-25", cut.Find("#c-cpf").GetAttribute("value"));
    }

    [Fact]
    public void Senhas_diferentes_sao_barradas()
    {
        var cut = Preencher("Ana Souza", "ana@email.demo", "529.982.247-25", "Doar@2026", "Outra@2026", termos: true);

        cut.Find("form").Submit();

        Assert.Equal("As senhas não são iguais.", cut.Find("#err-c-conf").TextContent);
        Assert.Equal(0, _api.Chamadas);
    }

    [Fact]
    public void Email_repetido_na_API_aparece_no_campo_de_email()
    {
        _api.Responder(HttpStatusCode.Conflict, """{"status":409,"title":"Conflito","detail":"Já existe uma conta com este e-mail."}""");
        var cut = Preencher("Ana Souza", "ana@email.demo", "529.982.247-25", "Doar@2026", "Doar@2026", termos: true);

        cut.Find("form").Submit();

        cut.WaitForAssertion(() => Assert.Equal("Já existe uma conta com este e-mail.", cut.Find("#err-c-email").TextContent));
        Assert.Equal(1, _api.Chamadas);
    }

    // HttpMessageHandler que devolve uma resposta fixa e conta as chamadas.
    private sealed class ApiFalsa : HttpMessageHandler
    {
        private HttpStatusCode _status = HttpStatusCode.Created;
        private string _corpo = "{}";
        public int Chamadas { get; private set; }

        public void Responder(HttpStatusCode status, string corpo) => (_status, _corpo) = (status, corpo);

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken ct)
        {
            Chamadas++;
            return Task.FromResult(new HttpResponseMessage(_status)
            {
                Content = new StringContent(_corpo, Encoding.UTF8, "application/problem+json")
            });
        }
    }
}
