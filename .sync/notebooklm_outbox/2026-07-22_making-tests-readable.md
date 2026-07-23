--- NOTEBOOKLM SYNC ---
Notebook destino: 🧠 Mi Biblioteca de Conocimiento & Crónicas (ID: 6b703266-4050-4357-b010-ae7076119e5f)
Título de la fuente: Event Sourcing (Dometrain) — Making Our Tests More Readable (Full Transcript)
Acción: crear fuente nueva
---
CONTENIDO:
# Event Sourcing en .NET (Dometrain) — "Making Our Tests More Readable"

## Objetivo
- Tests fáciles de **mantener, extender y LEER** — idealmente legibles hasta para un **product owner** (rol funcional, no técnico). El test crudo con `new new BoxCreated(new BoxCapacity(6))` no es legible.

## Clase base intermedia por aggregate
- Se introduce una base class **entre** el test concreto y `CommandHandlerTest`: una abstract class para **todos los tests de handlers del mismo aggregate** (ej. `BoxTest`). Genérica en `TCommand`, **sin constraints** (igual que la base del command handler).
- Sandwich: `CommandHandlerTest<T>` → **`BoxTest<T>`** → `AddBeerHandlerTest`.
- **Alias del aggregate ID ambiente:** `protected Guid BoxId => AggregateId;` → más significativo en los tests.

## Builders de eventos (en la base compartida del aggregate)
- Los eventos se reutilizan entre tests y entre commands → se escriben una sola vez aquí.
```csharp
protected BoxCreated BoxCreatedWithCapacity(int capacity)
    => new BoxCreated(new BoxCapacity(capacity));
protected BeerBottleAdded BeerBottleAdded(BeerBottle bottle)
    => new BeerBottleAdded(bottle);
```
- Uso legible: `BoxCreatedWithCapacity(6)`.

## Builder del command (al fondo del test concreto, NO en la base)
- El command solo se usa en su propio test → va al final de esa clase:
```csharp
private AddBeer AsBeerBottle(BeerBottle bottle) => new AddBeerBottle(BoxId, bottle);
```
- Usa el `BoxId` ambiente.

## Test data (variables con nombre, en la base compartida)
```csharp
protected BeerBottle CartBlanche = /* esa cerveza belga */;
```

## Resultado: test casi en lenguaje natural
```csharp
Given(BoxCreatedWithCapacity(6));
When(AsBeerBottle(CartBlanche));
Then(BeerBottleAdded(CartBlanche));
```
> "Dado que la caja se creó con capacidad seis, cuando agregamos una botella llamada Cart Blanche, entonces obtenemos un evento beer-bottle-added Cart Blanche."

## Por qué es genial
- Escribir tests es **predecible**: nuevos tipos de evento → unas funciones builder + la función del command → listo.
- **Sin SpecFlow, sin Cucumber, sin mocking framework** — solo unit tests muy legibles que calzan bien con el dominio.
- Regla de ubicación: **eventos + test data compartidos** en la base del aggregate (reusados por varios handlers); **command builder** en el test concreto (solo se usa ahí).
