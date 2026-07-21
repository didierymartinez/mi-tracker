--- NOTEBOOKLM SYNC ---
Notebook destino: 🧠 Mi Biblioteca de Conocimiento & Crónicas (ID: 6b703266-4050-4357-b010-ae7076119e5f)
Título de la fuente: Event Sourcing (Dometrain) — Future, Present & Past (Full Transcript)
Acción: crear fuente nueva
---
CONTENIDO:
# Event Sourcing en .NET (Dometrain) — "Future, Present & Past"

## Idea central
A través de "los ojos del tiempo": **comando = futuro**, **estado = presente**, **evento = pasado**. Al dudar entre guardar estado o eventos, guarda **eventos**: no pierdes información y puedes reconstruir el estado con replay.

## El flujo
Llega un **comando** → dispara la **lógica de negocio** → la lógica interactúa con el **estado actual** de la(s) entidad(es) → produce **eventos**. Guardar eventos (no reescribir estado) evita perder información.

## Por qué NO guardar comandos (aunque son el entry point)
1. **La lógica evoluciona:** reprocesar el mismo comando en el futuro puede producir un resultado distinto al que ya se **comunicó al exterior** en el pasado. Lo comunicado debe permanecer válido. (Los comandos sirven para logging, no como estrategia de persistencia.)
2. **La lógica es pesada y con efectos secundarios:** reprocesar comandos vuelve a llamar APIs/storage (side effects). Reproducir **eventos** es liviano — no hay que dudar de su validez porque el trabajo pesado ya ocurrió; el replay solo re-setea propiedades del estado. Miles de eventos en milisegundos.

## Estado vs eventos
- De **eventos → estado**: sí (replay).
- De **estado → eventos**: no (falta información).
- Guardar solo estado = sistema CRUD clásico (se descarta la historia).

## Regla
Guarda **eventos** si (a) no quieres perder información y (b) quieres un sistema fácil de mantener (sin recomputar lógica pesada ni arriesgar resultados distintos a los ya comunicados).
