--- NOTEBOOKLM SYNC ---
Notebook destino: 🧠 Mi Biblioteca de Conocimiento & Crónicas (ID: 6b703266-4050-4357-b010-ae7076119e5f)
Título de la fuente: Event Sourcing (Dometrain) — Implementing the Event Store with Dapper (Full Transcript)
Acción: crear fuente nueva
---
CONTENIDO:
# Event Sourcing en .NET (Dometrain) — Implementando el Event Store con Dapper

Se reemplazan las 3 `NotImplementedException` del `IEventStore` con código Dapper.

## GetEvents (SELECT)
- Query SQL que selecciona los 6 campos (coinciden con las props de `DatabaseEvent`) de la tabla `events`, **WHERE AggregateId = @aggregate**, **ORDER BY SequenceNumber** → orden correcto de ocurrencia (si no, el aggregate se rehidrata mal).
- `using` de la `DbConnection` (de la factory) → Dapper abre cuando hace falta y, por el `using`, la devuelve al pool al terminar.
- `connection.Query<DatabaseEvent>(query, new { aggregate = aggregateId })` → Dapper mapea el objeto anónimo al parámetro `@aggregate`.
- Devuelve `IEnumerable<DatabaseEvent>` → `.Select(e => e.ToStoredEvent())` → `IEnumerable<StoredEvent>` (aquí brilla el mapping bidireccional).

## AppendEvent + SaveChanges = unit of work (estilo DbContext de EF)
- **AppendEvent** NO va a la BD: solo **cachea** el evento en una lista privada `List<StoredEvent> newEvents` (instanciada). `newEvents.Add(...)`.
- **SaveChanges** es donde se **ejecuta y commitea** todo.

## SaveChanges (INSERT en transacción)
- INSERT en `dbo.events` con 5 parámetros (AggregateId, SequenceNumber, Timestamp, EventType, EventBody…) → si pasas un objeto que matchea esos params (ej. `DatabaseEvent`), Dapper los inserta.
- `using` de connection; hay que **abrir la conexión** primero, luego `using` transaction = `connection.BeginTransaction()` (para que todo se commitee junto o nada).
- `connection.Execute(insertCommand, newEvents.Select(DatabaseEvent.From), transaction)`:
  - `Execute` = para statements sin resultados.
  - Dapper detecta que es una **colección** → ejecuta el insert **una vez por item**.
  - Se pasa la `transaction` para ligarlo.
- Si todo va bien: `transaction.Commit()` y **limpiar `newEvents`** (para dejarlo listo/limpio si se reusa).

## Nota
- En sistemas más avanzados se agrega **metadata** a la tabla: correlation IDs, conversation IDs, etc. Esto es el event store **más simple** que funciona y permite correr un sistema completo.
