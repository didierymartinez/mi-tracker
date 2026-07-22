--- NOTEBOOKLM SYNC ---
Notebook destino: 🧠 Mi Biblioteca de Conocimiento & Crónicas (ID: 6b703266-4050-4357-b010-ae7076119e5f)
Título de la fuente: Event Sourcing (Dometrain) — Building Command Handlers (Full Transcript)
Acción: crear fuente nueva
---
CONTENIDO:
# Event Sourcing en .NET (Dometrain) — "Building Command Handlers"

## StoredEvent — el evento con metadatos de persistencia
- Dentro del aggregate los eventos son solo `object` (sin metadatos). Al **persistir** necesitas saber a qué aggregate pertenece, el orden y cuándo pasó.
- `record StoredEvent(Guid AggregateId, long SequenceNumber, DateTime Timestamp, object EventData)` — el mínimo necesario.

## IEventStore (interfaz; implementación después)
- `IEnumerable<StoredEvent> GetEvents(Guid aggregateId)` — trae todos los eventos del aggregate.
- `void AppendEvent(StoredEvent appEvent)` — agrega un evento (prefijo `app` porque `event` es palabra reservada).
- `SaveChanges()` — actúa como **unit of work**: persiste todo lo acumulado.

## EventStream<TEntity> — el stream de UN aggregate
- `class EventStream<TEntity> where TEntity : AggregateRoot, new()`. Constructor recibe `IEventStore` + `aggregateId`.
- `GetEntity()`:
  1. `var events = store.GetEvents(aggregateId)` (todo el histórico).
  2. `var entity = new TEntity();`
  3. `foreach (var e in events) { entity.Apply((dynamic)e.EventData); lastSequenceNumber = e.SequenceNumber; }` — el **cast a `dynamic`** es clave: sin él siempre iría al `Apply(object)` vacío; con él elige el overload correcto.
  4. `return entity;`
- `Append(object @event)`: `lastSequenceNumber++`; crea `new StoredEvent(aggregateId, lastSequenceNumber, DateTime.UtcNow, @event)`; llama `store.AppendEvent(...)`.
- (Luego se extiende con cosas como "entidad en cierto punto del tiempo".)

## CommandHandler<TCommand> — clase base abstracta
- Primary constructor recibe `IEventStore`.
- `abstract void Handle(TCommand command)` — aquí va la lógica de negocio en las implementaciones.
- `protected EventStream<TEntity> GetStream<TEntity>(Guid aggregateId) where TEntity : AggregateRoot, new()` → `return new EventStream<TEntity>(eventStore, aggregateId);`
- **Importante:** NO intentar mapear genéricamente command→entidad (causa fricción). Cada handler toma las props que necesita y pide su stream con `GetStream`.

## Ejemplo: AddShippingLabelHandler : CommandHandler<AddShippingLabel>
```csharp
public class AddShippingLabelHandler(IEventStore eventStore)
    : CommandHandler<AddShippingLabel>(eventStore)
{
    public override void Handle(AddShippingLabel command)
    {
        var boxStream = GetStream<Box>(command.BoxId);   // localizar aggregate por prop del command
        var box = boxStream.GetEntity();                 // dispara el replay de todos los eventos

        if (command.Label.IsValid())                     // aquí brillan los value types
            boxStream.Append(new ShippingLabelAdded(command.Label));
        else
            boxStream.Append(new ShippingLabelFailedToAdd(
                ShippingLabelFailedToAdd.FailReason.TrackingCodeInvalid));
    }
}
```
- **No se guarda aquí.** El `SaveChanges()` se hace en el **command router**, no en el handler → así puedes **encadenar varios handlers y guardar todo como una sola transacción**.

## Organización y payoff
- Un archivo por concepto; carpeta `Commands` con **command + handler en el mismo archivo**.
- De nuevo: crear un command nuevo = siempre ~el mismo trabajo (base + handler + eventos que puede emitir) → **estimación predecible**.
