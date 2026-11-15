// Revisão visual e responsiva: abre cada tela em vários aparelhos, tira captura e acusa problemas.
// Uso: node capturas.mjs [baseUrl] [rotas separadas por vírgula]
//   baseUrl padrão: http://localhost:5173 (dotnet run) — atrás do Kong use http://localhost:8000
// Saída: e2e/capturas/<aparelho>-<tela>.png e um resumo no terminal (sai com código 1 se achar problema).
import { chromium, webkit, devices } from 'playwright';
import { mkdirSync } from 'node:fs';

const base = (process.argv[2] || 'http://localhost:5173').replace(/\/$/, '');
const filtro = process.argv[3]?.split(',');
const pasta = new URL('./capturas/', import.meta.url).pathname.replace(/^\/([A-Z]:)/, '$1');
mkdirSync(pasta, { recursive: true });

const aparelhos = [
  { nome: 'desktop', motor: chromium, opcoes: { viewport: { width: 1440, height: 900 } } },
  { nome: 'notebook', motor: chromium, opcoes: { viewport: { width: 1024, height: 768 } } },
  { nome: 'ipad', motor: webkit, opcoes: devices['iPad (gen 7)'] },
  { nome: 'iphone15', motor: webkit, opcoes: devices['iPhone 15'] },
  { nome: 'iphonese', motor: webkit, opcoes: devices['iPhone SE'] },
];

// perfil: null (anônimo), 'doador' ou 'gestor'
const telas = [
  { nome: 'painel', rota: '/', perfil: null, espera: '.grid-cards' },
  { nome: 'campanha', rota: 'PRIMEIRA_CAMPANHA', perfil: null, espera: '.camp-layout' },
  { nome: 'campanha-doador', rota: 'PRIMEIRA_CAMPANHA', perfil: 'doador', espera: '.donate-card' },
  { nome: 'como-funciona', rota: '/como-funciona', perfil: null, espera: '.howto' },
  { nome: 'termos', rota: '/termos', perfil: null, espera: '.legal' },
  { nome: 'entrar', rota: '/entrar', perfil: null, espera: '.auth-form' },
  { nome: 'cadastro', rota: '/cadastro', perfil: null, espera: '.cad-form' },
  { nome: 'minhas-doacoes', rota: '/minhas-doacoes', perfil: 'doador', espera: 'h1' },
  { nome: 'gestao', rota: '/gestao', perfil: 'gestor', espera: 'h1' },
  { nome: 'gestao-campanhas', rota: '/gestao/campanhas', perfil: 'gestor', espera: 'h1' },
  { nome: 'gestao-campanha', rota: 'PRIMEIRA_CAMPANHA_GESTAO', perfil: 'gestor', espera: '.tiles' },
  { nome: 'gestao-nova', rota: '/gestao/campanhas/nova', perfil: 'gestor', espera: 'form' },
  { nome: 'gestao-doadores', rota: '/gestao/doadores', perfil: 'gestor', espera: 'h1' },
  { nome: 'gestao-doacoes', rota: '/gestao/doacoes', perfil: 'gestor', espera: 'h1' },
  { nome: 'nao-encontrada', rota: '/nao-existe', perfil: null, espera: 'h1' },
].filter(t => !filtro || filtro.includes(t.nome));

const contas = {
  doador: { email: 'mariana@email.demo', senha: 'Doar@2026' },
  gestor: { email: 'helena@esperancasolidaria.demo', senha: 'Gestao@2026' },
};

async function entrar(page, perfil) {
  await page.goto(`${base}/entrar`);
  await page.waitForSelector('#login-email');
  await page.fill('#login-email', contas[perfil].email);
  await page.fill('#login-senha', contas[perfil].senha);
  await page.click('button[type=submit]');
  await page.waitForURL(u => !u.pathname.endsWith('/entrar'), { timeout: 30000 });
}

const problemas = [];
let primeiraCampanha = null;
let primeiraGestao = null;

for (const ap of aparelhos) {
  const browser = await ap.motor.launch();
  for (const perfil of [null, 'doador', 'gestor']) {
    const daVez = telas.filter(t => t.perfil === perfil);
    if (daVez.length === 0) continue;
    const ctx = await browser.newContext({ ...ap.opcoes, locale: 'pt-BR', reducedMotion: 'reduce' });
    const page = await ctx.newPage();
    const erros = [];
    page.on('console', m => { if (m.type() === 'error') erros.push(m.text()); });
    page.on('pageerror', e => erros.push(e.message));
    if (perfil) await entrar(page, perfil);

    for (const t of daVez) {
      erros.length = 0;
      if (t.rota === 'PRIMEIRA_CAMPANHA' && !primeiraCampanha) {
        await page.goto(`${base}/`);
        await page.waitForSelector('.camp-card h3 a');
        primeiraCampanha = new URL(await page.getAttribute('.camp-card h3 a', 'href'), `${base}/`).pathname;
      }
      if (t.rota === 'PRIMEIRA_CAMPANHA_GESTAO' && !primeiraGestao) {
        await page.goto(`${base}/gestao/campanhas`);
        try {
          await page.waitForSelector('td a[href^="gestao/campanhas/"]', { timeout: 15000 });
          primeiraGestao = '/' + await page.getAttribute('td a[href^="gestao/campanhas/"]', 'href');
        } catch { primeiraGestao = '/gestao/campanhas'; }
      }
      const rota = t.rota === 'PRIMEIRA_CAMPANHA' ? primeiraCampanha : t.rota === 'PRIMEIRA_CAMPANHA_GESTAO' ? primeiraGestao : t.rota;
      await page.goto(`${base}${rota}`);
      // Uma nova tentativa antes de acusar: com a máquina carregada (Docker + 5 navegadores),
      // o WebKit às vezes demora mais que o limite para iniciar o WebAssembly.
      let apareceu = false;
      for (let tentativa = 0; tentativa < 2 && !apareceu; tentativa++) {
        if (tentativa > 0) await page.reload();
        try { await page.waitForSelector(t.espera, { timeout: 15000 }); apareceu = true; } catch { }
      }
      if (!apareceu) problemas.push(`${ap.nome}/${t.nome}: não apareceu "${t.espera}"`);
      await page.waitForTimeout(700);

      const largura = await page.evaluate(() => ({ doc: document.documentElement.scrollWidth, tela: window.innerWidth }));
      if (largura.doc > largura.tela + 1) problemas.push(`${ap.nome}/${t.nome}: rolagem horizontal (${largura.doc}px > ${largura.tela}px)`);

      // Elementos visíveis que passam da borda direita da tela, fora de áreas com rolagem própria (tabelas).
      const vazando = await page.evaluate(() => {
        const tela = window.innerWidth, achados = [];
        const rolavel = el => { for (let p = el.parentElement; p; p = p.parentElement) { const o = getComputedStyle(p).overflowX; if (o === 'auto' || o === 'scroll' || o === 'hidden' || o === 'clip') return true; } return false; };
        for (const el of document.querySelectorAll('body *')) {
          const r = el.getBoundingClientRect();
          if (r.width === 0 || r.height === 0 || r.right <= tela + 1) continue;
          if (getComputedStyle(el).position === 'fixed' || rolavel(el)) continue;
          achados.push(`${el.tagName.toLowerCase()}${el.className && typeof el.className === 'string' ? '.' + el.className.trim().split(/\s+/).join('.') : ''} (${Math.round(r.right)}px)`);
          if (achados.length >= 3) break;
        }
        return achados;
      });
      for (const v of vazando) problemas.push(`${ap.nome}/${t.nome}: elemento passa da tela: ${v}`);
      for (const e of erros) problemas.push(`${ap.nome}/${t.nome}: console: ${e.slice(0, 160)}`);

      await page.screenshot({ path: `${pasta}${ap.nome}-${t.nome}.png`, fullPage: true });
    }
    await ctx.close();
  }
  await browser.close();
}

console.log(`${aparelhos.length} aparelhos x ${telas.length} telas. Capturas em ${pasta}`);
if (problemas.length) {
  console.log(`\n${problemas.length} problema(s):\n- ` + problemas.join('\n- '));
  process.exit(1);
}
console.log('Nenhum problema encontrado.');
