--- NOTEBOOKLM SYNC ---
Notebook destino: 🧠 Mi Biblioteca de Conocimiento & Crónicas (ID: 6b703266-4050-4357-b010-ae7076119e5f)
Título de la fuente: Event Sourcing (Dometrain) — Exercise: Box Domain (Full Spec)
Acción: crear fuente nueva
---
CONTENIDO:
# Event Sourcing en .NET (Dometrain) — "An exercise for you to make"

## Contexto
- El dominio queda "fully functioning" pero con **un solo command handler** implementado (`AddShippingLabel`). El resto es **ejercicio**.
- Trabajar bajo la carpeta `Boxes`: añadir **4 commands + command handlers** más y reorganizar el `Box.cs` (que aún tiene varias clases juntas) en archivos por concepto.

## Spec del dominio Box (Miro board)
| Command | Éxito | Fallo | Estilo de fallo |
|---------|-------|-------|-----------------|
| **Create box** | `BoxCreated` (tamaño correcto vía value type `BoxCapacity`) | — nunca falla | N/A |
| **Add shipping label** | `ShippingLabelAdded` | `ShippingLabelFailedToAdd` (label inválido) | **evento** |
| **Add beer bottle** | `BeerAdded` | box llena → **excepción** | **excepción** |
| **Close box** | `BoxClosed` | box aún vacía → **excepción** | **excepción** |
| **Ship box** | `BoxShipped` (si está al menos cerrada **y** con label) | **dos razones distintas** de fallo | (a decidir) |

## Observación de diseño (importante)
- El curso **mezcla dos estilos de fallo**: unos como **evento** (`ShippingLabelFailedToAdd`, persistido en la historia) y otros como **excepción** (box llena, box vacía — NO quedan en el stream).
- Conecta con la lección "Introduction to our domain": modelar el fallo como **evento** lo deja auditable/reproducible; como **excepción** no queda en la historia. Decisión de diseño según si el fallo es parte del relato del negocio o solo una guardia técnica.

## Tarea
- Implementar los 4 handlers restantes siguiendo el patrón: `GetStream<Box>(command.BoxId).GetEntity()` (replay) → validar contra el estado → `Append` del evento de éxito o fallo → (guardado lo hace el router).
- Añadir los `Apply(...)` correspondientes en la root entity `Box` para cada evento nuevo.
- Posiblemente nuevos value types (p. ej. para la cerveza / capacidad usada).
