// Service worker mínimo: torna o site instalável (PWA) sem guardar cache, para uma versão nova
// aparecer na hora. As requisições seguem direto para a rede.
self.addEventListener('install', () => self.skipWaiting());
self.addEventListener('activate', e => e.waitUntil(self.clients.claim()));
self.addEventListener('fetch', () => { });
