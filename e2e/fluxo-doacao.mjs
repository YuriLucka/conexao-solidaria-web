// Roteiro d.iii pelo site, contra a API real: doadora entra, doa, acompanha até "Confirmada" e vê o valor subir.
// Uso: node fluxo-doacao.mjs [baseUrl] [webkit|chromium]
import { chromium, webkit, devices } from 'playwright';

const base = (process.argv[2] || 'http://localhost:5173').replace(/\/$/, '');
const motor = process.argv[3] === 'chromium' ? chromium : webkit;
const pasta = new URL('./capturas/', import.meta.url).pathname.replace(/^\/([A-Z]:)/, '$1');
const lerReais = t => Number(t.replace(/[^\d,]/g, '').replace(',', '.'));

const browser = await motor.launch();
const page = await (await browser.newContext({ ...(motor === webkit ? devices['iPhone 15'] : { viewport: { width: 1440, height: 900 } }), locale: 'pt-BR' })).newPage();
const passo = (t) => console.log(`- ${t}`);

await page.goto(`${base}/`);
await page.waitForSelector('.camp-card h3 a');
await page.click('.camp-card h3 a');
await page.waitForSelector('.donate-card');
const antes = lerReais(await page.textContent('[data-teste=valor-arrecadado]'));
passo(`campanha aberta; arrecadado antes: R$ ${antes}`);

await page.click('.donate-card .btn-doar');
await page.waitForURL(/\/entrar$/);
await page.fill('#login-email', 'mariana@email.demo');
await page.fill('#login-senha', 'Doar@2026');
await page.click('button[type=submit]');
await page.waitForSelector('.donate-card form');
passo('login da doadora e volta para a campanha');

await page.fill('#valor-doacao', '37,50');
await page.click('#btn-doar');
await page.waitForURL(/\/doacoes\//);
passo('doação enviada; acompanhamento aberto');
await page.waitForSelector('.status-hero.celebrar', { timeout: 30000 });
await page.screenshot({ path: `${pasta}fluxo-acompanhamento-confirmada.png`, fullPage: true });
passo('doação confirmada pelo worker');

await page.click('a:has-text("Ver a campanha")');
await page.waitForSelector('[data-teste=valor-arrecadado]');
await page.waitForFunction(v => Number(document.querySelector('[data-teste=valor-arrecadado]').textContent.replace(/[^\d,]/g, '').replace(',', '.')) >= v, antes + 37.5, { timeout: 20000 });
const depois = lerReais(await page.textContent('[data-teste=valor-arrecadado]'));
passo(`valor na campanha: R$ ${antes} -> R$ ${depois}`);

await page.goto(`${base}/minhas-doacoes`);
await page.waitForSelector('table');
await page.screenshot({ path: `${pasta}fluxo-minhas-doacoes.png`, fullPage: true });
passo('minhas doações listando a doação');

await browser.close();
if (depois < antes + 37.5) { console.log('FALHOU: valor não subiu'); process.exit(1); }
console.log('OK: fluxo de doação completo pelo site.');
