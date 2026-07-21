--- NOTEBOOKLM SYNC ---
Notebook destino: 🧠 Mi Biblioteca de Conocimiento & Crónicas (ID: 6b703266-4050-4357-b010-ae7076119e5f)
Título de la fuente: Event Sourcing (Dometrain) — What is Event Sourcing? (Full Transcript)
Acción: crear fuente nueva
---
CONTENIDO:
# Event Sourcing en .NET (Dometrain) — "What is Event Sourcing?"

## Definición
Event Sourcing es **meramente un patrón de almacenamiento**: guardas las **transiciones de estado (eventos)** en vez del **estado actual**.

## El flujo
Un **comando** entra → corre lógica → interactúa con el estado de una entidad. El primer comando crea la entidad (V1); comandos siguientes la transicionan (V2, V3…).

## Estado vs eventos
- **Normalizado (tradicional):** persistes V1 en filas; al llegar V2 **actualizas** esas filas (V1 deja de existir). Se pierde la historia. Herencia de cuando el **almacenamiento era carísimo** (guardar cada dato una sola vez).
- **Hoy:** en la nube, **compute y memoria cuestan más que el storage** → el sacrificio de perder historia ya no compensa.
- **Event Sourcing:** modelas cada cambio como **evento** (event 1, 2, 3…). Reproduciendo (**replay**) los eventos reconstruyes el estado en cualquier punto del tiempo. Para correr lógica: replay hasta V3 → actúas → generas nuevos eventos.
- **Asimetría:** de eventos → estado siempre; de estado → eventos **nunca** (la historia ya se perdió).

## Ventaja para features nuevos
Si construyes funcionalidad nueva y conservas todos los eventos, la info para poblar las tablas nuevas **está en el historial**. Con tablas normalizadas esa info pudo **perderse** → poblar sería **adivinar (guesswork)**. ES no pierde lo relevante.

## Ubicación
ES **no** es DDD ni CQRS; encaja muy bien con ambos pero es, por sí solo, un patrón de almacenamiento de las transiciones de estado.
