--- NOTEBOOKLM SYNC ---
Notebook destino: 🧠 Mi Biblioteca de Conocimiento & Crónicas (ID: 6b703266-4050-4357-b010-ae7076119e5f)
Título de la fuente: Event Sourcing (Dometrain) — Making Root Entities (Full Transcript)
Acción: crear fuente nueva
---
CONTENIDO:
# Event Sourcing en .NET (Dometrain) — "Making Root Entities"

## Root entity vs aggregate
- `Box` es la **root entity** del aggregate Box. Se confunde a menudo con el aggregate: aquí `Box` = root entity, y **todo lo que vive en la carpeta `Boxes`** = el aggregate completo.
- No se persiste ni se hace fetch de la root entity: se **reconstruye a partir de eventos pasados** (replay).

## Clase base `AggregateRoot`
- `abstract class AggregateRoot` con un método `Apply(object @event)`.
- **Dos enfoques:**
  1. `Apply` **abstract** → cada aggregate lo implementa, hace switch por tipo de evento y llama a métodos específicos. **Fuertemente tipado.**
  2. **(el que usa Boyne)** `Apply(object @event)` **NO abstracto, cuerpo vacío** (no hace nada). En el aggregate concreto se definen **overloads** por tipo de evento.

## El patrón de overloads + `dynamic`
```csharp
public abstract class AggregateRoot
{
    public void Apply(object @event) { }   // fallback vacío
    // (dispatch) => ((dynamic)@event) resuelve al overload más cercano
}

public class Box : AggregateRoot
{
    public BoxCapacity Capacity { get; private set; }      // private set; puede ser null al inicio
    public ShippingLabel ShippingLabel { get; private set; }

    public void Apply(BoxCreated @event)        => Capacity = @event.Capacity;
    public void Apply(ShippingLabelAdded @event)=> ShippingLabel = @event.Label;
}
```
- **Truco C# poco conocido:** al castear el evento a **`dynamic`**, en runtime se resuelve al **overload más cercano** al tipo real → no cae en el `Apply(object)` vacío, sino en `Apply(BoxCreated)`, etc. El `Apply(object)` vacío queda como fallback para eventos sin overload.
- Basta con crear un overload por cada evento y la root entity queda funcional.

## ⚠️ Gotcha crítico (ya vivido en el workshop propio, 15/jul)
- `dynamic` **respeta la accesibilidad desde el sitio de la llamada**. Como el dispatch (`((dynamic)this).Apply(...)`) vive en la clase base, **los overloads `Apply(...)` deben ser `public`** (no `protected`/`private`), o revienta con `RuntimeBinderException: ... inaccessible due to its protection level`.
- Contraste: Marten usa reflexión y sí accede a miembros no públicos.

## Diseño clave
- La root entity **solo tiene métodos `Apply` + datos** — **NADA de lógica de negocio**. Toda la lógica para manejar comandos vive **fuera** (en los command handlers, siguiente lección).
- Alternativa sin `dynamic`: `Apply` abstract + dispatch manual por tipo → todo strongly typed. También válido; Boyne usa dynamic por menos trabajo.
