// Acessibilidade: roda o axe-core (regras WCAG 2.1 A/AA) em cada tela, no tema claro e no escuro,
// no desktop (Chromium) e no iPhone (WebKit).
// Uso: node acessibilidade.mjs [baseUrl] [telas separadas por vírgula]
//   baseUrl padrão: http://localhost:5173 — atrás do Kong use http://localhost:8000
// Sai com código 1 se achar alguma violação (o resumo lista regra, tela e elementos).
import { chromium, webkit, devices } from 'playwright';
import AxeBuilder from '@axe-core/playwright';

const base = (process.argv[2] || 'http://localhost:5173').replace(/\/$/, '');
const filtro = process.argv[3]?.split(',');

const aparelhos = [
  { nome: 'desktop', motor: chromium, opcoes: { viewport: { width: 1440, height: 900 } } },
  { nome: 'iphone15', motor: webkit, opcoes: devices['iPhone 15'] },
];
const temas = ['light', 'dark'];

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
  await page.waitForURL(u => !u.pathname.endsWith('/entrar'), { timeout: 15000 });
}

// regra -> { impacto, ajuda, ocorrencias: Set("aparelho/tema/tela: alvo") }
const violacoes = new Map();
let primeiraCampanha = null;
let analises = 0;

for (const ap of aparelhos) {
  const browser = await ap.motor.launch();
  for (const tema of temas) {
    for (const perfil of [null, 'doador', 'gestor']) {
      const daVez = telas.filter(t => t.perfil === perfil);
      if (daVez.length === 0) continue;
      // O site segue o tema do sistema quando o visitante não escolheu nenhum.
      const ctx = await browser.newContext({ ...ap.opcoes, locale: 'pt-BR', colorScheme: tema, reducedMotion: 'reduce' });
      const page = await ctx.newPage();
      if (perfil) await entrar(page, perfil);

      for (const t of daVez) {
        if (t.rota === 'PRIMEIRA_CAMPANHA' && !primeiraCampanha) {
          await page.goto(`${base}/`);
          await page.waitForSelector('.camp-card h3 a');
          primeiraCampanha = new URL(await page.getAttribute('.camp-card h3 a', 'href'), `${base}/`).pathname;
        }
        const rota = t.rota === 'PRIMEIRA_CAMPANHA' ? primeiraCampanha : t.rota;
        await page.goto(`${base}${rota}`);
        await page.waitForSelector(t.espera, { timeout: 15000 });
        await page.waitForTimeout(800);

        const r = await new AxeBuilder({ page })
          .withTags(['wcag2a', 'wcag2aa', 'wcag21a', 'wcag21aa'])
          .analyze();
        analises++;
        for (const v of r.violations) {
          const item = violacoes.get(v.id) ?? { impacto: v.impact, ajuda: v.help, ocorrencias: new Set() };
          for (const n of v.nodes) item.ocorrencias.add(`${ap.nome}/${tema}/${t.nome}: ${n.target.join(' ')}`);
          violacoes.set(v.id, item);
        }
      }
      await ctx.close();
    }
  }
  await browser.close();
}

console.log(`${analises} análises (${aparelhos.length} aparelhos x ${temas.length} temas x ${telas.length} telas).`);
if (violacoes.size) {
  for (const [regra, v] of violacoes) {
    console.log(`\n[${v.impacto}] ${regra}: ${v.ajuda} (${v.ocorrencias.size})`);
    for (const o of [...v.ocorrencias].slice(0, 12)) console.log(`  - ${o}`);
    if (v.ocorrencias.size > 12) console.log(`  ... mais ${v.ocorrencias.size - 12}`);
  }
  process.exit(1);
}
console.log('Nenhuma violação WCAG 2.1 AA encontrada.');
