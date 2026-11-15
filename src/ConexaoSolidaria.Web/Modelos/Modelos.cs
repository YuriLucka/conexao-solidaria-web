namespace ConexaoSolidaria.Web.Modelos;

// Contratos da campanhas-api (mesmos nomes de campo do JSON, camelCase via System.Text.Json Web).

public record LoginRequest(string Email, string Senha);
public record TokenResponse(string AccessToken, string TokenType, DateTime ExpiraEm, string Nome, string Perfil);

public record CadastrarDoadorRequest(string NomeCompleto, string Email, string Cpf, string Senha);
public record DoadorResponse(Guid Id, string NomeCompleto, string Email);

public record CampanhaAtiva(
    Guid Id, string Titulo, string Descricao, decimal MetaFinanceira, decimal ValorTotalArrecadado,
    DateTime DataInicio, DateTime DataFim, bool PrazoEncerrado, string? Categoria, string? Capa, int? CriancasAtendidas);

public record Campanha(
    Guid Id, string Titulo, string Descricao, DateTime DataInicio, DateTime DataFim, decimal MetaFinanceira,
    string Status, decimal ValorTotalArrecadado, bool PrazoEncerrado, string? Categoria, string? Capa, int? CriancasAtendidas);

public record SalvarCampanhaRequest(
    string Titulo, string Descricao, DateTime DataInicio, DateTime DataFim, decimal MetaFinanceira,
    string? Status, string? Categoria, string? Capa, int? CriancasAtendidas);

public record DoacaoRecente(string Doador, decimal Valor, DateTime ProcessadaEm);
public record DoacoesDaCampanha(int QuantidadeDoacoes, int QuantidadeDoadores, IReadOnlyList<DoacaoRecente> Recentes);

public record CriarDoacaoRequest(Guid IdCampanha, decimal ValorDoacao);
public record Doacao(
    Guid Id, Guid IdCampanha, string? TituloCampanha, decimal ValorDoacao, string Status,
    string? MotivoRejeicao, DateTime CriadaEm, DateTime? ProcessadaEm);

public record DoacaoGestao(
    Guid Id, Guid IdCampanha, string TituloCampanha, Guid DoadorId, string NomeDoador, decimal ValorDoacao,
    string Status, string? MotivoRejeicao, DateTime CriadaEm, DateTime? ProcessadaEm);

public record DoadorGestao(
    Guid Id, string NomeCompleto, string Email, string CpfMascarado, DateTime CriadoEm,
    int QuantidadeDoacoes, decimal TotalDoado, DateTime? UltimaDoacaoEm);

public record ArrecadacaoDia(DateOnly Dia, decimal Valor, int Doacoes);
public record ResumoGestao(
    int Dias, decimal Arrecadado, decimal ArrecadadoPeriodoAnterior, int DoacoesConfirmadas, int DoacoesConfirmadasPeriodoAnterior,
    int DoadoresUnicos, int DoadoresUnicosPeriodoAnterior, decimal TicketMedio, decimal TicketMedioPeriodoAnterior,
    decimal ArrecadadoHoje, int DoacoesHoje, int DoacoesPendentes, IReadOnlyList<ArrecadacaoDia> PorDia);

// Erro da API (ProblemDetails) já traduzido para a tela.
public record Problema(int Status, string Mensagem, IReadOnlyDictionary<string, string[]> Campos)
{
    public static Problema SemConexao() =>
        new(0, "Não conseguimos falar com o servidor. Verifique sua conexão e tente de novo.", new Dictionary<string, string[]>());

    public string? Campo(string nome) => Campos.TryGetValue(nome, out var m) ? m.FirstOrDefault() : null;
}

public readonly record struct Resultado<T>(T? Valor, Problema? Problema)
{
    public bool Ok => Problema is null;
    public static Resultado<T> De(T valor) => new(valor, null);
    public static Resultado<T> Falha(Problema problema) => new(default, problema);
}
