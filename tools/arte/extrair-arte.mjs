// Extrai do protótipo do grupo (proto.js) os desenhos em SVG: símbolo do logo, 12 capas, expressões do mascote e ícones.
// Roda só as funções de desenho (linhas 90-253 e 549-623), sem DOM.
import { readFileSync, writeFileSync, mkdirSync } from 'node:fs';
import vm from 'node:vm';

const base = 'C:/Users/Yuri/AppData/Local/Temp/claude/C--Users-Yuri/0ec0aa94-89e7-454f-9e76-933060c7c5b7/scratchpad';
const linhas = readFileSync(`${base}/hk/proto.js`, 'utf8').split('\n');
const trecho = (a, b) => linhas.slice(a - 1, b).join('\n');

const codigo = [trecho(90, 253), "const tema = () => 'claro';", trecho(556, 623),
  `globalThis.__arte = {
     symbol: symbol(64),
     capas: Object.fromEntries(CAPAS.map(k => [k.id, {nome: k.nome, svg: capaSVG(k)}])),
     capaPorCategoria: CAPA_POR_CATEGORIA,
     icones: ICONS,
     sol: Object.fromEntries(['feliz','curioso','acenando','cadeado','carta','comemorando','dormindo','espiando','pensando','piscando','preocupado','radiante','abraco']
       .map(e => [e, sol(e, {size: 160})]))
   };`].join('\n');

const ctx = { console };
vm.createContext(ctx);
vm.runInContext(codigo, ctx);
const arte = ctx.__arte;

const out = `${base}/arte`;
mkdirSync(out, { recursive: true });
writeFileSync(`${out}/arte.json`, JSON.stringify(arte, null, 2));
console.log('capas', Object.keys(arte.capas).length, 'sol', Object.keys(arte.sol).length, 'icones', Object.keys(arte.icones).length);
