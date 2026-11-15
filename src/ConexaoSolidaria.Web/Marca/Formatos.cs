using System.Globalization;

namespace ConexaoSolidaria.Web.Marca;

// Formatação em português do Brasil, igual à do protótipo. Não depende dos dados de globalização do navegador:
// moeda e números usam um NumberFormatInfo próprio; datas são mostradas no horário de Brasília (UTC-3).
public static class Formatos
{
    public static readonly TimeSpan Brasilia = TimeSpan.FromHours(-3);

    private static readonly NumberFormatInfo Br = new()
    {
        NumberDecimalSeparator = ",",
        NumberGroupSeparator = ".",
        CurrencyDecimalSeparator = ",",
        CurrencyGroupSeparator = ".",
        CurrencySymbol = "R$",
        CurrencyPositivePattern = 2, // "R$ 1.234,56"
        CurrencyNegativePattern = 9,
        NumberGroupSizes = [3],
        CurrencyGroupSizes = [3]
    };

    public static string Dinheiro(decimal v) => v.ToString("C2", Br);
    public static string DinheiroInteiro(decimal v) => decimal.Round(v, 0, MidpointRounding.AwayFromZero).ToString("C0", Br);
    public static string Numero(int v) => v.ToString("N0", Br);
    public static string Valor(decimal v) => v.ToString("N2", Br);
    public static string Porcentagem(double p) => $"{Math.Round(p * 100):0}%";

    public static DateTime ParaBrasilia(DateTime utc) => DateTime.SpecifyKind(Utc(utc) + Brasilia, DateTimeKind.Unspecified);
    public static string Data(DateTime utc) => ParaBrasilia(utc).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
    public static string DataHora(DateTime utc) => ParaBrasilia(utc).ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
    public static string DiaMes(DateOnly d) => d.ToString("dd/MM", CultureInfo.InvariantCulture);

    private static DateTime Utc(DateTime d) => d.Kind == DateTimeKind.Utc ? d : DateTime.SpecifyKind(d, DateTimeKind.Utc);

    public static double Progresso(decimal arrecadado, decimal meta) => meta > 0 ? (double)(arrecadado / meta) : 0;

    public static int DiasRestantes(DateTime dataFim) => (int)Math.Ceiling((Utc(dataFim) - DateTime.UtcNow).TotalDays);

    // "Faltam 13 dias", "Termina amanhã", "Termina hoje", "Prazo encerrado".
    public static string Prazo(string status, DateTime dataFim, bool prazoEncerrado)
    {
        if (status == "Concluida") return $"Encerrada em {Data(dataFim)}";
        if (status == "Cancelada") return "Cancelada";
        if (prazoEncerrado) return "Prazo encerrado";
        var d = DiasRestantes(dataFim);
        return d <= 0 ? "Termina hoje" : d == 1 ? "Termina amanhã" : $"Faltam {d} dias";
    }

    public static string Ha(DateTime utc)
    {
        var s = (DateTime.UtcNow - Utc(utc)).TotalSeconds;
        if (s < 10) return "agora mesmo";
        if (s < 60) return $"há {Math.Round(s)} s";
        if (s < 3600) return $"há {Math.Round(s / 60)} min";
        if (s < 86400) return $"há {Math.Round(s / 3600)} h";
        var d = (int)Math.Round(s / 86400);
        return d == 1 ? "ontem" : $"há {d} dias";
    }

    public static string PrimeiroNome(string nome) => nome.Split(' ', StringSplitOptions.RemoveEmptyEntries).FirstOrDefault() ?? nome;

    public static string Iniciais(string nome)
    {
        var p = nome.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (p.Length == 0) return "";
        return (p.Length > 1 ? $"{p[0][0]}{p[^1][0]}" : $"{p[0][0]}").ToUpperInvariant();
    }

    // Cor do avatar (av0 a av3), estável por nome.
    public static int CorAvatar(string chave) => Math.Abs(chave.Aggregate(17, (h, c) => h * 31 + c)) % 4;

    // "R$ 1.234,56", "1234,56", "1.234" -> decimal. Null se não for número.
    public static decimal? LerDinheiro(string? texto)
    {
        var s = (texto ?? "").Replace("R$", "").Replace(" ", "").Trim();
        if (s.Length == 0) return null;
        if (s.Contains(',')) s = s.Replace(".", "").Replace(',', '.');
        else if (System.Text.RegularExpressions.Regex.IsMatch(s, @"^\d{1,3}(\.\d{3})+$")) s = s.Replace(".", "");
        return decimal.TryParse(s, NumberStyles.Number, CultureInfo.InvariantCulture, out var v) ? v : null;
    }

    public static string MascaraCpf(string texto)
    {
        var d = new string(texto.Where(char.IsDigit).Take(11).ToArray());
        return d.Length switch
        {
            <= 3 => d,
            <= 6 => $"{d[..3]}.{d[3..]}",
            <= 9 => $"{d[..3]}.{d[3..6]}.{d[6..]}",
            _ => $"{d[..3]}.{d[3..6]}.{d[6..9]}-{d[9..]}"
        };
    }

    // "15112026" -> "15/11/2026" enquanto digita.
    public static string MascaraData(string? texto)
    {
        var d = new string((texto ?? "").Where(char.IsDigit).Take(8).ToArray());
        return d.Length switch
        {
            <= 2 => d,
            <= 4 => $"{d[..2]}/{d[2..]}",
            _ => $"{d[..2]}/{d[2..4]}/{d[4..]}"
        };
    }

    // Mesma regra da API (dígitos verificadores), para avisar antes de enviar.
    public static bool CpfValido(string texto)
    {
        var d = texto.Where(char.IsDigit).Select(c => c - '0').ToArray();
        if (d.Length != 11 || d.All(x => x == d[0])) return false;
        int Dv(int n) { var s = 0; for (var i = 0; i < n; i++) s += d[i] * (n + 1 - i); var r = s % 11; return r < 2 ? 0 : 11 - r; }
        return d[9] == Dv(9) && d[10] == Dv(10);
    }
}
