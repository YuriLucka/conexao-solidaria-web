// PWA: manifesto válido, ícones acessíveis, service worker ativo (Chromium/Android) e a opção "Instalar app"
// no iPhone (WebKit) mostrando o passo a passo do Safari. Uso: node pwa.mjs [baseUrl]
import { chromium, webkit, devices } from 'playwright';

const base = (process.argv[2] || 'http://localhost:8000').replace(/\/$/, '');
const falhas = [];
const ok = (cond, msg) => { if (!cond) falhas.push(msg); else console.log('- ' + msg); };

// Android (Chromium): manifesto, ícones e service worker.
{
  const b = await chromium.launch(); const ctx = await b.newContext({ ...devices['Pixel 7'], locale: 'pt-BR' }); const p = await ctx.newPage();
  await p.goto(`${base}/`); await p.waitForSelector('h1');
  const href = await p.getAttribute('link[rel=manifest]', 'href');
  const r = await p.request.get(`${base}/${href}`);
  const m = await r.json();
  ok(r.ok() && m.display === 'standalone' && m.start_url, 'manifesto com display standalone e start_url');
  ok((r.headers()['content-type'] || '').includes('manifest+json'), 'manifesto servido como application/manifest+json');
  for (const i of m.icons) ok((await p.request.get(`${base}/${i.src}`)).ok(), `ícone ${i.sizes}${i.purpose ? ' ' + i.purpose : ''} acessível`);
  ok(m.icons.some(i => i.purpose === 'maskable'), 'ícone maskable para Android');
  const sw = await p.evaluate(async () => { const reg = await navigator.serviceWorker.ready; return !!reg.active; });
  ok(sw, 'service worker ativo');
  await b.close();
}

// iPhone (WebKit): menu tem "Instalar app" e mostra como adicionar à Tela de Início.
{
  const b = await webkit.launch(); const ctx = await b.newContext({ ...devices['iPhone 15'], locale: 'pt-BR' }); const p = await ctx.newPage();
  await p.goto(`${base}/`); await p.waitForSelector('h1');
  ok(await p.getAttribute('link[rel=apple-touch-icon]', 'href') !== null, 'apple-touch-icon declarado');
  await p.click('.menu-btn');
  const item = p.locator('.instalar-item');
  await item.waitFor({ timeout: 10000 }).catch(() => { });
  ok(await item.isVisible(), 'iPhone: "Instalar app" no menu');
  if (await item.isVisible()) {
    await item.click();
    ok(await p.locator('.instalar-ios').isVisible(), 'iPhone: passo a passo do Safari aparece');
  }
  await b.close();
}

if (falhas.length) { console.log('\nFalhas:\n- ' + falhas.join('\n- ')); process.exit(1); }
console.log('OK: PWA instalável no Android e com instruções no iPhone.');
