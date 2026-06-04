#!/usr/bin/env bash
# auto_push.sh — Push automático del tracker a GitHub desde el agente (Cowork).
# Sortea el límite del sandbox (no se puede operar git sobre el mount):
# clona el repo en un dir local, copia los archivos actuales del proyecto y hace push.
#
# Requiere: archivo .git_token en la raíz del proyecto (Fine-grained PAT, Contents R/W).
# Uso:  bash .sync/auto_push.sh "mensaje de commit"
set -euo pipefail

PROJECT_DIR="$(cd "$(dirname "$0")/.." && pwd)"
TOKEN_FILE="$PROJECT_DIR/.git_token"
REPO="didierymartinez/mi-tracker"
WORK="/tmp/mi-tracker-sync"

[ -f "$TOKEN_FILE" ] || { echo "❌ No hay .git_token. Crea el PAT y guárdalo ahí."; exit 1; }
TOKEN="$(tr -d ' \n\r' < "$TOKEN_FILE")"
MSG="${1:-sync: actualización tracker $(date '+%Y-%m-%d %H:%M')}"

# 1. Clon limpio (con token, sin exponerlo en logs)
rm -rf "$WORK"
git clone --quiet "https://x-access-token:${TOKEN}@github.com/${REPO}.git" "$WORK" 2>/dev/null \
  || { echo "❌ Clone/auth falló. ¿Token válido y con acceso a $REPO?"; exit 1; }

# 2. Copiar archivos actuales del proyecto al clon (excluye ruido y secretos)
rsync -a --delete \
  --exclude '.git/' --exclude '.git_token' \
  --exclude '.mcp_venv/' --exclude '.DS_Store' \
  --exclude 'cosmos_analysis/' \
  "$PROJECT_DIR/" "$WORK/"

# 3. Commit + push
cd "$WORK"
git config user.name  "Didier (Cowork sync)"
git config user.email "didier.martinez@sinco.co"
git add -A
if git diff --cached --quiet; then
  echo "✅ Nada nuevo que sincronizar."
else
  git commit --quiet -m "$MSG"
  git push --quiet origin main
  echo "🚀 Push OK → github.com/${REPO}: $MSG"
fi
rm -rf "$WORK"
