--- NOTEBOOKLM SYNC ---
Notebook destino: 🧠 Mi Biblioteca de Conocimiento & Crónicas (ID: 6b703266-4050-4357-b010-ae7076119e5f)
Título de la fuente: Event Sourcing (Dometrain) — CQRS Architecture (Full Transcript)
Acción: crear fuente nueva
---
CONTENIDO:
# Event Sourcing en .NET (Dometrain) — "CQRS Architecture"

## Punto de partida: CQS (Command Query Separation)
- Dos tipos de operaciones: **comando** (solo cambia estado, **no** devuelve resultados — a lo sumo la entidad modificada) y **query** (solo lee, **no** cambia estado).
- Buena práctica general: las queries nunca deben mutar estado (los consumidores no lo esperarían); los comandos no devuelven grandes resultados.

## CQRS = CQS llevado a arquitectura
- Command site y Query site son **dos APIs completamente separadas**, cada una con su **modelo optimizado**: uno para escribir (comandos), otro para leer (queries).
- El dato debe **moverse** del command site al query site. Para (semi) tiempo real con carga alta, la mejor forma es **emitir eventos** en el command site que describan el resultado de cada operación.

## Proyecciones (projections)
- Los eventos entran a una **projection** que actualiza tablas **optimizadas para consulta** en el read store.
- Como los comandos son fine-grained, las operaciones de la projection suelen ser pequeñas.
- Las projections corren **en aislamiento** (igual que los command handlers).

## Por qué Event Sourcing encaja perfecto con CQRS
- En el command site usamos **Event Sourcing**: persistimos todos los eventos en un stream.
- **Eventos de persistencia ≈ eventos de comunicación** (se mantienen idénticos). Muy poderoso: una projection nueva puede **reproducir todos los eventos desde el inicio** y llenar la nueva feature con datos históricos.
- Usar eventos como modelo de persistencia **y** de comunicación reduce complejidad: con CRUD + eventos tendrías que garantizar que todo el dato viva en los eventos y además guardarlos aparte.

## Herramienta de estimación (doble)
- Complejidad de una projection ≈ cuántos **tipos de eventos fuente** necesita para llenar sus datos.
- Ej: 4 projections que consumen 17 tipos de eventos + 7 comandos → estimación predecible del feature completo. **Command handlers + projections = feature medible por adelantado.**
