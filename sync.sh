#!/usr/bin/env bash
# sync.sh — Sincroniza el tracker a GitHub.
# Uso:  bash sync.sh "mensaje de commit"
# Si no pasas mensaje, usa uno con la fecha actual.
set -e

cd "$(dirname "$0")"

# 1. Limpia un posible lock huérfano (de procesos interrumpidos)
[ -f .git/index.lock ] && rm -f .git/index.lock && echo "🔓 index.lock removido"

# 2. Mensaje de commit
MSG="${1:-sync: actualización tracker $(date '+%Y-%m-%d %H:%M')}"

# 3. Stage de todo lo relevante (ignora .DS_Store y el venv)
git add -A

# 4. ¿Hay algo que commitear?
if git diff --cached --quiet; then
  echo "✅ Nada nuevo que sincronizar."
  exit 0
fi

# 5. Commit + push
git commit -m "$MSG"
git push origin main
echo "🚀 Sincronizado a GitHub: $MSG"
