// Capturas para revisão visual humana/IA: telas principais em desktop, iPhone (WebKit) e Android (Chromium),
// nos temas claro e escuro. Salva a página inteira; tools/fotos não precisa rodar.
// Uso: node revisao.mjs [baseUrl] [pasta de saída]
import { chromium, webkit, devices } from 'playwright';
import { mkdirSync } from 'node:fs';

const base = (process.argv[2] || 'http://localhost:8000').replace(/\/$/, '');
const pasta = (process.argv[3] || new URL('./revisao/', import.meta.url).pathname.replace(/^\/([A-Z]:)/, '$1')).replace(/\/?$/, '/');
mkdirSync(pasta, { recursive: true });

const aparelhos = [
  { nome: 'desktop', motor: chromium, opcoes: { viewport: { width: 1440, height: 900 } }, temas: ['light', 'dark'] },
  { nome: 'iphone15', motor: webkit, opcoes: devices['iPhone 15'], temas: ['light', 'dark'] },
  { nome: 'android', motor: chromium, opcoes: devices['Pixel 7'], temas: ['light'] },
];
const telas = [
  { nome: 'painel', rota: '/', perfil: null },
  { nome: 'campanha', rota: 'CAMPANHA', perfil: 'doador' },
  { nome: 'como-funciona', rota: '/como-funciona', perfil: null },
  { nome: 'entrar', rota: '/entrar', perfil: null },
  { nome: 'cadastro', rota: '/cadastro', perfil: null },
  { nome: 'minhas-doacoes', rota: '/minhas-doacoes', perfil: 'doador' },
  { nome: 'gestao', rota: '/gestao', perfil: 'gestor' },
  { nome: 'gestao-campanhas', rota: '/gestao/campanhas', perfil: 'gestor' },
  { nome: 'gestao-campanha', rota: 'CAMPANHA_GESTAO', perfil: 'gestor' },
  { nome: 'gestao-nova', rota: '/gestao/campanhas/nova', perfil: 'gestor' },
  { nome: 'gestao-doadores', rota: '/gestao/doadores', perfil: 'gestor' },
  { nome: 'gestao-doacoes', rota: '/gestao/doacoes', perfil: 'gestor' },
];
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

const especiais = {};
async function rota(page, t) {
  if (t.rota === 'CAMPANHA') {
    if (especiais.c === undefined) {
      await page.goto(`${base}/`);
      especiais.c = await page.locator('.camp-card h3 a').first().getAttribute('href', { timeout: 15000 }).then(h => '/' + h).catch(() => null);
    }
    return especiais.c;
  }
  if (t.rota === 'CAMPANHA_GESTAO') {
    if (especiais.g === undefined) {
      await page.goto(`${base}/gestao/campanhas`);
      especiais.g = await page.locator('td a[href^="gestao/campanhas/"]').first().getAttribute('href', { timeout: 15000 }).then(h => '/' + h).catch(() => null);
    }
    return especiais.g;
  }
  return t.rota;
}

for (const ap of aparelhos) {
  const browser = await ap.motor.launch();
  for (const tema of ap.temas) {
    for (const perfil of [null, 'doador', 'gestor']) {
      const daVez = telas.filter(t => t.perfil === perfil);
      const ctx = await browser.newContext({ ...ap.opcoes, locale: 'pt-BR', colorScheme: tema, reducedMotion: 'reduce' });
      const page = await ctx.newPage();
      if (perfil) await entrar(page, perfil);
      for (const t of daVez) {
        const r = await rota(page, t);
        if (!r) continue;
        await page.goto(`${base}${r}`);
        await page.waitForSelector('h1', { timeout: 20000 }).catch(() => { });
        await page.waitForTimeout(1200);
        await page.screenshot({ path: `${pasta}${ap.nome}-${tema}-${t.nome}.png`, fullPage: true });
      }
      await ctx.close();
    }
  }
  await browser.close();
}
console.log(`Capturas de revisão em ${pasta}`);
