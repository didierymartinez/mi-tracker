--- NOTEBOOKLM SYNC ---
Notebook destino: 🧠 Mi Biblioteca de Conocimiento & Crónicas (ID: 6b703266-4050-4357-b010-ae7076119e5f)
Título de la fuente: Event Sourcing (Dometrain) — The Command Handler Life Cycle (Full Transcript)
Acción: crear fuente nueva
---
CONTENIDO:
# Event Sourcing en .NET (Dometrain) — "The Command Handler Life Cycle"

## El ciclo de vida (6 pasos, siempre el mismo)
1. **Comando → Handler.** El comando entra y llama a la lógica de negocio (el *handler*).
2. **Localizar el aggregate / stream.** El handler usa una o más propiedades del comando (normalmente el **id**) para saber a qué stream hablarle.
3. **Fetch + Replay.** Trae todos los eventos del stream y los **reproduce en memoria** (aplica cada evento pasado) para reconstruir el estado actual. Liviano: miles de eventos en pocos ms, porque el pasado no se re-valida.
4. **Execute.** Con el estado reconstruido, el handler decide. **Aquí vive la decisión de negocio** (aceptar/rechazar).
5. **Nuevos eventos.** El resultado del execute son **1..N eventos nuevos**.
6. **Append.** Esos eventos se **agregan al stream** (append-only). *Apply* ≠ *Append*: apply es interno del replay; append es la persistencia final.

> Si un comando se rechaza y no produce eventos, **no hay append → el stream queda intacto**.

## Propiedades clave
- Manejar un comando **no tiene efectos secundarios** sobre otros aggregates ni sobre otras partes del sistema → cada handler se desarrolla **en aislamiento**.
- El esfuerzo por handler es casi **constante** → puedes **estimar un feature contando cuántos comandos tiene**.
- A escala (aggregates con miles de eventos) → **snapshotting / caching**.
