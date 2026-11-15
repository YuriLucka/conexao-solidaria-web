// Pequenas funções do navegador usadas pelo Blazor (armazenamento, tema, rolagem).
window.cs = {
  ler: chave => { try { return localStorage.getItem(chave); } catch { return null; } },
  gravar: (chave, valor) => { try { localStorage.setItem(chave, valor); } catch { } },
  apagar: chave => { try { localStorage.removeItem(chave); } catch { } },

  // Tema igual ao do protótipo: atributo data-cs-tema="escuro" no <html>.
  temaPreferido: () => {
    const salvo = window.cs.ler('conexao-solidaria:tema');
    if (salvo === 'escuro' || salvo === 'claro') return salvo;
    return matchMedia('(prefers-color-scheme: dark)').matches ? 'escuro' : 'claro';
  },
  aplicarTema: tema => {
    if (tema === 'escuro') document.documentElement.dataset.csTema = 'escuro';
    else delete document.documentElement.dataset.csTema;
    window.cs.gravar('conexao-solidaria:tema', tema);
  },

  topo: () => window.scrollTo(0, 0),
  rolarAte: id => document.getElementById(id)?.scrollIntoView({ behavior: 'smooth', block: 'center' }),

  // Página da campanha: a barra fixa de doar some enquanto o cartão de doação aparece na tela.
  observarCartaoDoar: () => {
    const bar = document.getElementById('doar-bar'), card = document.getElementById('doar-card');
    if (!bar || !card || !('IntersectionObserver' in window)) return;
    window.cs._obsDoar?.disconnect();
    window.cs._obsDoar = new IntersectionObserver(es => es.forEach(e => bar.classList.toggle('oculta', e.isIntersecting)), { threshold: .2 });
    window.cs._obsDoar.observe(card);
  },
  focar: id => document.getElementById(id)?.focus(),

  // Conta de 0 até o valor final (uma vez, na primeira exibição). O texto final já vem renderizado pelo Blazor,
  // então sem JavaScript ou com "reduzir movimento" o número aparece direto.
  contar: (el, valor, moeda) => {
    if (!el || matchMedia('(prefers-reduced-motion: reduce)').matches || !(valor > 0)) return;
    const formato = new Intl.NumberFormat('pt-BR', moeda ? { style: 'currency', currency: 'BRL', maximumFractionDigits: 0 } : { maximumFractionDigits: 0 });
    const final = el.textContent, inicio = performance.now(), duracao = 1100;
    const passo = agora => {
      const t = Math.min(1, (agora - inicio) / duracao), suave = 1 - Math.pow(1 - t, 3);
      el.textContent = t < 1 ? formato.format(Math.round(valor * suave)) : final;
      if (t < 1) requestAnimationFrame(passo);
    };
    requestAnimationFrame(passo);
  }
};

// Valores grandes ("R$ 18.326.757") ficam numa linha: se não cabem no cartão, a fonte diminui só o necessário.
(() => {
  const seletor = '.tile .val, .hero-fig .big, .donate-card .big';
  const ajustar = el => {
    el.style.fontSize = '';
    let t = parseFloat(getComputedStyle(el).fontSize);
    while (el.scrollWidth > el.clientWidth + 1 && t > 14) { t -= 1; el.style.fontSize = t + 'px'; }
  };
  let pendente = false;
  const tudo = () => { if (pendente) return; pendente = true; requestAnimationFrame(() => { pendente = false; document.querySelectorAll(seletor).forEach(ajustar); }); };
  new MutationObserver(tudo).observe(document.documentElement, { childList: true, subtree: true, characterData: true });
  addEventListener('resize', tudo);
  addEventListener('load', tudo);
})();

// Instalação como app (PWA). Chromium/Android entregam o evento beforeinstallprompt; no iPhone o Safari não tem
// botão de instalar, então o site mostra como fazer pelo menu Compartilhar. Sem modal automático: só a opção no menu.
window.csInstalar = (() => {
  let aviso = null, ouvinte = null;
  const instalado = () => matchMedia('(display-mode: standalone)').matches || navigator.standalone === true;
  const ios = () => /iphone|ipad|ipod/i.test(navigator.userAgent) || (navigator.platform === 'MacIntel' && navigator.maxTouchPoints > 1);
  const estado = () => instalado() ? 'instalado' : aviso ? 'pronto' : ios() ? 'ios' : 'indisponivel';
  const avisar = () => ouvinte?.invokeMethodAsync('Atualizar', estado());
  addEventListener('beforeinstallprompt', e => { e.preventDefault(); aviso = e; avisar(); });
  addEventListener('appinstalled', () => { aviso = null; avisar(); });
  if ('serviceWorker' in navigator) addEventListener('load', () => navigator.serviceWorker.register('service-worker.js').catch(() => { }));
  return {
    estado: ref => { if (ref) ouvinte = ref; return estado(); },
    instalar: async () => {
      if (!aviso) return estado();
      aviso.prompt();
      const { outcome } = await aviso.userChoice;
      aviso = null;
      return outcome === 'accepted' ? 'instalado' : estado();
    }
  };
})();

// Entrada suave dos blocos marcados com .revelar ao entrarem na tela. Só esconde quando o observador existe
// (classe "animar" no <html>); com "reduzir movimento" nada se move.
(() => {
  if (!('IntersectionObserver' in window) || matchMedia('(prefers-reduced-motion: reduce)').matches) return;
  document.documentElement.classList.add('animar');
  const obs = new IntersectionObserver(es => es.forEach(e => {
    if (e.isIntersecting) { e.target.classList.add('visivel'); obs.unobserve(e.target); }
  }), { rootMargin: '0px 0px -8% 0px' });
  const ligar = raiz => raiz.querySelectorAll?.('.revelar:not(.visivel)').forEach(el => obs.observe(el));
  new MutationObserver(ms => ms.forEach(m => m.addedNodes.forEach(n => { if (n.nodeType === 1) { if (n.matches('.revelar')) obs.observe(n); ligar(n); } })))
    .observe(document.documentElement, { childList: true, subtree: true });
  ligar(document);
})();

// Aplica o tema antes do Blazor carregar, para não piscar claro no modo escuro.
window.cs.aplicarTema(window.cs.temaPreferido());
