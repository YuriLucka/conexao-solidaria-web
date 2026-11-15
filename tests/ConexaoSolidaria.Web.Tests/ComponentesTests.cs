using ConexaoSolidaria.Web.Componentes;

namespace ConexaoSolidaria.Web.Tests;

// Componentes visuais renderizados com bUnit.
public class ComponentesTests : BunitContext
{
    [Fact]
    public void Medidor_informa_a_porcentagem_para_leitor_de_tela()
    {
        var cut = Render<Medidor>(p => p.Add(m => m.Progresso, 0.8));
        var barra = cut.Find("[role=progressbar]");

        Assert.Equal("80", barra.GetAttribute("aria-valuenow"));
        Assert.Contains("width:80.0%", cut.Find("span").GetAttribute("style"));
    }

    [Fact]
    public void Medidor_acima_da_meta_fica_cheio_sem_passar_de_100()
    {
        var cut = Render<Medidor>(p => p.Add(m => m.Progresso, 1.3));

        Assert.Contains("full", cut.Find(".meter").ClassName);
        Assert.Equal("100", cut.Find("[role=progressbar]").GetAttribute("aria-valuenow"));
    }

    [Theory]
    [InlineData("Ativa", "Ativa", "pill-ok")]
    [InlineData("Concluida", "Concluída", "pill-info")]
    [InlineData("Cancelada", "Cancelada", "pill-bad")]
    public void Selo_do_status_da_campanha(string status, string texto, string classe)
    {
        var cut = Render<SeloStatus>(p => p.Add(s => s.Status, status));

        Assert.Contains(texto, cut.Markup);
        Assert.Contains(classe, cut.Find(".pill").ClassName);
    }

    [Theory]
    [InlineData("Pendente", "Em confirmação")]
    [InlineData("Processada", "Confirmada")]
    [InlineData("Rejeitada", "Não concluída")]
    public void Selo_do_andamento_da_doacao(string status, string texto)
        => Assert.Contains(texto, Render<SeloDoacao>(p => p.Add(s => s.Status, status)).Markup);

    [Fact]
    public void Card_de_campanha_ativa_tem_botao_doar_com_link()
    {
        var id = Guid.NewGuid();
        var cut = Render<CardCampanha>(p => p
            .Add(c => c.Id, id).Add(c => c.Titulo, "Natal").Add(c => c.Meta, 1000m).Add(c => c.Arrecadado, 250m)
            .Add(c => c.DataFim, DateTime.UtcNow.AddDays(10)).Add(c => c.Categoria, "Educação"));

        Assert.Equal($"campanhas/{id}", cut.Find("a.btn-doar").GetAttribute("href"));
        Assert.Contains("25%", cut.Find(".pct").TextContent);
        Assert.Contains("Educação", cut.Find(".cat-tag").TextContent);
    }

    [Fact]
    public void Card_de_campanha_com_prazo_encerrado_nao_oferece_doar()
    {
        var cut = Render<CardCampanha>(p => p
            .Add(c => c.Id, Guid.NewGuid()).Add(c => c.Titulo, "Antiga").Add(c => c.Meta, 1000m)
            .Add(c => c.DataFim, DateTime.UtcNow.AddDays(-1)).Add(c => c.PrazoEncerrado, true));

        Assert.Empty(cut.FindAll("a.btn-doar"));
        Assert.Contains("Prazo encerrado", cut.Markup);
    }

    [Fact]
    public void Capa_sem_escolha_usa_a_da_categoria()
    {
        var porCategoria = Render<Capa>(p => p.Add(c => c.Categoria, "Saúde")).Markup;
        var saude = Render<Capa>(p => p.Add(c => c.Escolhida, "saude")).Markup;

        Assert.Equal(saude, porCategoria);
    }

    [Fact]
    public void Solzinho_respeita_o_tamanho()
    {
        var svg = Render<Sol>(p => p.Add(s => s.Expressao, "acenando").Add(s => s.Tamanho, 80)).Find("svg");

        Assert.Equal("80", svg.GetAttribute("width"));
        Assert.Equal("70", svg.GetAttribute("height"));
    }
}
