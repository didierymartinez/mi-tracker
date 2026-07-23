--- NOTEBOOKLM SYNC ---
Notebook destino: 🧠 Mi Biblioteca de Conocimiento & Crónicas (ID: 6b703266-4050-4357-b010-ae7076119e5f)
Título de la fuente: Event Sourcing (Dometrain) — Plugging the Domain into an API (Controllers) (Full Transcript)
Acción: crear fuente nueva
---
CONTENIDO:
# Event Sourcing en .NET (Dometrain) — Enchufando el dominio a una API (Controllers)

## Contexto
- No es un curso de ASP.NET ni una API production-ready — solo muestra **cómo enchufar el sistema event-sourced** a una app. Se usan **controllers** (podría ser minimal API sin problema).

## Esquema de URL
- Todos los command endpoints bajo `api/command`, agrupados por aggregate, luego el command: ej. `api/command/box/create`.

## El controller method
- Retorna `IActionResult`, ej. `CreateBox`. Atributos: `[Route("create")]` + `[HttpPost]` (se envía un command nuevo al sistema).
- **Contratos externos ↔ internos:** con terceros deberías **mapear** el contrato externo a uno interno (no reutilizar el command directamente). Boyne "hace trampa" y bindea el command directo `[FromBody] CreateBox command` con un comentario recordando que deberías mapear. En apps internas importa menos.

## El command router por DI
- Se **inyecta el command router** (viene del dominio, registrado en el contenedor de DI).
- `router.Handle(command)` procesa el command.

## Respuesta: 202 Accepted (no 200 OK)
- Boyne devuelve **`Accepted` (202)** a propósito: en un sistema event-sourced las **projections probablemente NO corren sincrónicamente**, así que 202 comunica *"recibí correctamente tu request, pero el procesamiento (read-side) aún no está terminado"* → consistencia eventual.
- Si prefieres, devolver `Ok` (200) está bien; es una decisión de diseño.
