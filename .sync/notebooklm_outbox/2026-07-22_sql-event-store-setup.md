--- NOTEBOOKLM SYNC ---
Notebook destino: 🧠 Mi Biblioteca de Conocimiento & Crónicas (ID: 6b703266-4050-4357-b010-ae7076119e5f)
Título de la fuente: Event Sourcing (Dometrain) — SQL Event Store Setup (Dapper) (Full Transcript)
Acción: crear fuente nueva
---
CONTENIDO:
# Event Sourcing en .NET (Dometrain) — Setup del Event Store en SQL (Dapper)

## Proyecto web (la API)
- ASP.NET simple: Razor pages + controllers, **OpenAPI + Swagger UI** configurado. Un **controller placeholder** (aún no hace nada). Connection strings de **LocalDB** en `appsettings` (reemplazables).

## Lado del event store (scaffolding preparado)
- **`EventStore`**: implementación stub (lanza `NotImplementedException` por ahora). Se implementará con **Dapper** (micro-ORM: ejecutar SQL sobre `DbConnection` sin ceremonia).
- **Connection factory:** saca la connection string de `IConfiguration` y crea nuevas `SqlConnection`. Nada más.

## `DatabaseEvent` — representación en BD (≠ StoredEvent en dominio)
- En BD no se puede guardar un `object` → hay que **serializar**. Campos:
  - `AggregateId`, `SequenceNumber` (qué aggregate + orden).
  - `Timestamp` (DateTime, cuándo ocurrió).
  - En vez de `EventData` (object): **`EventTypeName`** (nombre del tipo de la clase → sabemos qué hay guardado) + **`EventBody`** (el objeto serializado a **JSON**).
  - Columna SQL **`timestamp`/rowversion**: se auto-incrementa cada vez que se toca el registro (insert/update) → **muy útil para projections** más adelante.

## Mapping en ambas direcciones
- `StoredEvent` (dominio) ⇄ `DatabaseEvent` (BD).
- **De StoredEvent → DatabaseEvent:** saca el type name y **serializa** `EventData` a JSON.
- **De DatabaseEvent → StoredEvent:** convierte el type name a `Type`, **deserializa** el `EventBody` en `EventData`, y devuelve un `StoredEvent`. (Muchas excepciones que "nunca deberían ocurrir" porque los datos deberían estar completos.)

## Script CREATE TABLE
- Columnas; `nvarchar(max)` para `EventBody`, `nvarchar(256)` para el type name.
- **Primary key compuesta = (AggregateId, SequenceNumber).** Beneficios:
  - **Concurrency checking automático:** no se puede insertar dos veces el mismo `SequenceNumber` para el mismo aggregate → concurrencia optimista gratis.
  - Se escribe en **páginas distintas** de la tabla → más writes concurrentes, a costa de **page splits** ocasionales. (Alternativa: clave artificial + escribir solo en la última página.)
- Boyne lo corre en Azure Data Studio → tabla `events` creada. "Usado en producción, funciona de maravilla para la mayoría de escenarios."
