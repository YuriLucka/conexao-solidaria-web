"""Converte as fotos geradas (PNG/JPG) para WebP leve em wwwroot/img/fotos.

Uso: python tools/fotos/converter.py <login.png> <painel.png>
  login  -> retrato, até 1000x1500 (fundo do painel verde do login)
  painel -> paisagem, até 1600x1067 (topo do painel de transparência)
"""
import sys
from pathlib import Path
from PIL import Image

DESTINO = Path(__file__).resolve().parents[2] / "src/ConexaoSolidaria.Web/wwwroot/img/fotos"
LIMITES = {"login": (1000, 1500), "painel": (1600, 1067)}


def converter(origem: Path, nome: str) -> None:
    img = Image.open(origem).convert("RGB")
    img.thumbnail(LIMITES[nome], Image.LANCZOS)
    saida = DESTINO / f"{nome}.webp"
    img.save(saida, "WEBP", quality=74, method=6)
    print(f"{saida.name}: {img.width}x{img.height}, {saida.stat().st_size // 1024} KB")


if __name__ == "__main__":
    if len(sys.argv) != 3:
        sys.exit(__doc__)
    DESTINO.mkdir(parents=True, exist_ok=True)
    converter(Path(sys.argv[1]), "login")
    converter(Path(sys.argv[2]), "painel")
