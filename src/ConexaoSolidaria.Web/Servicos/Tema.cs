using Microsoft.JSInterop;

namespace ConexaoSolidaria.Web.Servicos;

// Tema claro/escuro do protótipo (atributo data-cs-tema no <html>), lembrado no navegador.
public class Tema
{
    private readonly IJSRuntime _js;

    public Tema(IJSRuntime js) => _js = js;

    public bool Escuro { get; private set; }
    public event Action? Mudou;

    public async Task CarregarAsync() => Escuro = await _js.InvokeAsync<string>("cs.temaPreferido") == "escuro";

    public async Task AlternarAsync()
    {
        Escuro = !Escuro;
        await _js.InvokeVoidAsync("cs.aplicarTema", Escuro ? "escuro" : "claro");
        Mudou?.Invoke();
    }
}
