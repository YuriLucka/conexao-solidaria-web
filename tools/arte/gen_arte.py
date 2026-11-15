# Gera Marca/Arte.cs (web) a partir de arte.json extraído do protótipo.
import json, re, sys

W = sys.argv[1]
a = json.load(open(r'C:/Users/Yuri/AppData/Local/Temp/claude/C--Users-Yuri/0ec0aa94-89e7-454f-9e76-933060c7c5b7/scratchpad/arte/arte.json', encoding='utf-8'))

def raw(s):
    s = ' '.join(s.split())
    return '"""\n        ' + s + '\n        """'

def sem_tamanho(svg):
    # Tamanho fica por conta do componente (atributo width/height injetado na renderização).
    return re.sub(r'^<svg([^>]*?) width="\d+" height="\d+"', r'<svg\1', svg, count=1)

L = ["// Gerado a partir do protótipo do grupo (Conexão Solidária.html): mesmos desenhos, cores pelas variáveis CSS do tema.",
     "// Não editar à mão; regerar com os scripts extrair-arte.mjs e gen_arte.py se o protótipo mudar.",
     "namespace ConexaoSolidaria.Web.Marca;", "", "public static class Arte", "{"]
L.append("    public const string Simbolo = " + raw(sem_tamanho(a['symbol'])) + ";\n")
L.append("    // Capa usada quando a campanha não escolheu uma: a da categoria, ou \"sol-colinas\".")
L.append("    public static readonly IReadOnlyDictionary<string, string> CapaPorCategoria = new Dictionary<string, string>\n    {")
for k, v in a['capaPorCategoria'].items():
    L.append(f'        ["{k}"] = "{v}",')
L.append("    };\n")
L.append("    public static readonly IReadOnlyList<(string Id, string Nome)> Capas =\n    [")
for k, v in a['capas'].items():
    L.append(f'        ("{k}", "{v["nome"]}"),')
L.append("    ];\n")
L.append("    public static readonly IReadOnlyDictionary<string, string> CapaSvg = new Dictionary<string, string>\n    {")
for k, v in a['capas'].items():
    L.append(f'        ["{k}"] = ' + raw(v['svg']) + ',')
L.append("    };\n")
L.append("    // Expressões do Solzinho (mascote), viewBox 160x140.")
L.append("    public static readonly IReadOnlyDictionary<string, string> Sol = new Dictionary<string, string>\n    {")
for k, v in a['sol'].items():
    L.append(f'        ["{k}"] = ' + raw(sem_tamanho(v)) + ',')
L.append("    };\n")
L.append("    // Conteúdo interno (paths) dos ícones de 24x24 com traço.")
L.append("    public static readonly IReadOnlyDictionary<string, string> Icones = new Dictionary<string, string>\n    {")
for k, v in a['icones'].items():
    L.append(f'        ["{k}"] = ' + raw(v) + ',')
L.append("    };")
L.append("}")
open(f'{W}/Marca/Arte.cs', 'w', encoding='utf-8').write("\n".join(L) + "\n")
print('ok')
