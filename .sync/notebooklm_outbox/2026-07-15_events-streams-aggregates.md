--- NOTEBOOKLM SYNC ---
Notebook destino: 🧠 Mi Biblioteca de Conocimiento & Crónicas (ID: 6b703266-4050-4357-b010-ae7076119e5f)
Título de la fuente: Event Sourcing (Dometrain) — Events, streams & aggregates (Full Transcript)
Acción: crear fuente nueva
---
CONTENIDO:
# Event Sourcing en .NET (Dometrain) — "Events, streams & aggregates"

## Identidad y streams
- Los comandos se procesan contra una **entidad** identificada por un **id** (entidad A, entidad B).
- Tradicional (normalizado): persistir el estado en filas (insert/update + FKs).
- **Events-first:** al procesar un comando, **añades uno o más eventos al stream** de esa entidad. Los eventos reflejan los cambios resultado del comando.
- Un **stream** = los eventos de UNA entidad; permite **replay**. Cada evento se **liga al id** de su entidad; **un evento pertenece a un solo stream** (nunca a dos entidades).

## Reconstruir estado
- Obtener el estado = **reproducir los eventos** hasta el punto necesario → grafo del objeto **en memoria** (no se reconstruye en BD).
- El **orden importa**: reproducir en otro orden puede dar otro resultado → cada evento tiene un **sequence number** (que además ayuda contra problemas de **concurrencia**).
- El stream es **append-only**: solo se agregan eventos, nunca se borran.

## Terminología DDD (la que el instructor mezcla a propósito)
- **Aggregate root** = la entidad (tiene la lógica y el estado; el estado se agrupa aquí).
- **Aggregate** = el *scope* dentro del cual decides cómo responder a un comando (lógica + estado + frontera de consistencia).
- **Stream** = la lista de eventos persistida de ese aggregate (el dato persistente).
- Regla mental: "stream de eventos" ↔ la entidad que representa es el aggregate root ↔ el scope de procesamiento es el aggregate.

## Siguiente
El ciclo de vida para procesar comandos (próxima lección).

## Conexión con el taller
§4 `EventStream<T>` (historia de una Empresa), §8 "el id es la llave" (un cajón por empresa), §9 sequence number/`Version` para concurrencia.
