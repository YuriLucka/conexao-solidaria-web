namespace ConexaoSolidaria.Web.Servicos;

// Avisos rápidos no topo da tela (toast do protótipo).
public class Avisos
{
    public record Aviso(Guid Id, string Texto, string Tipo);

    private readonly List<Aviso> _lista = [];
    public IReadOnlyList<Aviso> Lista => _lista;
    public event Action? Mudou;

    public void Ok(string texto) => Mostrar(texto, "ok");
    public void Erro(string texto) => Mostrar(texto, "bad");
    public void Info(string texto) => Mostrar(texto, "");

    private void Mostrar(string texto, string tipo)
    {
        var aviso = new Aviso(Guid.NewGuid(), texto, tipo);
        _lista.Add(aviso);
        Mudou?.Invoke();
        _ = Task.Delay(3800).ContinueWith(_ => { _lista.Remove(aviso); Mudou?.Invoke(); });
    }
}
