# Arte do site

`src/ConexaoSolidaria.Web/Marca/Arte.cs` (logo, 12 capas, expressões do Solzinho e ícones) e `wwwroot/css/prototipo.css` vêm do protótipo do grupo (`Conexão Solidária.html`). Para regerar depois de mudar o protótipo:

1. extrair o JavaScript do protótipo para `proto.js` e ajustar os caminhos no topo dos dois scripts;
2. `node extrair-arte.mjs` (gera `arte.json` rodando só as funções de desenho);
3. `python gen_arte.py <pasta do projeto web>` (gera `Marca/Arte.cs`).
