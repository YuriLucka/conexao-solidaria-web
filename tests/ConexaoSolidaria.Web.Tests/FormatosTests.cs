using ConexaoSolidaria.Web.Marca;

namespace ConexaoSolidaria.Web.Tests;

public class FormatosTests
{
    [Theory]
    [InlineData(1234.5, "R$ 1.234,50")]
    [InlineData(0, "R$ 0,00")]
    [InlineData(47398.22, "R$ 47.398,22")]
    [InlineData(1000000, "R$ 1.000.000,00")]
    [InlineData(0.01, "R$ 0,01")]
    [InlineData(-10, "-R$ 10,00")]
    public void Dinheiro_no_formato_brasileiro(decimal valor, string esperado)
        => Assert.Equal(esperado, Formatos.Dinheiro(valor));

    [Theory]
    [InlineData(59999.5, "R$ 59.999")]
    [InlineData(99.6, "R$ 99")]
    [InlineData(0.5, "R$ 0")]
    [InlineData(18326757.99, "R$ 18.326.757")]
    public void Dinheiro_inteiro_trunca_para_nunca_mostrar_mais_do_que_entrou(decimal valor, string esperado)
        => Assert.Equal(esperado, Formatos.DinheiroInteiro(valor));

    [Theory]
    [InlineData(0.4, "R$ 1")]
    [InlineData(1500, "R$ 1.500")]
    [InlineData(-20, "R$ 0")]
    public void Falta_arredonda_para_cima(decimal valor, string esperado)
        => Assert.Equal(esperado, Formatos.DinheiroFalta(valor));

    [Theory]
    [InlineData(0, "0%")]
    [InlineData(0.004, "<1%")]
    [InlineData(0.29, "29%")]
    [InlineData(0.996, "99%")]
    [InlineData(1, "100%")]
    [InlineData(5.63, "563%")]
    public void Porcentagem_so_mostra_100_quando_a_meta_foi_atingida(double progresso, string esperado)
        => Assert.Equal(esperado, Formatos.Porcentagem(progresso));

    [Theory]
    [InlineData("52998224725", "529.982.247-25")]
    [InlineData("5299", "529.9")]
    [InlineData("529.982.2", "529.982.2")]
    [InlineData("abc529982247259999", "529.982.247-25")]
    public void Mascara_de_cpf_enquanto_digita(string entrada, string esperado)
        => Assert.Equal(esperado, Formatos.MascaraCpf(entrada));

    [Theory]
    [InlineData("529.982.247-25", true)]
    [InlineData("111.444.777-35", true)]
    [InlineData("529.982.247-24", false)]
    [InlineData("111.111.111-11", false)]
    [InlineData("123", false)]
    public void Cpf_valido_pelos_digitos_verificadores(string cpf, bool valido)
        => Assert.Equal(valido, Formatos.CpfValido(cpf));

    [Theory]
    [InlineData("1.234,56", 1234.56)]
    [InlineData("R$ 50", 50)]
    [InlineData("37,5", 37.5)]
    [InlineData("1.500", 1500)]
    [InlineData("10.5", 10.5)]
    public void Le_valor_digitado(string texto, double esperado)
        => Assert.Equal((decimal)esperado, Formatos.LerDinheiro(texto));

    [Theory]
    [InlineData("")]
    [InlineData("abc")]
    public void Texto_sem_numero_nao_vira_valor(string texto)
        => Assert.Null(Formatos.LerDinheiro(texto));

    [Theory]
    [InlineData("15112026", "15/11/2026")]
    [InlineData("1511", "15/11")]
    [InlineData("15/11/2026", "15/11/2026")]
    public void Mascara_de_data(string entrada, string esperado)
        => Assert.Equal(esperado, Formatos.MascaraData(entrada));

    [Fact]
    public void Datas_aparecem_no_horario_de_Brasilia()
        => Assert.Equal("14/11/2026 22:30", Formatos.DataHora(new DateTime(2026, 11, 15, 1, 30, 0, DateTimeKind.Utc)));

    [Fact]
    public void Prazo_mostra_os_estados_da_campanha()
    {
        Assert.Equal("Cancelada", Formatos.Prazo("Cancelada", DateTime.UtcNow.AddDays(5), false));
        var ontem = DateTime.UtcNow.AddDays(-1);
        Assert.Equal($"Terminou em {Formatos.Data(ontem)}", Formatos.Prazo("Ativa", ontem, true));
        Assert.Equal("Termina amanhã", Formatos.Prazo("Ativa", DateTime.UtcNow.AddHours(20), false));
        Assert.Equal("Faltam 10 dias", Formatos.Prazo("Ativa", DateTime.UtcNow.AddDays(9.5), false));
    }

    [Theory]
    [InlineData("Mariana Albuquerque", "MA")]
    [InlineData("Helena", "H")]
    [InlineData("Paulo Roberto Souza", "PS")]
    public void Iniciais_do_avatar(string nome, string esperado)
        => Assert.Equal(esperado, Formatos.Iniciais(nome));

    [Fact]
    public void Progresso_sem_meta_e_zero()
        => Assert.Equal(0, Formatos.Progresso(100, 0));
}
