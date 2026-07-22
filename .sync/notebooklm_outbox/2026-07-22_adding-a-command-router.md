--- NOTEBOOKLM SYNC ---
Notebook destino: 🧠 Mi Biblioteca de Conocimiento & Crónicas (ID: 6b703266-4050-4357-b010-ae7076119e5f)
Título de la fuente: Event Sourcing (Dometrain) — Adding a Command Router (Full Transcript)
Acción: crear fuente nueva
---
CONTENIDO:
# Event Sourcing en .NET (Dometrain) — "Adding a Command Router"

## Por qué existe el router (y por qué SaveChanges no va en el handler)
- Podrías registrar handlers + `IEventStore` en DI y llamarlos directo; lo único que faltaría es `eventStore.SaveChanges()` al final.
- No se pone `SaveChanges` en el handler por **composabilidad**: un command complejo se puede **descomponer en varios commands pequeños**, ejecutar sus handlers, y hacer **un solo `SaveChanges` al final**. Con `SaveChanges` dentro de cada handler eso se vuelve muy difícil.
- **Command Router** = orquestador que: **localiza el handler correcto → lo ejecuta → llama `SaveChanges()` al final.**
- Si te sirve tener `SaveChanges` al final de los handlers, no necesitas router. Boyne casi siempre lo usa.

## Anatomía
- El router necesita el `IEventStore` **del mismo scope** que el router, para que `SaveChanges()` opere sobre la **misma instancia** del store que usó el handler.
- Método público `Handle(object command)`. El tipo real del command **solo se conoce en runtime**.

## Enfoque 1 — switch (hard-coded)
```csharp
public void Handle(object command)
{
    switch (command)
    {
        case AddShippingLabel addLabelCommand:
            var handler = new AddShippingLabelHandler(eventStore);
            handler.Handle(addLabelCommand);
            break;
        default:
            throw new Exception($"No handler for {command.GetType()}");
    }
    eventStore.SaveChanges();
}
```
- Funciona, pero la clase se vuelve **muy larga** y, sobre todo, si un handler necesita **dependencias extra** (ej. un proxy para un servicio externo), tendrías que inyectarlas también en el router → constructor y clase se vuelven un desastre.

## Enfoque 2 — reflection + DI container (el que usa Boyne)
- Inyecta también el **`IServiceProvider`** (resuelve en el **mismo scope** → misma instancia de `IEventStore`).
- Como el tipo del command es de runtime, ~5 líneas de reflexión:
  1. `var commandType = command.GetType();`
  2. Construir el tipo genérico `CommandHandler<commandType>`.
  3. `serviceProvider.GetService(handlerType)` → el handler real (con **sus propias deps** resueltas por DI).
  4. Reflexión para obtener el `MethodInfo` de `Handle` e **invocarlo** con el command como único parámetro.
  5. `eventStore.SaveChanges()`.
- "Cinco líneas de reflexión, nada sofisticado", pero resuelve el problema de dependencias sin ensuciar el router.

## Resultado
- El **command router es el único punto de interacción con el dominio**: lo enchufas a APIs o donde sea.
- Registra el event store + todos los command handlers → le lanzas commands y todo funciona.
