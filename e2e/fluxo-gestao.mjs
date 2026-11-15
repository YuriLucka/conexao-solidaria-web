// Roteiro d.ii pelo site: gestora cria campanha (inválida e válida), vê no painel, conclui e confirma que sumiu do painel.
// Uso: node fluxo-gestao.mjs [baseUrl] [webkit|chromium]
import { chromium, webkit, devices } from 'playwright';

const base = (process.argv[2] || 'http://localhost:5173').replace(/\/$/, '');
const motor = process.argv[3] === 'webkit' ? webkit : chromium;
const pasta = new URL('./capturas/', import.meta.url).pathname.replace(/^\/([A-Z]:)/, '$1');
const titulo = `Teste E2E ${Date.now().toString().slice(-6)}`;
const passo = t => console.log(`- ${t}`);

const browser = await motor.launch();
const page = await (await browser.newContext({ ...(motor === webkit ? devices['iPhone 15'] : { viewport: { width: 1440, height: 900 } }), locale: 'pt-BR' })).newPage();

await page.goto(`${base}/entrar`);
await page.fill('#login-email', 'helena@esperancasolidaria.demo');
await page.fill('#login-senha', 'Gestao@2026');
await page.click('button[type=submit]');
await page.waitForURL(/\/gestao$/);
passo('login da gestora: abriu a visão geral');

await page.goto(`${base}/gestao/campanhas/nova`);
await page.waitForSelector('#f-titulo');
await page.fill('#f-titulo', titulo);
await page.fill('#f-descricao', 'Campanha criada pelo teste de ponta a ponta do site.');
await page.fill('#f-fim', '01/01/2020');
await page.fill('#f-meta', '0');
await page.click('button[type=submit]');
await page.waitForSelector('#err-f-fim:not(:empty)');
const erroFim = await page.textContent('#err-f-fim');
const erroMeta = await page.textContent('#err-f-meta');
await page.screenshot({ path: `${pasta}fluxo-gestao-invalida.png`, fullPage: true });
passo(`campanha inválida bloqueada: "${erroFim}" / "${erroMeta}"`);

const f = new Date(Date.now() + 40 * 864e5);
const fim = `${String(f.getDate()).padStart(2, '0')}/${String(f.getMonth() + 1).padStart(2, '0')}/${f.getFullYear()}`;
await page.fill('#f-fim', fim);
await page.fill('#f-meta', '1.500,00');
await page.click('label.capa-opt:has(#f-capa-festa)');
await page.click('button[type=submit]');
await page.waitForURL(/\/gestao\/campanhas\/[0-9a-f-]{36}$/);
passo('campanha válida criada (201) e detalhe aberto');

await page.goto(`${base}/`);
await page.waitForSelector(`.camp-card:has-text("${titulo}")`);
passo('aparece no painel público');

await page.goBack();
await page.waitForSelector('button:has-text("Concluir")');
await page.click('button:has-text("Concluir")');
await page.waitForSelector('.modal');
await page.screenshot({ path: `${pasta}fluxo-gestao-confirmar.png` });
await page.click('.modal .btn-primary');
await page.waitForSelector('.pill-info');
passo('concluída pela janela de confirmação');

await page.goto(`${base}/`);
await page.waitForSelector('.grid-cards');
await page.waitForTimeout(1000);
const aindaAparece = await page.locator(`.camp-card:has-text("${titulo}")`).count();
passo(aindaAparece ? 'ERRO: ainda aparece no painel' : 'saiu do painel público (só Ativa aparece)');

await browser.close();
if (aindaAparece || !erroFim || !erroMeta) process.exit(1);
console.log('OK: fluxo da gestão completo pelo site.');
