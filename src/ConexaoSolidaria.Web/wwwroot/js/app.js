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
  focar: id => document.getElementById(id)?.focus()
};

// Aplica o tema antes do Blazor carregar, para não piscar claro no modo escuro.
window.cs.aplicarTema(window.cs.temaPreferido());
