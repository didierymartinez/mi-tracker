--- NOTEBOOKLM SYNC ---
Notebook destino: 🧠 Mi Biblioteca de Conocimiento & Crónicas (ID: 6b703266-4050-4357-b010-ae7076119e5f)
Título de la fuente: Event Sourcing (Dometrain) — Value Types in Commands & Events (Full Transcript)
Acción: crear fuente nueva
---
CONTENIDO:
# Event Sourcing en .NET (Dometrain) — "Value Types in Commands & Events"

## Qué es un value type (DDD)
- Agrupar campos que **siempre se usan juntos** en una clase representada por sus valores. Ej: `ShippingLabel` = `Carrier` + `TrackingCode` (se validan/usan en tándem).
- Ventaja: la **lógica asociada viaja con los datos** (ej. `IsTrackingCodeValid(carrier, code)`), y al meter el value type en el evento heredas esa lógica "out of the box".
- Se implementan como **`record`** para tener **igualdad por valor**.

## Los dos enfoques (y su impacto en Event Sourcing)
### Strict value types
- **Lanzan excepción en el constructor** si los valores son inválidos → *imposible representar estado inválido*.
- Problema en ES: si el value type vive en un **evento** y deserializas algo que **antes era válido y ya no** → la excepción **impide deserializar el evento**. Un breaking change en un value type strict usado en eventos = **breaking change de todo el sistema**.
- Consecuencia: ante un cambio que rompe, hay que **versionar el value type** (copia con la lógica nueva) **y versionar los eventos** (los viejos usan lógica vieja y deserializan; los nuevos usan la nueva).

### Loose value types
- **No lanzan**; permiten construir estado inválido, pero exponen **métodos de validación** (ej. `IsValid`) para chequear cuando quieras.
- Ventaja: **siempre deserializan** → red de seguridad. Puedes **añadir lógica nueva sin versionar** y seguir usándolos en eventos.
- Sacrificio: es posible representar estado inválido.

## Efectos por ubicación
| | Commands | Events |
|---|----------|--------|
| **Strict** | Excepción **antes del dominio** → el dominio nunca ve un command inválido (hay que manejarla antes) | Breaking change → no deserializa eventos viejos → obliga a **versionar** value type + eventos |
| **Loose** | El dominio **sí** puede recibir/validar lo inválido → flexibilidad para responder | **Siempre deserializa** → añadir lógica sin versionar |

## Estrategias válidas (todas legítimas)
1. **Loose en todos lados** (la que elige Boyne — red de seguridad al deserializar).
2. **Strict en todos lados** + versionar cuando toque.
3. **Evitar value types en commands/events** (usar primitivos ahí) y usar value types **solo en el domain code**.

## Segundo ejemplo: `BoxCapacity`
- Caja solo admite **6, 12 o 24** espacios. `record BoxCapacity(int Spots)`.
- **Factory method estático** que mapea un "desired number of spots" al tamaño correcto: ≤6→6, ≤12→12, resto→24 (podría lanzar si >24).
- Agrupa la lógica de negocio con el dato; los factory methods ayudan a **no crear estado inválido**, y aun así el evento **deserializa** aunque venga un número fuera de la lista.
- Ojo: en el command se maneja como *desired number of spots* (input), y en el evento `BoxCreated` se usa ya como `BoxCapacity capacity`.
