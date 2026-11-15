// Gerado a partir do protótipo do grupo (Conexão Solidária.html): mesmos desenhos, cores pelas variáveis CSS do tema.
// Não editar à mão; regerar com os scripts extrair-arte.mjs e gen_arte.py se o protótipo mudar.
namespace ConexaoSolidaria.Web.Marca;

public static class Arte
{
    public const string Simbolo = """
        <svg viewBox="0 0 64 64" aria-hidden="true"> <circle cx="17" cy="13" r="6" style="fill:var(--logo-a)"/><circle cx="47" cy="13" r="6" style="fill:var(--logo-b)"/> <path d="M22.5 24C8 26 8 42 32 57" style="fill:none;stroke:var(--logo-a)" stroke-width="6.5" stroke-linecap="round"/> <path d="M41.5 24C56 26 56 42 32 57" style="fill:none;stroke:var(--logo-b)" stroke-width="6.5" stroke-linecap="round"/> <circle cx="32" cy="35" r="5.5" style="fill:var(--logo-c)"/></svg>
        """;

    // Capa usada quando a campanha não escolheu uma: a da categoria, ou "sol-colinas".
    public static readonly IReadOnlyDictionary<string, string> CapaPorCategoria = new Dictionary<string, string>
    {
        ["Datas especiais"] = "presentes",
        ["Educação"] = "livros",
        ["Infraestrutura"] = "casa",
        ["Saúde"] = "saude",
        ["Alimentação"] = "horta",
        ["Esporte e lazer"] = "bola",
        ["Inverno"] = "inverno",
    };

    public static readonly IReadOnlyList<(string Id, string Nome)> Capas =
    [
        ("sol-colinas", "Sol e colinas"),
        ("amanhecer", "Amanhecer"),
        ("livros", "Livros"),
        ("presentes", "Presentes"),
        ("casa", "Lar"),
        ("saude", "Cuidado"),
        ("horta", "Horta"),
        ("bola", "Esporte"),
        ("inverno", "Inverno"),
        ("estrelas", "Noite estrelada"),
        ("abraco", "Abraço"),
        ("festa", "Festa"),
    ];

    public static readonly IReadOnlyDictionary<string, string> CapaSvg = new Dictionary<string, string>
    {
        ["sol-colinas"] = """
        <svg viewBox="0 0 320 150" preserveAspectRatio="xMidYMid slice" aria-hidden="true"><rect width="320" height="150" style="fill:var(--sol-tinta)"/><circle cx="72" cy="36" r="4" style="fill:var(--coral)" opacity=".5"/><circle cx="112" cy="58" r="3" style="fill:var(--verde)" opacity=".5"/><circle cx="178" cy="28" r="3" style="fill:var(--coral)" opacity=".45"/><circle cx="236" cy="50" r="24" style="fill:var(--sol)"/><path d="M-20 150C40 82 120 88 190 150Z" style="fill:var(--verde)"/><path d="M120 150C180 76 270 88 340 150Z" style="fill:var(--coral)" opacity=".92"/></svg>
        """,
        ["amanhecer"] = """
        <svg viewBox="0 0 320 150" preserveAspectRatio="xMidYMid slice" aria-hidden="true"><rect width="320" height="150" style="fill:var(--coral-tinta)"/><line x1="102.0" y1="96.5" x2="86.6" y2="92.3" style="fill:none;stroke:var(--sol)" stroke-width="6" stroke-linecap="round" stroke-linejoin="round"/><line x1="114.0" y1="73.4" x2="101.8" y2="63.1" style="fill:none;stroke:var(--sol)" stroke-width="6" stroke-linecap="round" stroke-linejoin="round"/><line x1="134.6" y1="57.6" x2="127.9" y2="43.1" style="fill:none;stroke:var(--sol)" stroke-width="6" stroke-linecap="round" stroke-linejoin="round"/><line x1="160.0" y1="52.0" x2="160.0" y2="36.0" style="fill:none;stroke:var(--sol)" stroke-width="6" stroke-linecap="round" stroke-linejoin="round"/><line x1="185.4" y1="57.6" x2="192.1" y2="43.1" style="fill:none;stroke:var(--sol)" stroke-width="6" stroke-linecap="round" stroke-linejoin="round"/><line x1="206.0" y1="73.4" x2="218.2" y2="63.1" style="fill:none;stroke:var(--sol)" stroke-width="6" stroke-linecap="round" stroke-linejoin="round"/><line x1="218.0" y1="96.5" x2="233.4" y2="92.3" style="fill:none;stroke:var(--sol)" stroke-width="6" stroke-linecap="round" stroke-linejoin="round"/><circle cx="160" cy="112" r="46" style="fill:var(--sol)"/><path d="M0 114Q160 98 320 114V150H0Z" style="fill:var(--verde)"/><path d="M0 132Q100 120 200 134T320 128V150H0Z" style="fill:var(--coral)" opacity=".9"/></svg>
        """,
        ["livros"] = """
        <svg viewBox="0 0 320 150" preserveAspectRatio="xMidYMid slice" aria-hidden="true"><rect width="320" height="150" style="fill:var(--sol-tinta)"/><circle cx="256" cy="44" r="20" style="fill:var(--sol)"/><rect x="40" y="118" width="240" height="7" rx="3.5" style="fill:var(--verde)" opacity=".35"/> <rect x="78" y="52" width="26" height="66" rx="4" style="fill:var(--verde)"/><rect x="108" y="38" width="22" height="80" rx="4" style="fill:var(--coral)"/><rect x="134" y="60" width="30" height="58" rx="4" style="fill:var(--info)"/> <rect x="84" y="62" width="14" height="4" rx="2" style="fill:var(--surface)" opacity=".75"/><rect x="112" y="50" width="14" height="4" rx="2" style="fill:var(--surface)" opacity=".75"/><rect x="140" y="72" width="18" height="4" rx="2" style="fill:var(--surface)" opacity=".75"/> <g transform="rotate(14 170 118)"><rect x="170" y="58" width="26" height="60" rx="4" style="fill:var(--verde)" opacity=".75"/></g> <rect x="214" y="96" width="54" height="22" rx="4" style="fill:var(--coral)" opacity=".85"/><rect x="219" y="102" width="44" height="3" rx="1.5" style="fill:var(--surface)" opacity=".75"/></svg>
        """,
        ["presentes"] = """
        <svg viewBox="0 0 320 150" preserveAspectRatio="xMidYMid slice" aria-hidden="true"><rect width="320" height="150" style="fill:var(--coral-tinta)"/><circle cx="40" cy="30" r="4" style="fill:var(--sol)"/><circle cx="292" cy="40" r="5" style="fill:var(--verde)" opacity=".6"/><circle cx="250" cy="22" r="3" style="fill:var(--coral)"/><circle cx="178" cy="34" r="3.5" style="fill:var(--sol)"/><circle cx="30" cy="98" r="3" style="fill:var(--verde)" opacity=".5"/><rect x="40" y="132" width="250" height="4" rx="2" style="fill:var(--coral)" opacity=".25"/> <rect x="70" y="72" width="72" height="60" rx="6" style="fill:var(--verde)" opacity=".88"/><rect x="64" y="60" width="84" height="16" rx="5" style="fill:var(--verde)"/><rect x="101" y="60" width="10" height="72" style="fill:var(--sol)"/> <circle cx="98" cy="51" r="8" style="fill:none;stroke:var(--sol)" stroke-width="5" stroke-linecap="round" stroke-linejoin="round"/><circle cx="114" cy="51" r="8" style="fill:none;stroke:var(--sol)" stroke-width="5" stroke-linecap="round" stroke-linejoin="round"/> <rect x="160" y="90" width="58" height="42" rx="6" style="fill:var(--coral)" opacity=".88"/><rect x="155" y="80" width="68" height="13" rx="4" style="fill:var(--coral)"/><rect x="184" y="80" width="9" height="52" style="fill:var(--surface)" opacity=".85"/> <rect x="232" y="104" width="40" height="28" rx="5" style="fill:var(--sol)" opacity=".9"/><rect x="228" y="96" width="48" height="10" rx="4" style="fill:var(--sol)"/><rect x="248" y="96" width="8" height="36" style="fill:var(--verde)"/></svg>
        """,
        ["casa"] = """
        <svg viewBox="0 0 320 150" preserveAspectRatio="xMidYMid slice" aria-hidden="true"><rect width="320" height="150" style="fill:var(--surface-2)"/><circle cx="254" cy="40" r="16" style="fill:var(--sol)"/><path d="M-20 150C40 100 100 104 150 150Z" style="fill:var(--verde)" opacity=".22"/><path d="M170 150C220 96 290 100 340 150Z" style="fill:var(--verde)" opacity=".22"/> <path d="M0 132Q160 124 320 132V150H0Z" style="fill:var(--verde)" opacity=".45"/> <rect x="184" y="44" width="12" height="24" rx="2" style="fill:var(--coral)"/> <rect x="118" y="74" width="84" height="60" rx="4" style="fill:var(--verde)"/> <path d="M110 78L160 40L210 78Z" style="fill:var(--coral);stroke:var(--coral)" stroke-width="8" stroke-linejoin="round"/> <rect x="128" y="88" width="16" height="14" rx="3" style="fill:var(--sol-tinta)"/><rect x="176" y="88" width="16" height="14" rx="3" style="fill:var(--sol-tinta)"/> <path d="M150 134V112a10 10 0 0 1 20 0V134Z" style="fill:var(--sol)"/></svg>
        """,
        ["saude"] = """
        <svg viewBox="0 0 320 150" preserveAspectRatio="xMidYMid slice" aria-hidden="true"><rect width="320" height="150" style="fill:var(--verde-tinta)"/><circle cx="78" cy="42" r="12" style="fill:var(--sol)"/><circle cx="88" cy="112" r="7" style="fill:var(--verde)" opacity=".45"/><circle cx="252" cy="108" r="9" style="fill:var(--sol)" opacity=".8"/><circle cx="236" cy="34" r="4" style="fill:var(--coral)" opacity=".5"/><rect x="114" y="59" width="92" height="32" rx="12" style="fill:var(--coral)"/><rect x="144" y="29" width="32" height="92" rx="12" style="fill:var(--coral)"/> <path transform="translate(214 46) scale(1.8)" d="M8 14S0 9.4 0 4.2C0 1.6 2 0 4.2 0 5.8 0 7.2.9 8 2.3 8.8.9 10.2 0 11.8 0 14 0 16 1.6 16 4.2 16 9.4 8 14 8 14Z" style="fill:var(--verde)"/></svg>
        """,
        ["horta"] = """
        <svg viewBox="0 0 320 150" preserveAspectRatio="xMidYMid slice" aria-hidden="true"><rect width="320" height="150" style="fill:var(--sol-tinta)"/><circle cx="56" cy="40" r="17" style="fill:var(--sol)"/><line x1="70" y1="120" x2="70" y2="86" style="fill:none;stroke:var(--verde)" stroke-width="4" stroke-linecap="round" stroke-linejoin="round"/><path d="M70 98c-6-12-18-14-24-10 2 10 12 16 24 10Z" style="fill:var(--verde)"/><path d="M70 90c6-12 18-14 24-10-2 10-12 16-24 10Z" style="fill:var(--verde)" opacity=".8"/><line x1="118" y1="120" x2="118" y2="76" style="fill:none;stroke:var(--verde)" stroke-width="4" stroke-linecap="round" stroke-linejoin="round"/><path d="M118 88c-6-12-18-14-24-10 2 10 12 16 24 10Z" style="fill:var(--verde)"/><path d="M118 80c6-12 18-14 24-10-2 10-12 16-24 10Z" style="fill:var(--verde)" opacity=".8"/><line x1="166" y1="120" x2="166" y2="90" style="fill:none;stroke:var(--verde)" stroke-width="4" stroke-linecap="round" stroke-linejoin="round"/><path d="M166 102c-6-12-18-14-24-10 2 10 12 16 24 10Z" style="fill:var(--verde)"/><path d="M166 94c6-12 18-14 24-10-2 10-12 16-24 10Z" style="fill:var(--verde)" opacity=".8"/><line x1="214" y1="120" x2="214" y2="72" style="fill:none;stroke:var(--verde)" stroke-width="4" stroke-linecap="round" stroke-linejoin="round"/><path d="M214 84c-6-12-18-14-24-10 2 10 12 16 24 10Z" style="fill:var(--verde)"/><path d="M214 76c6-12 18-14 24-10-2 10-12 16-24 10Z" style="fill:var(--verde)" opacity=".8"/><line x1="262" y1="120" x2="262" y2="84" style="fill:none;stroke:var(--verde)" stroke-width="4" stroke-linecap="round" stroke-linejoin="round"/><path d="M262 96c-6-12-18-14-24-10 2 10 12 16 24 10Z" style="fill:var(--verde)"/><path d="M262 88c6-12 18-14 24-10-2 10-12 16-24 10Z" style="fill:var(--verde)" opacity=".8"/><path d="M0 118Q160 106 320 118V150H0Z" style="fill:var(--coral)" opacity=".8"/><path d="M0 134Q160 124 320 136V150H0Z" style="fill:var(--coral)"/></svg>
        """,
        ["bola"] = """
        <svg viewBox="0 0 320 150" preserveAspectRatio="xMidYMid slice" aria-hidden="true"><rect width="320" height="150" style="fill:var(--verde-tinta)"/><circle cx="264" cy="38" r="14" style="fill:var(--sol)"/><path d="M0 112Q160 98 320 112V150H0Z" style="fill:var(--verde)"/><path d="M40 150Q160 116 280 150" style="fill:none;stroke:#FFFFFF" stroke-width="3" opacity=".5"/> <line x1="92" y1="62" x2="124" y2="62" style="fill:none;stroke:var(--coral)" stroke-width="5" stroke-linecap="round" stroke-linejoin="round"/><line x1="78" y1="78" x2="120" y2="78" style="fill:none;stroke:var(--coral)" stroke-width="5" stroke-linecap="round" stroke-linejoin="round" opacity=".8"/><line x1="96" y1="94" x2="124" y2="94" style="fill:none;stroke:var(--coral)" stroke-width="5" stroke-linecap="round" stroke-linejoin="round" opacity=".6"/> <circle cx="172" cy="78" r="34" style="fill:#FFFFFF;stroke:#10241F" stroke-width="3"/><path d="M172 64l13 9.5-5 15.5h-16l-5-15.5z" style="fill:#10241F"/> <path d="M172 64V46M185 73.5l15-6M180 89l9 13M164 89l-9 13M159 73.5l-15-6" style="fill:none;stroke:#10241F" stroke-width="3" stroke-linecap="round"/></svg>
        """,
        ["inverno"] = """
        <svg viewBox="0 0 320 150" preserveAspectRatio="xMidYMid slice" aria-hidden="true"><rect width="320" height="150" style="fill:var(--info-bg)"/><circle cx="60" cy="40" r="15" style="fill:var(--sol)" opacity=".85"/><g transform="translate(130 36) scale(1.2)" style="fill:none;stroke:var(--info)" stroke-width="2.4" stroke-linecap="round" stroke-linejoin="round"><path d="M0-10V10M-8.7-5 8.7 5M-8.7 5 8.7-5"/><path d="M-3-7.5 0-4.5 3-7.5M-3 7.5 0 4.5 3 7.5"/></g><g transform="translate(200 58) scale(1)" style="fill:none;stroke:var(--info)" stroke-width="2.4" stroke-linecap="round" stroke-linejoin="round"><path d="M0-10V10M-8.7-5 8.7 5M-8.7 5 8.7-5"/><path d="M-3-7.5 0-4.5 3-7.5M-3 7.5 0 4.5 3 7.5"/></g><g transform="translate(252 30) scale(1.4)" style="fill:none;stroke:var(--info)" stroke-width="2.4" stroke-linecap="round" stroke-linejoin="round"><path d="M0-10V10M-8.7-5 8.7 5M-8.7 5 8.7-5"/><path d="M-3-7.5 0-4.5 3-7.5M-3 7.5 0 4.5 3 7.5"/></g><g transform="translate(96 76) scale(0.8)" style="fill:none;stroke:var(--info)" stroke-width="2.4" stroke-linecap="round" stroke-linejoin="round"><path d="M0-10V10M-8.7-5 8.7 5M-8.7 5 8.7-5"/><path d="M-3-7.5 0-4.5 3-7.5M-3 7.5 0 4.5 3 7.5"/></g><g transform="translate(284 80) scale(0.9)" style="fill:none;stroke:var(--info)" stroke-width="2.4" stroke-linecap="round" stroke-linejoin="round"><path d="M0-10V10M-8.7-5 8.7 5M-8.7 5 8.7-5"/><path d="M-3-7.5 0-4.5 3-7.5M-3 7.5 0 4.5 3 7.5"/></g><path d="M-20 150C50 92 130 96 200 150Z" style="fill:#FFFFFF" opacity=".92"/><path d="M130 150C190 100 270 104 340 150Z" style="fill:#FFFFFF" opacity=".75"/></svg>
        """,
        ["estrelas"] = """
        <svg viewBox="0 0 320 150" preserveAspectRatio="xMidYMid slice" aria-hidden="true"><rect width="320" height="150" style="fill:#0E7C66"/><circle cx="40" cy="60" r="1.8" style="fill:#FFFFFF" opacity=".7"/><circle cx="150" cy="46" r="1.8" style="fill:#FFFFFF" opacity=".7"/><circle cx="96" cy="22" r="1.8" style="fill:#FFFFFF" opacity=".7"/><circle cx="228" cy="96" r="1.8" style="fill:#FFFFFF" opacity=".7"/><circle cx="286" cy="70" r="1.8" style="fill:#FFFFFF" opacity=".7"/><circle cx="190" cy="18" r="1.8" style="fill:#FFFFFF" opacity=".7"/><circle cx="244" cy="46" r="22" style="fill:var(--sol)"/><circle cx="254" cy="39" r="19" style="fill:#0E7C66"/><path transform="translate(70 40) scale(1.2)" d="M0-8C1-2 2-1 8 0 2 1 1 2 0 8-1 2-2 1-8 0-2-1-1-2 0-8Z" style="fill:var(--sol)"/><path transform="translate(130 26) scale(0.8)" d="M0-8C1-2 2-1 8 0 2 1 1 2 0 8-1 2-2 1-8 0-2-1-1-2 0-8Z" style="fill:var(--sol)" opacity=".85"/><path transform="translate(178 60) scale(1)" d="M0-8C1-2 2-1 8 0 2 1 1 2 0 8-1 2-2 1-8 0-2-1-1-2 0-8Z" style="fill:var(--sol)"/><path transform="translate(104 76) scale(0.6)" d="M0-8C1-2 2-1 8 0 2 1 1 2 0 8-1 2-2 1-8 0-2-1-1-2 0-8Z" style="fill:var(--sol)" opacity=".7"/><path transform="translate(208 24) scale(0.6)" d="M0-8C1-2 2-1 8 0 2 1 1 2 0 8-1 2-2 1-8 0-2-1-1-2 0-8Z" style="fill:var(--sol)" opacity=".7"/><path d="M-20 150C60 104 150 110 220 150Z" style="fill:#0A5E4D"/><path d="M140 150C210 112 290 116 340 150Z" style="fill:#0B6B57"/></svg>
        """,
        ["abraco"] = """
        <svg viewBox="0 0 320 150" preserveAspectRatio="xMidYMid slice" aria-hidden="true"><rect width="320" height="150" style="fill:#0E7C66"/><circle cx="46" cy="120" r="40" style="fill:#FFFFFF" opacity=".07"/><circle cx="292" cy="22" r="32" style="fill:#FFFFFF" opacity=".07"/> <g transform="translate(93 6) scale(2.1)"><circle cx="17" cy="13" r="6" style="fill:#FFFFFF"/><circle cx="47" cy="13" r="6" style="fill:#F0643C"/><path d="M22.5 24C8 26 8 42 32 57" style="fill:none;stroke:#FFFFFF" stroke-width="6.5" stroke-linecap="round"/><path d="M41.5 24C56 26 56 42 32 57" style="fill:none;stroke:#F0643C" stroke-width="6.5" stroke-linecap="round"/><circle cx="32" cy="35" r="5.5" style="fill:#F9B935"/></g></svg>
        """,
        ["festa"] = """
        <svg viewBox="0 0 320 150" preserveAspectRatio="xMidYMid slice" aria-hidden="true"><rect width="320" height="150" style="fill:var(--sol-tinta)"/><path d="M0 10Q160 70 320 10" style="fill:none;stroke:var(--ink-2)" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round" opacity=".5"/><path d="M16.7 19.2L36.7 19.2L26.7 39.2Z" style="fill:var(--sol)"/><path d="M43.3 26.7L63.3 26.7L53.3 46.7Z" style="fill:var(--verde)"/><path d="M70.0 32.5L90.0 32.5L80.0 52.5Z" style="fill:var(--info)"/><path d="M96.7 36.7L116.7 36.7L106.7 56.7Z" style="fill:var(--coral)"/><path d="M123.3 39.2L143.3 39.2L133.3 59.2Z" style="fill:var(--sol)"/><path d="M150.0 40.0L170.0 40.0L160.0 60.0Z" style="fill:var(--verde)"/><path d="M176.7 39.2L196.7 39.2L186.7 59.2Z" style="fill:var(--info)"/><path d="M203.3 36.7L223.3 36.7L213.3 56.7Z" style="fill:var(--coral)"/><path d="M230.0 32.5L250.0 32.5L240.0 52.5Z" style="fill:var(--sol)"/><path d="M256.7 26.7L276.7 26.7L266.7 46.7Z" style="fill:var(--verde)"/><path d="M283.3 19.2L303.3 19.2L293.3 39.2Z" style="fill:var(--info)"/><path d="M0 50Q160 110 320 50" style="fill:none;stroke:var(--ink-2)" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round" opacity=".5"/><path d="M27.6 61.9L43.6 61.9L35.6 77.9Z" style="fill:var(--sol)"/><path d="M63.1 70.7L79.1 70.7L71.1 86.7Z" style="fill:var(--verde)"/><path d="M98.7 76.7L114.7 76.7L106.7 92.7Z" style="fill:var(--info)"/><path d="M134.2 79.6L150.2 79.6L142.2 95.6Z" style="fill:var(--coral)"/><path d="M169.8 79.6L185.8 79.6L177.8 95.6Z" style="fill:var(--sol)"/><path d="M205.3 76.7L221.3 76.7L213.3 92.7Z" style="fill:var(--verde)"/><path d="M240.9 70.7L256.9 70.7L248.9 86.7Z" style="fill:var(--info)"/><path d="M276.4 61.9L292.4 61.9L284.4 77.9Z" style="fill:var(--coral)"/><circle cx="40" cy="120" r="4" style="fill:var(--coral)"/><circle cx="96" cy="132" r="3" style="fill:var(--verde)"/><circle cx="160" cy="118" r="5" style="fill:var(--sol)"/><circle cx="214" cy="136" r="3.5" style="fill:var(--info)"/><circle cx="270" cy="122" r="4" style="fill:var(--coral)" opacity=".7"/><circle cx="300" cy="140" r="3" style="fill:var(--verde)"/></svg>
        """,
    };

    // Expressões do Solzinho (mascote), viewBox 160x140.
    public static readonly IReadOnlyDictionary<string, string> Sol = new Dictionary<string, string>
    {
        ["feliz"] = """
        <svg class="sol" viewBox="0 0 160 140" aria-hidden="true"><ellipse cx="80" cy="134" rx="24" ry="4" fill="currentColor" opacity=".08"/><g class="sol-rays"><line x1="44.5" y1="49.5" x2="35.8" y2="44.5" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="1.00"/><line x1="59.5" y1="34.5" x2="54.5" y2="25.8" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="1.00"/><line x1="80.0" y1="29.0" x2="80.0" y2="19.0" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="1.00"/><line x1="100.5" y1="34.5" x2="105.5" y2="25.8" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="1.00"/><line x1="115.5" y1="49.5" x2="124.2" y2="44.5" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="1.00"/></g><path d="M50 80Q36 90 40 104" fill="none" stroke="var(--logo-a)" stroke-width="6" stroke-linecap="round"/><path d="M110 80Q124 90 120 104" fill="none" stroke="var(--logo-b)" stroke-width="6" stroke-linecap="round"/><circle cx="80" cy="70" r="33" fill="#F9B935" stroke="#E09A1A" stroke-width="2"/><path d="M111.0 58.7A33 33 0 0 1 68.7 101.0Q96.5 84.8 111.0 58.7Z" fill="#E8862F" opacity=".22"/><g class="sol-eyes"><ellipse cx="68" cy="65" rx="3.6" ry="5" fill="#10241F"/><circle cx="69.2" cy="63.2" r="1.3" fill="#FFFFFF"/><ellipse cx="92" cy="65" rx="3.6" ry="5" fill="#10241F"/><circle cx="93.2" cy="63.2" r="1.3" fill="#FFFFFF"/></g><circle cx="60" cy="77" r="5.5" fill="#F0643C" opacity=".35"/><circle cx="100" cy="77" r="5.5" fill="#F0643C" opacity=".35"/><path d="M70 78Q80 88 90 78" fill="none" stroke="#10241F" stroke-width="3" stroke-linecap="round"/></svg>
        """,
        ["curioso"] = """
        <svg class="sol" viewBox="0 0 160 140" aria-hidden="true"><ellipse cx="80" cy="134" rx="24" ry="4" fill="currentColor" opacity=".08"/><g class="sol-rays"><line x1="44.5" y1="49.5" x2="35.8" y2="44.5" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="1.00"/><line x1="59.5" y1="34.5" x2="54.5" y2="25.8" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="1.00"/><line x1="80.0" y1="29.0" x2="80.0" y2="19.0" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="1.00"/><line x1="100.5" y1="34.5" x2="105.5" y2="25.8" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="1.00"/><line x1="115.5" y1="49.5" x2="124.2" y2="44.5" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="1.00"/></g><path d="M50 80Q36 90 40 104" fill="none" stroke="var(--logo-a)" stroke-width="6" stroke-linecap="round"/><circle cx="80" cy="70" r="33" fill="#F9B935" stroke="#E09A1A" stroke-width="2"/><path d="M111.0 58.7A33 33 0 0 1 68.7 101.0Q96.5 84.8 111.0 58.7Z" fill="#E8862F" opacity=".22"/><g class="sol-eyes"><ellipse cx="72" cy="65" rx="3.6" ry="5" fill="#10241F"/><circle cx="73.2" cy="63.2" r="1.3" fill="#FFFFFF"/><ellipse cx="96" cy="65" rx="3.6" ry="5" fill="#10241F"/><circle cx="97.2" cy="63.2" r="1.3" fill="#FFFFFF"/></g><circle cx="60" cy="77" r="5.5" fill="#F0643C" opacity=".35"/><circle cx="100" cy="77" r="5.5" fill="#F0643C" opacity=".35"/><ellipse cx="80" cy="82" rx="3.6" ry="4.4" fill="#10241F"/><path d="M110 78Q124 78 130 68" fill="none" stroke="var(--logo-b)" stroke-width="6" stroke-linecap="round"/><line x1="130" y1="68" x2="136" y2="60" stroke="#5B6F69" stroke-width="6" stroke-linecap="round"/><circle cx="144" cy="50" r="12" fill="#D4EAF7" stroke="#10241F" stroke-width="3"/><path d="M138 47q3-5 8-4" fill="none" stroke="#FFFFFF" stroke-width="2.4" stroke-linecap="round"/></svg>
        """,
        ["acenando"] = """
        <svg class="sol" viewBox="0 0 160 140" aria-hidden="true"><ellipse cx="80" cy="134" rx="24" ry="4" fill="currentColor" opacity=".08"/><g class="sol-motion" fill="none" stroke="var(--logo-b)" stroke-width="3.5" stroke-linecap="round"><path d="M130.8 39.4A11 11 0 0 1 138.6 52.8"/><path d="M132.9 31.6A19 19 0 0 1 146.4 54.9"/></g><g class="sol-rays"><line x1="44.5" y1="49.5" x2="35.8" y2="44.5" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="1.00"/><line x1="59.5" y1="34.5" x2="54.5" y2="25.8" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="1.00"/><line x1="80.0" y1="29.0" x2="80.0" y2="19.0" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="1.00"/><line x1="100.5" y1="34.5" x2="105.5" y2="25.8" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="1.00"/><line x1="115.5" y1="49.5" x2="124.2" y2="44.5" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="1.00"/></g><path d="M50 80Q36 90 40 104" fill="none" stroke="var(--logo-a)" stroke-width="6" stroke-linecap="round"/><path class="sol-arm" d="M110 74Q126 68 128 50" fill="none" stroke="var(--logo-b)" stroke-width="6" stroke-linecap="round"/><circle cx="80" cy="70" r="33" fill="#F9B935" stroke="#E09A1A" stroke-width="2"/><path d="M111.0 58.7A33 33 0 0 1 68.7 101.0Q96.5 84.8 111.0 58.7Z" fill="#E8862F" opacity=".22"/><g class="sol-eyes"><ellipse cx="68" cy="65" rx="3.6" ry="5" fill="#10241F"/><circle cx="69.2" cy="63.2" r="1.3" fill="#FFFFFF"/><ellipse cx="92" cy="65" rx="3.6" ry="5" fill="#10241F"/><circle cx="93.2" cy="63.2" r="1.3" fill="#FFFFFF"/></g><circle cx="60" cy="77" r="5.5" fill="#F0643C" opacity=".35"/><circle cx="100" cy="77" r="5.5" fill="#F0643C" opacity=".35"/><path d="M70 78Q80 88 90 78" fill="none" stroke="#10241F" stroke-width="3" stroke-linecap="round"/></svg>
        """,
        ["cadeado"] = """
        <svg class="sol" viewBox="0 0 160 140" aria-hidden="true"><ellipse cx="80" cy="134" rx="24" ry="4" fill="currentColor" opacity=".08"/><g class="sol-rays"><line x1="44.5" y1="43.5" x2="35.8" y2="38.5" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="1.00"/><line x1="59.5" y1="28.5" x2="54.5" y2="19.8" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="1.00"/><line x1="80.0" y1="23.0" x2="80.0" y2="13.0" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="1.00"/><line x1="100.5" y1="28.5" x2="105.5" y2="19.8" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="1.00"/><line x1="115.5" y1="43.5" x2="124.2" y2="38.5" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="1.00"/></g><path d="M50 74Q36 84 40 98" fill="none" stroke="var(--logo-a)" stroke-width="6" stroke-linecap="round"/><circle cx="80" cy="64" r="33" fill="#F9B935" stroke="#E09A1A" stroke-width="2"/><path d="M111.0 52.7A33 33 0 0 1 68.7 95.0Q96.5 78.8 111.0 52.7Z" fill="#E8862F" opacity=".22"/><g class="sol-eyes"><ellipse cx="68" cy="59" rx="3.6" ry="5" fill="#10241F"/><circle cx="69.2" cy="57.2" r="1.3" fill="#FFFFFF"/><ellipse cx="92" cy="59" rx="3.6" ry="5" fill="#10241F"/><circle cx="93.2" cy="57.2" r="1.3" fill="#FFFFFF"/></g><circle cx="60" cy="71" r="5.5" fill="#F0643C" opacity=".35"/><circle cx="100" cy="71" r="5.5" fill="#F0643C" opacity=".35"/><path d="M74 76H86" stroke="#10241F" stroke-width="3" stroke-linecap="round"/><path d="M110 74Q122 84 116 96" fill="none" stroke="var(--logo-b)" stroke-width="6" stroke-linecap="round"/><path d="M106 100V92a10 10 0 0 1 20 0V100" fill="none" stroke="#5B6F69" stroke-width="4.5"/><rect x="100" y="98" width="32" height="24" rx="5" fill="#0E7C66"/><circle cx="116" cy="108" r="3" fill="#F9B935"/><rect x="115" y="109" width="2" height="6" rx="1" fill="#F9B935"/></svg>
        """,
        ["carta"] = """
        <svg class="sol" viewBox="0 0 160 140" aria-hidden="true"><ellipse cx="80" cy="134" rx="24" ry="4" fill="currentColor" opacity=".08"/><g class="sol-rays"><line x1="44.5" y1="41.5" x2="35.8" y2="36.5" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="1.00"/><line x1="59.5" y1="26.5" x2="54.5" y2="17.8" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="1.00"/><line x1="80.0" y1="21.0" x2="80.0" y2="11.0" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="1.00"/><line x1="100.5" y1="26.5" x2="105.5" y2="17.8" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="1.00"/><line x1="115.5" y1="41.5" x2="124.2" y2="36.5" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="1.00"/></g><path d="M50 72Q36 82 40 96" fill="none" stroke="var(--logo-a)" stroke-width="6" stroke-linecap="round"/><circle cx="80" cy="62" r="33" fill="#F9B935" stroke="#E09A1A" stroke-width="2"/><path d="M111.0 50.7A33 33 0 0 1 68.7 93.0Q96.5 76.8 111.0 50.7Z" fill="#E8862F" opacity=".22"/><g class="sol-eyes"><ellipse cx="68" cy="57" rx="3.6" ry="5" fill="#10241F"/><circle cx="69.2" cy="55.2" r="1.3" fill="#FFFFFF"/><ellipse cx="92" cy="57" rx="3.6" ry="5" fill="#10241F"/><circle cx="93.2" cy="55.2" r="1.3" fill="#FFFFFF"/></g><circle cx="60" cy="69" r="5.5" fill="#F0643C" opacity=".35"/><circle cx="100" cy="69" r="5.5" fill="#F0643C" opacity=".35"/><path d="M70 70Q80 80 90 70" fill="none" stroke="#10241F" stroke-width="3" stroke-linecap="round"/><rect x="100" y="90" width="44" height="30" rx="4" fill="#FFFFFF" stroke="#AEBBB6" stroke-width="2"/><polyline points="100,92 122,108 144,92" fill="none" stroke="#AEBBB6" stroke-width="2" stroke-linejoin="round"/><path d="M122 112c-5-3-6-6-4-8 1-1 3-1 4 1 1-2 3-2 4-1 2 2 1 5-4 8z" fill="#F0643C"/><path d="M110 72Q124 78 116 92" fill="none" stroke="var(--logo-b)" stroke-width="6" stroke-linecap="round"/></svg>
        """,
        ["comemorando"] = """
        <svg class="sol" viewBox="0 0 160 140" aria-hidden="true"><ellipse cx="80" cy="134" rx="24" ry="4" fill="currentColor" opacity=".08"/><circle cx="18" cy="34" r="3.5" fill="#B7DFA4"/><circle cx="146" cy="26" r="3.5" fill="#9CCDE8"/><circle cx="20" cy="104" r="3" fill="#F0643C"/><circle cx="144" cy="100" r="3" fill="#0E7C66"/><path d="M32 16l5 4M124 44l7-2M12 66l-5 3M150 62l5 3" stroke="#F9B935" stroke-width="3" stroke-linecap="round"/><g class="sol-rays"><line x1="121.0" y1="70.0" x2="131.0" y2="70.0" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="1.00"/><line x1="109.0" y1="99.0" x2="116.1" y2="106.1" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="1.00"/><line x1="80.0" y1="111.0" x2="80.0" y2="121.0" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="1.00"/><line x1="51.0" y1="99.0" x2="43.9" y2="106.1" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="1.00"/><line x1="39.0" y1="70.0" x2="29.0" y2="70.0" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="1.00"/><line x1="51.0" y1="41.0" x2="43.9" y2="33.9" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="1.00"/><line x1="80.0" y1="29.0" x2="80.0" y2="19.0" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="1.00"/><line x1="109.0" y1="41.0" x2="116.1" y2="33.9" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="1.00"/></g><path d="M50 72Q34 64 32 46" fill="none" stroke="var(--logo-a)" stroke-width="6" stroke-linecap="round"/><path d="M110 72Q126 64 128 46" fill="none" stroke="var(--logo-b)" stroke-width="6" stroke-linecap="round"/><circle cx="80" cy="70" r="33" fill="#F9B935" stroke="#E09A1A" stroke-width="2"/><path d="M111.0 58.7A33 33 0 0 1 68.7 101.0Q96.5 84.8 111.0 58.7Z" fill="#E8862F" opacity=".22"/><path d="M63 67Q68 60 73 67" fill="none" stroke="#10241F" stroke-width="3" stroke-linecap="round"/><path d="M87 67Q92 60 97 67" fill="none" stroke="#10241F" stroke-width="3" stroke-linecap="round"/><circle cx="60" cy="77" r="5.5" fill="#F0643C" opacity=".35"/><circle cx="100" cy="77" r="5.5" fill="#F0643C" opacity=".35"/><path d="M68 76Q80 96 92 76Z" fill="#10241F"/><ellipse cx="80" cy="85" rx="5" ry="3" fill="#F0643C"/></svg>
        """,
        ["dormindo"] = """
        <svg class="sol" viewBox="0 0 160 140" aria-hidden="true"><ellipse cx="80" cy="134" rx="24" ry="4" fill="currentColor" opacity=".08"/><g fill="none" stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" opacity=".45"><polyline points="114,44 125,44 114,55 125,55" stroke-width="2.6"/><polyline points="130,28 138,28 130,36 138,36" stroke-width="2.3"/><polyline points="142,16 148,16 142,22 148,22" stroke-width="2"/></g><g class="sol-rays"><line x1="44.5" y1="49.5" x2="35.8" y2="44.5" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="0.45"/><line x1="59.5" y1="34.5" x2="54.5" y2="25.8" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="0.45"/><line x1="80.0" y1="29.0" x2="80.0" y2="19.0" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="0.45"/><line x1="100.5" y1="34.5" x2="105.5" y2="25.8" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="0.45"/><line x1="115.5" y1="49.5" x2="124.2" y2="44.5" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="0.45"/></g><circle cx="80" cy="70" r="33" fill="#F9B935" stroke="#E09A1A" stroke-width="2"/><path d="M111.0 58.7A33 33 0 0 1 68.7 101.0Q96.5 84.8 111.0 58.7Z" fill="#E8862F" opacity=".22"/><path d="M63 64Q68 69 73 64" fill="none" stroke="#10241F" stroke-width="3" stroke-linecap="round"/><path d="M87 64Q92 69 97 64" fill="none" stroke="#10241F" stroke-width="3" stroke-linecap="round"/><circle cx="60" cy="77" r="5.5" fill="#F0643C" opacity=".35"/><circle cx="100" cy="77" r="5.5" fill="#F0643C" opacity=".35"/><ellipse cx="80" cy="82" rx="2.6" ry="2" fill="#10241F"/></svg>
        """,
        ["espiando"] = """
        <svg class="sol" viewBox="0 0 160 140" aria-hidden="true"><ellipse cx="80" cy="134" rx="24" ry="4" fill="currentColor" opacity=".08"/><g class="sol-rays"><line x1="44.5" y1="37.5" x2="35.8" y2="32.5" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="1.00"/><line x1="59.5" y1="22.5" x2="54.5" y2="13.8" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="1.00"/><line x1="80.0" y1="17.0" x2="80.0" y2="7.0" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="1.00"/><line x1="100.5" y1="22.5" x2="105.5" y2="13.8" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="1.00"/><line x1="115.5" y1="37.5" x2="124.2" y2="32.5" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="1.00"/></g><circle cx="80" cy="58" r="33" fill="#F9B935" stroke="#E09A1A" stroke-width="2"/><path d="M111.0 46.7A33 33 0 0 1 68.7 89.0Q96.5 72.8 111.0 46.7Z" fill="#E8862F" opacity=".22"/><g class="sol-eyes"><ellipse cx="68" cy="56" rx="3.6" ry="5" fill="#10241F"/><circle cx="69.2" cy="54.2" r="1.3" fill="#FFFFFF"/><ellipse cx="92" cy="56" rx="3.6" ry="5" fill="#10241F"/><circle cx="93.2" cy="54.2" r="1.3" fill="#FFFFFF"/></g><circle cx="60" cy="65" r="5.5" fill="#F0643C" opacity=".35"/><circle cx="100" cy="65" r="5.5" fill="#F0643C" opacity=".35"/><ellipse cx="80" cy="70" rx="3.6" ry="4.4" fill="#10241F"/><polygon points="40,84 120,84 128,72 32,72" fill="#C98A55"/><polygon points="32,72 40,84 22,94 14,80" fill="#F6BD78" stroke="#C98A55" stroke-width="2" stroke-linejoin="round"/><polygon points="128,72 120,84 138,94 146,80" fill="#F6BD78" stroke="#C98A55" stroke-width="2" stroke-linejoin="round"/><rect x="40" y="84" width="80" height="42" rx="3" fill="#F6BD78" stroke="#C98A55" stroke-width="2"/><circle cx="58" cy="84" r="6" fill="var(--logo-a)"/><circle cx="102" cy="84" r="6" fill="var(--logo-b)"/></svg>
        """,
        ["pensando"] = """
        <svg class="sol" viewBox="0 0 160 140" aria-hidden="true"><ellipse cx="80" cy="134" rx="24" ry="4" fill="currentColor" opacity=".08"/><g class="sol-rays"><line x1="121.0" y1="70.0" x2="131.0" y2="70.0" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="1.00"/><line x1="109.0" y1="99.0" x2="116.1" y2="106.1" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="0.89"/><line x1="80.0" y1="111.0" x2="80.0" y2="121.0" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="0.78"/><line x1="51.0" y1="99.0" x2="43.9" y2="106.1" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="0.67"/><line x1="39.0" y1="70.0" x2="29.0" y2="70.0" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="0.56"/><line x1="51.0" y1="41.0" x2="43.9" y2="33.9" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="0.45"/><line x1="80.0" y1="29.0" x2="80.0" y2="19.0" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="0.34"/><line x1="109.0" y1="41.0" x2="116.1" y2="33.9" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="0.23"/></g><path d="M50 80Q36 90 40 104" fill="none" stroke="var(--logo-a)" stroke-width="6" stroke-linecap="round"/><path d="M110 80Q124 90 120 104" fill="none" stroke="var(--logo-b)" stroke-width="6" stroke-linecap="round"/><circle cx="80" cy="70" r="33" fill="#F9B935" stroke="#E09A1A" stroke-width="2"/><path d="M111.0 58.7A33 33 0 0 1 68.7 101.0Q96.5 84.8 111.0 58.7Z" fill="#E8862F" opacity=".22"/><g class="sol-eyes"><ellipse cx="71" cy="62" rx="3.6" ry="5" fill="#10241F"/><circle cx="72.2" cy="60.2" r="1.3" fill="#FFFFFF"/><ellipse cx="95" cy="62" rx="3.6" ry="5" fill="#10241F"/><circle cx="96.2" cy="60.2" r="1.3" fill="#FFFFFF"/></g><circle cx="60" cy="77" r="5.5" fill="#F0643C" opacity=".35"/><circle cx="100" cy="77" r="5.5" fill="#F0643C" opacity=".35"/><path d="M76 81Q83 85 89 79" fill="none" stroke="#10241F" stroke-width="3" stroke-linecap="round"/></svg>
        """,
        ["piscando"] = """
        <svg class="sol" viewBox="0 0 160 140" aria-hidden="true"><ellipse cx="80" cy="134" rx="24" ry="4" fill="currentColor" opacity=".08"/><g class="sol-rays"><line x1="44.5" y1="49.5" x2="35.8" y2="44.5" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="1.00"/><line x1="59.5" y1="34.5" x2="54.5" y2="25.8" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="1.00"/><line x1="80.0" y1="29.0" x2="80.0" y2="19.0" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="1.00"/><line x1="100.5" y1="34.5" x2="105.5" y2="25.8" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="1.00"/><line x1="115.5" y1="49.5" x2="124.2" y2="44.5" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="1.00"/></g><path d="M50 80Q36 90 40 104" fill="none" stroke="var(--logo-a)" stroke-width="6" stroke-linecap="round"/><path d="M110 80Q124 90 120 104" fill="none" stroke="var(--logo-b)" stroke-width="6" stroke-linecap="round"/><circle cx="80" cy="70" r="33" fill="#F9B935" stroke="#E09A1A" stroke-width="2"/><path d="M111.0 58.7A33 33 0 0 1 68.7 101.0Q96.5 84.8 111.0 58.7Z" fill="#E8862F" opacity=".22"/><g class="sol-eyes"><ellipse cx="68" cy="65" rx="3.6" ry="5" fill="#10241F"/><circle cx="69.2" cy="63.2" r="1.3" fill="#FFFFFF"/></g><path d="M87 67Q92 60 97 67" fill="none" stroke="#10241F" stroke-width="3" stroke-linecap="round"/><circle cx="60" cy="77" r="5.5" fill="#F0643C" opacity=".35"/><circle cx="100" cy="77" r="5.5" fill="#F0643C" opacity=".35"/><path d="M70 78Q80 88 90 78" fill="none" stroke="#10241F" stroke-width="3" stroke-linecap="round"/></svg>
        """,
        ["preocupado"] = """
        <svg class="sol" viewBox="0 0 160 140" aria-hidden="true"><ellipse cx="80" cy="134" rx="24" ry="4" fill="currentColor" opacity=".08"/><g class="sol-rays"><line x1="44.5" y1="35.5" x2="35.8" y2="30.5" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="0.60"/><line x1="59.5" y1="20.5" x2="54.5" y2="11.8" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="0.60"/><line x1="80.0" y1="15.0" x2="80.0" y2="5.0" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="0.60"/><line x1="100.5" y1="20.5" x2="105.5" y2="11.8" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="0.60"/><line x1="115.5" y1="35.5" x2="124.2" y2="30.5" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="0.60"/></g><circle cx="80" cy="56" r="33" fill="#F9B935" stroke="#E09A1A" stroke-width="2"/><path d="M111.0 44.7A33 33 0 0 1 68.7 87.0Q96.5 70.8 111.0 44.7Z" fill="#E8862F" opacity=".22"/><path d="M61 43L72 39M99 43L88 39" stroke="#10241F" stroke-width="2.6" stroke-linecap="round"/><g class="sol-eyes"><ellipse cx="68" cy="52" rx="3.6" ry="5" fill="#10241F"/><circle cx="69.2" cy="50.2" r="1.3" fill="#FFFFFF"/><ellipse cx="92" cy="52" rx="3.6" ry="5" fill="#10241F"/><circle cx="93.2" cy="50.2" r="1.3" fill="#FFFFFF"/></g><path d="M70 68Q75 63 80 68Q85 73 90 68" fill="none" stroke="#10241F" stroke-width="2.6" stroke-linecap="round"/><circle cx="50" cy="102" r="17" fill="#DCE3E0"/><circle cx="75" cy="92" r="21" fill="#DCE3E0"/><circle cx="102" cy="96" r="18" fill="#DCE3E0"/><circle cx="122" cy="105" r="13" fill="#DCE3E0"/><rect x="36" y="102" width="98" height="18" rx="9" fill="#DCE3E0"/><path d="M45 120Q34 120 34 110A17 17 0 0 1 56 86A21 21 0 0 1 93 82A18 18 0 0 1 119.7 92.9A13 13 0 0 1 134 110Q134 120 125 120Z" fill="none" stroke="#AEBBB6" stroke-width="2"/><path d="M58 126l-3 7M80 126l-3 7M102 126l-3 7" stroke="#2F67B5" stroke-width="3" stroke-linecap="round"/></svg>
        """,
        ["radiante"] = """
        <svg class="sol" viewBox="0 0 160 140" aria-hidden="true"><ellipse cx="80" cy="134" rx="24" ry="4" fill="currentColor" opacity=".08"/><g class="sol-rays"><line x1="121.0" y1="70.0" x2="131.0" y2="70.0" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="1.00"/><line x1="109.0" y1="99.0" x2="116.1" y2="106.1" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="1.00"/><line x1="80.0" y1="111.0" x2="80.0" y2="121.0" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="1.00"/><line x1="51.0" y1="99.0" x2="43.9" y2="106.1" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="1.00"/><line x1="39.0" y1="70.0" x2="29.0" y2="70.0" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="1.00"/><line x1="51.0" y1="41.0" x2="43.9" y2="33.9" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="1.00"/><line x1="80.0" y1="29.0" x2="80.0" y2="19.0" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="1.00"/><line x1="109.0" y1="41.0" x2="116.1" y2="33.9" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="1.00"/></g><path d="M50 80Q36 90 40 104" fill="none" stroke="var(--logo-a)" stroke-width="6" stroke-linecap="round"/><path d="M110 80Q124 90 120 104" fill="none" stroke="var(--logo-b)" stroke-width="6" stroke-linecap="round"/><circle cx="80" cy="70" r="33" fill="#F9B935" stroke="#E09A1A" stroke-width="2"/><path d="M111.0 58.7A33 33 0 0 1 68.7 101.0Q96.5 84.8 111.0 58.7Z" fill="#E8862F" opacity=".22"/><path d="M63 67Q68 60 73 67" fill="none" stroke="#10241F" stroke-width="3" stroke-linecap="round"/><path d="M87 67Q92 60 97 67" fill="none" stroke="#10241F" stroke-width="3" stroke-linecap="round"/><circle cx="60" cy="77" r="5.5" fill="#F0643C" opacity=".35"/><circle cx="100" cy="77" r="5.5" fill="#F0643C" opacity=".35"/><path d="M68 76Q80 96 92 76Z" fill="#10241F"/><ellipse cx="80" cy="85" rx="5" ry="3" fill="#F0643C"/></svg>
        """,
        ["abraco"] = """
        <svg class="sol" viewBox="0 0 160 140" aria-hidden="true"><ellipse cx="80" cy="134" rx="24" ry="4" fill="currentColor" opacity=".08"/><circle cx="18" cy="34" r="3.5" fill="#B7DFA4"/><circle cx="146" cy="26" r="3.5" fill="#9CCDE8"/><circle cx="20" cy="104" r="3" fill="#F0643C"/><circle cx="144" cy="100" r="3" fill="#0E7C66"/><path d="M32 16l5 4M124 44l7-2M12 66l-5 3M150 62l5 3" stroke="#F9B935" stroke-width="3" stroke-linecap="round"/><g class="sol-rays"><line x1="121.0" y1="62.0" x2="131.0" y2="62.0" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="1.00"/><line x1="109.0" y1="91.0" x2="116.1" y2="98.1" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="1.00"/><line x1="80.0" y1="103.0" x2="80.0" y2="113.0" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="1.00"/><line x1="51.0" y1="91.0" x2="43.9" y2="98.1" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="1.00"/><line x1="39.0" y1="62.0" x2="29.0" y2="62.0" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="1.00"/><line x1="51.0" y1="33.0" x2="43.9" y2="25.9" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="1.00"/><line x1="80.0" y1="21.0" x2="80.0" y2="11.0" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="1.00"/><line x1="109.0" y1="33.0" x2="116.1" y2="25.9" stroke="#F9B935" stroke-width="6" stroke-linecap="round" opacity="1.00"/></g><circle cx="80" cy="62" r="33" fill="#F9B935" stroke="#E09A1A" stroke-width="2"/><path d="M111.0 50.7A33 33 0 0 1 68.7 93.0Q96.5 76.8 111.0 50.7Z" fill="#E8862F" opacity=".22"/><path d="M63 59Q68 52 73 59" fill="none" stroke="#10241F" stroke-width="3" stroke-linecap="round"/><path d="M87 59Q92 52 97 59" fill="none" stroke="#10241F" stroke-width="3" stroke-linecap="round"/><circle cx="60" cy="69" r="5.5" fill="#F0643C" opacity=".35"/><circle cx="100" cy="69" r="5.5" fill="#F0643C" opacity=".35"/><path d="M68 68Q80 88 92 68Z" fill="#10241F"/><ellipse cx="80" cy="77" rx="5" ry="3" fill="#F0643C"/><path d="M80 126C58 112 52 100 58 92C64 84 74 86 80 94C86 86 96 84 102 92C108 100 102 112 80 126Z" fill="#F0643C" stroke="#C94A26" stroke-width="2"/><path d="M50 76Q46 98 62 106" fill="none" stroke="var(--logo-a)" stroke-width="6" stroke-linecap="round"/><path d="M110 76Q114 98 98 106" fill="none" stroke="var(--logo-b)" stroke-width="6" stroke-linecap="round"/></svg>
        """,
    };

    // Conteúdo interno (paths) dos ícones de 24x24 com traço.
    public static readonly IReadOnlyDictionary<string, string> Icones = new Dictionary<string, string>
    {
        ["home"] = """
        <path d="M3 10.5 12 3l9 7.5"/><path d="M5 9.5V21h14V9.5"/>
        """,
        ["flag"] = """
        <path d="M5 21V4"/><path d="M5 4h12l-2.5 4.5L17 13H5"/>
        """,
        ["users"] = """
        <circle cx="9" cy="8" r="3.5"/><path d="M2.5 20c0-3.6 2.9-6 6.5-6s6.5 2.4 6.5 6"/><path d="M16 4.6a3.5 3.5 0 0 1 0 6.8"/><path d="M18.5 14.3c1.8.8 3 2.8 3 5.7"/>
        """,
        ["heart"] = """
        <path d="M12 20s-7.5-4.6-9.2-9.4C1.6 7 3.9 4 7.1 4c2 0 3.6 1.1 4.9 2.9C13.3 5.1 14.9 4 16.9 4c3.2 0 5.5 3 4.3 6.6C19.5 15.4 12 20 12 20z"/>
        """,
        ["layers"] = """
        <path d="M12 3 2 8l10 5 10-5-10-5z"/><path d="m2 13 10 5 10-5"/><path d="m2 18 10 5 10-5" opacity=".0"/>
        """,
        ["activity"] = """
        <path d="M3 12h4l3-8 4 16 3-8h4"/>
        """,
        ["moeda"] = """
        <circle cx="12" cy="12" r="9"/><path d="M14.6 9.4c-.5-.9-1.5-1.4-2.6-1.4-1.5 0-2.6.8-2.6 2s1.1 1.7 2.6 2 2.6.8 2.6 2-1.1 2-2.6 2c-1.2 0-2.2-.6-2.7-1.5"/><path d="M12 6v2M12 16v2"/>
        """,
        ["recibo"] = """
        <path d="M6 3h12v18l-3-2-3 2-3-2-3 2z"/><path d="M9 8h6M9 12h6M9 16h3"/>
        """,
        ["instalar"] = """
        <path d="M12 4v11"/><path d="M7 10l5 5 5-5"/><path d="M5 20h14"/>
        """,
        ["logout"] = """
        <path d="M15 4h3a2 2 0 0 1 2 2v12a2 2 0 0 1-2 2h-3"/><path d="M10 17l-5-5 5-5"/><path d="M5 12h11"/>
        """,
        ["plus"] = """
        <path d="M12 5v14M5 12h14"/>
        """,
        ["edit"] = """
        <path d="M4 20h4L19 9l-4-4L4 16v4z"/><path d="m13.5 6.5 4 4"/>
        """,
        ["search"] = """
        <circle cx="11" cy="11" r="7"/><path d="m20 20-3.5-3.5"/>
        """,
        ["check"] = """
        <path d="m5 12.5 4.5 4.5L19 7.5"/>
        """,
        ["x"] = """
        <path d="M6 6l12 12M18 6 6 18"/>
        """,
        ["clock"] = """
        <circle cx="12" cy="12" r="9"/><path d="M12 7v5l3 2"/>
        """,
        ["right"] = """
        <path d="M5 12h14M13 6l6 6-6 6"/>
        """,
        ["left"] = """
        <path d="M19 12H5M11 6l-6 6 6 6"/>
        """,
        ["shield"] = """
        <path d="M12 3 4 6v6c0 4.5 3.4 8 8 9 4.6-1 8-4.5 8-9V6l-8-3z"/><path d="m9 12 2 2 4-4"/>
        """,
        ["code"] = """
        <path d="m8 8-5 4 5 4M16 8l5 4-5 4"/>
        """,
        ["map"] = """
        <path d="M9 4 3 6v14l6-2 6 2 6-2V4l-6 2-6-2z"/><path d="M9 4v14M15 6v14"/>
        """,
        ["eye"] = """
        <path d="M2 12s3.6-7 10-7 10 7 10 7-3.6 7-10 7S2 12 2 12z"/><circle cx="12" cy="12" r="3"/>
        """,
        ["eyeoff"] = """
        <path d="M3 3l18 18"/><path d="M10.6 5.1A10.6 10.6 0 0 1 12 5c6.4 0 10 7 10 7a17 17 0 0 1-3.2 4"/><path d="M6.6 6.6A17.4 17.4 0 0 0 2 12s3.6 7 10 7a9.7 9.7 0 0 0 5.4-1.6"/><path d="M9.9 9.9a3 3 0 0 0 4.2 4.2"/>
        """,
        ["lock"] = """
        <rect x="4" y="11" width="16" height="10" rx="2"/><path d="M8 11V8a4 4 0 0 1 8 0v3"/>
        """,
        ["mail"] = """
        <rect x="3" y="5" width="18" height="14" rx="2"/><path d="m3 7 9 6 9-6"/>
        """,
        ["user"] = """
        <circle cx="12" cy="8" r="4"/><path d="M4 21c0-4 3.6-7 8-7s8 3 8 7"/>
        """,
        ["idcard"] = """
        <rect x="3" y="5" width="18" height="14" rx="2"/><circle cx="9" cy="11" r="2"/><path d="M6 16c.6-1.5 1.7-2.2 3-2.2s2.4.7 3 2.2M15 10h3M15 13h3"/>
        """,
        ["calendar"] = """
        <rect x="3" y="5" width="18" height="16" rx="2"/><path d="M3 10h18M8 3v4M16 3v4"/>
        """,
        ["target"] = """
        <circle cx="12" cy="12" r="9"/><circle cx="12" cy="12" r="5"/><circle cx="12" cy="12" r="1"/>
        """,
        ["pause"] = """
        <path d="M9 5v14M15 5v14"/>
        """,
        ["play"] = """
        <path d="M7 4.5v15l12-7.5z"/>
        """,
        ["zap"] = """
        <path d="M13 2 4 14h7l-1 8 9-12h-7l1-8z"/>
        """,
        ["refresh"] = """
        <path d="M20 11a8 8 0 1 0-2.3 5.7"/><path d="M20 4v7h-7"/>
        """,
        ["server"] = """
        <rect x="3" y="4" width="18" height="7" rx="1.5"/><rect x="3" y="13" width="18" height="7" rx="1.5"/><path d="M7 7.5h.01M7 16.5h.01"/>
        """,
        ["menu"] = """
        <path d="M4 7h16M4 12h16M4 17h16"/>
        """,
        ["info"] = """
        <circle cx="12" cy="12" r="9"/><path d="M12 11v5M12 8h.01"/>
        """,
        ["alert"] = """
        <path d="M12 3 2 20h20L12 3z"/><path d="M12 10v4M12 17h.01"/>
        """,
        ["git"] = """
        <circle cx="6" cy="5" r="2.2"/><circle cx="6" cy="19" r="2.2"/><circle cx="18" cy="8" r="2.2"/><path d="M6 7.2v9.6M18 10.2c0 4-5 4-11.2 7"/>
        """,
        ["globe"] = """
        <circle cx="12" cy="12" r="9"/><path d="M3 12h18M12 3c2.5 2.7 3.8 5.7 3.8 9s-1.3 6.3-3.8 9c-2.5-2.7-3.8-5.7-3.8-9S9.5 5.7 12 3z"/>
        """,
        ["gift"] = """
        <rect x="3" y="8" width="18" height="5" rx="1"/><path d="M5 13v8h14v-8M12 8v13"/><path d="M12 8c-1.5-3-5-3.5-5-1.2C7 8 9 8 12 8zm0 0c1.5-3 5-3.5 5-1.2C17 8 15 8 12 8z"/>
        """,
        ["trend"] = """
        <path d="m3 17 6-6 4 4 8-8"/><path d="M15 7h6v6"/>
        """,
        ["copy"] = """
        <rect x="9" y="9" width="12" height="12" rx="2"/><path d="M5 15V5a2 2 0 0 1 2-2h10"/>
        """,
        ["sun"] = """
        <circle cx="12" cy="12" r="4"/><path d="M12 2v2M12 20v2M4.9 4.9l1.4 1.4M17.7 17.7l1.4 1.4M2 12h2M20 12h2M4.9 19.1l1.4-1.4M17.7 6.3l1.4-1.4"/>
        """,
        ["moon"] = """
        <path d="M20 14.5A8 8 0 0 1 9.5 4a8 8 0 1 0 10.5 10.5z"/>
        """,
    };
}
