// Sessão pelo site: JWT só em memória, refresh token em cookie HttpOnly.
// Login; nada de token no localStorage; recarregar a página mantém o login; sair encerra a sessão no servidor.
// Uso: node sessao.mjs [baseUrl] [webkit|chromium]
import { chromium, webkit, devices } from 'playwright';

const base = (process.argv[2] || 'http://localhost:5173').replace(/\/$/, '');
const motor = process.argv[3] === 'webkit' ? webkit : chromium;
const passo = t => console.log(`- ${t}`);
const falhar = t => { console.error(`FALHOU: ${t}`); process.exit(1); };

const browser = await motor.launch();
const contexto = await browser.newContext({ ...(motor === webkit ? devices['iPhone 15'] : { viewport: { width: 1440, height: 900 } }), locale: 'pt-BR' });
const page = await contexto.newPage();

await page.goto(`${base}/entrar`);
await page.fill('#login-email', 'mariana@email.demo');
await page.fill('#login-senha', 'Doar@2026');
await page.click('button[type=submit]');
await page.waitForURL(u => !u.pathname.endsWith('/entrar'));
passo('login da doadora');

const armazenado = await page.evaluate(() => JSON.stringify({ ...localStorage }) + JSON.stringify({ ...sessionStorage }));
if (/eyJ[\w-]+\.[\w-]+\./.test(armazenado)) falhar('há um JWT no localStorage ou sessionStorage');
const cookie = (await contexto.cookies()).find(c => c.name === 'cs_refresh');
if (!cookie) falhar('cookie cs_refresh não foi gravado');
// O WebKit do Playwright no Windows não implementa SameSite e informa "None"; nos outros motores tem de ser Strict.
const sameSiteOk = cookie.sameSite === 'Strict' || (motor === webkit && process.platform === 'win32');
if (!cookie.httpOnly || !sameSiteOk || cookie.path !== '/api/auth') falhar(`cookie com atributos errados: ${JSON.stringify(cookie)}`);
if (await page.evaluate(() => document.cookie.includes('cs_refresh'))) falhar('o JavaScript da página enxerga o refresh token');
passo('JWT só em memória; refresh token em cookie HttpOnly, SameSite=Strict, Path=/api/auth');

await page.goto(`${base}/minhas-doacoes`);
await page.waitForSelector('h1:has-text("Minhas doações")', { timeout: 30000 });
await page.reload();
await page.waitForSelector('h1:has-text("Minhas doações")', { timeout: 30000 });
passo('recarregou a página e continuou logada (sessão restaurada pelo cookie)');

const cookieAntes = cookie.value;
// No celular o botão fica dentro do menu.
if (await page.isVisible('.menu-btn')) await page.click('.menu-btn');
await page.click('button:has-text("Sair")');
await page.waitForFunction(() => !document.body.innerText.includes('Sair'), null, { timeout: 15000 }).catch(() => { });
passo('saiu');

await page.goto(`${base}/minhas-doacoes`);
await page.waitForURL(/\/entrar/, { timeout: 30000 });
passo('depois de sair, a área do doador pede login de novo');

// O cookie antigo, se alguém o tivesse copiado, também não serve mais.
const resposta = await page.request.post(`${base}/api/auth/refresh`, { headers: { Cookie: `cs_refresh=${cookieAntes}` } });
if (resposta.status() !== 401) falhar(`refresh com cookie revogado devolveu ${resposta.status()}`);
passo('cookie antigo revogado no servidor (401)');

await browser.close();
console.log('OK: sessão com refresh token funcionando pelo site.');
