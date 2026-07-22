--- NOTEBOOKLM SYNC ---
Notebook destino: 🧠 Mi Biblioteca de Conocimiento & Crónicas (ID: 6b703266-4050-4357-b010-ae7076119e5f)
Título de la fuente: Event Sourcing (Dometrain) — Introduction to the Test Base Class (Full Transcript)
Acción: crear fuente nueva
---
CONTENIDO:
# Event Sourcing en .NET (Dometrain) — "Introduction to the Test Base Class"

## TestStore — IEventStore en memoria (sin mocks)
- Implementación in-memory de `IEventStore` con dos colecciones públicas:
  - **PreviousEvents**: eventos que ya ocurrieron *antes* de ejecutar el command (se llenan por escenario de test).
  - **NewEvents**: eventos que el handler emite *después* de ejecutar (para hacer assert).
- `GetEvents(id)` → devuelve del **PreviousEvents** los eventos del aggregate (lo usa el handler para rehidratar/replay).
- `AppendEvent(...)` → los mete en **NewEvents**.
- Ventaja: **inspeccionas el lado de storage directamente, sin mockear** el IEventStore.

## Base test para command handlers — patrón Given / When / Then
Todos los tests se estructuran igual: *unos eventos pasados → disparar un command → verificar los eventos nuevos resultantes.*
- **Given(...)**: agrega eventos pasados a `PreviousEvents`, transformando los objetos normales en `StoredEvent`. Hay un overload que usa el **aggregate ID ambiente** (campo de la clase base) → para tests de un solo aggregate (el 99%) queda más limpio, sin pasar el id.
- **When(...)**: llama `Handle` en el handler. El **handler es una propiedad abstracta** que cada test implementa (no sabemos qué otros parámetros necesita además del event store). El TestStore alimenta al handler; todo queda enlazado.
- **Then(...)**: assertion. Toma de `NewEvents` los eventos del aggregate, los **ordena**, y hace `Select` del **EventData** (el contenido, no el StoredEvent completo). Luego:
  1. Verifica el **número** de eventos (FluentAssertions: `actual.Length.Should().Be(expected.Length)`).
  2. Recorre ambas colecciones en paralelo: chequea **tipos iguales** y luego **`BeEquivalentTo`** (compara propiedad por propiedad, tan profundo como el object graph) → **no necesita `Equals` en los eventos** y verifica todo el contenido.

## Caveat de FluentAssertions
- En la versión actual, dos eventos **vacíos** (sin propiedades públicas) hacen que `BeEquivalentTo` lance `InvalidOperationException` con mensaje que empieza "no members were found for comparison".
- Como ya se afirmó que son del **mismo tipo** y el esperado tampoco tiene propiedades, **no es un error real** → se **traga (swallow)** esa excepción específica; cualquier otra sí se relanza.

## Extra
- Soporta **múltiples eventos** de un solo command (expected events + check events).
- Recomienda usar **FluentAssertions**.
