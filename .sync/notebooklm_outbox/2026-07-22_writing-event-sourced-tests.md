--- NOTEBOOKLM SYNC ---
Notebook destino: 🧠 Mi Biblioteca de Conocimiento & Crónicas (ID: 6b703266-4050-4357-b010-ae7076119e5f)
Título de la fuente: Event Sourcing (Dometrain) — Writing Event Sourced Tests (Full Transcript)
Acción: crear fuente nueva
---
CONTENIDO:
# Event Sourcing en .NET (Dometrain) — "Writing Event Sourced Tests"

## Organización
- Los tests se agrupan **igual que los aggregates** (carpeta por aggregate → box tests).

## Primer test: AddBeerHandlerTest
- Hereda de la **base test** para command handlers e implementa la **propiedad abstracta `Handler`**:
  ```csharp
  protected override CommandHandler<AddBeer> Handler => new AddBeerBottleHandler(EventStore);
  ```
  (el `EventStore` viene de la clase base). A partir de ahí, es como escribir unit tests normales.

## Patrón Given / When / Then (xUnit, [Fact])
```csharp
[Fact]
public void IfBoxIsEmpty_ThenBottleShouldBeAdded()
{
    // Given — eventos que ya ocurrieron
    Given(new BoxCreated(new BoxCapacity(6)));   // caja creada, vacía

    // When — encolar el command (usa el aggregate ID ambiente)
    When(new AddBeer(BoxId, wolfCartLaunch));    // una cerveza belga

    // Then — eventos nuevos esperados
    Then(new BeerBottleAdded(new BeerBottle(...)));  // NUEVA instancia de la cerveza
}
```
- El nombre del test: cualquier convención sirve.
- `[Fact]` porque usa **xUnit**.

## Detalle clave: instancia NUEVA en el esperado
- En el evento esperado se crea una **instancia nueva** de la cerveza (no se reusa la misma referencia que se pasó al command).
- Propósito: probar que la comparación es por **contenido/valor (deep, `BeEquivalentTo`)** y **no por referencia**. Si pasara la misma referencia, el test podría "pasar" sin validar realmente el contenido.

## Prueba de que no hace trampa
- Corre el test → verde. Para demostrar que sí asserta: cambia algo **profundo en el object graph** (ej. agrega un "." al nombre) → el test **falla** → confirma que la comparación profunda realmente verifica el contenido.

## Patrón universal
- Todos los tests siguen lo mismo: **eventos pasados (Given) → encolar command (When) → esperar eventos nuevos (Then)**.
- Puede haber **múltiples eventos** en el given y en el expected. Con esto puedes unit-testear todo el dominio.
