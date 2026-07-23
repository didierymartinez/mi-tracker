--- NOTEBOOKLM SYNC ---
Notebook destino: 🧠 Mi Biblioteca de Conocimiento & Crónicas (ID: 6b703266-4050-4357-b010-ae7076119e5f)
Título de la fuente: Event Sourcing (Dometrain) — Ports & Adapters / Command Side Recap (Full Transcript)
Acción: crear fuente nueva
---
CONTENIDO:
# Event Sourcing en .NET (Dometrain) — Intro capítulo: Ports & Adapters (command side)

## Qué se construyó
- Un **dominio event-sourced** que representa toda la funcionalidad descubierta en el event storm: todos los **commands**, todos los **events**, verificado con **tests**.
- El dominio es **agnóstico** a la aplicación en que corre — como en **Clean Architecture / Ports & Adapters (hexagonal)**.

## Los dos puertos (ports) ya definidos
- **Driving port** (primario / de entrada): el **command router** — acepta todos los commands a procesar.
- **Driven port** (secundario / de salida): la **interfaz `IEventStore`** — define qué espera el dominio del store que persiste los eventos en una base de datos.

## Qué falta para tener una app real (roadmap del capítulo)
1. **Implementar el event store**: una implementación de `IEventStore` que habla con **SQL Server** (una tabla en SQL). Al implementar la interfaz, el store queda listo.
2. **Poner algo al frente** del dominio: puede ser lo que sea (app driven grande, desktop, etc.). Boyne encapsula el dominio en una **API** (porque la mayoría hace web hoy). La API **consume el command router** (como haría cualquier otra app).
3. La **API es la startup application** → debe **registrar el event store en su contenedor de DI**.

## Resultado
- Con esto corre el **command side completo** de CQRS. Después falta el **query/read side** (projections).
- Aun con solo el command side ya se puede usar mucha de la funcionalidad de negocio del dominio.

## Conexión clave (Ports & Adapters ↔ testing)
- El `IEventStore` es un **driven port**; sus implementaciones son **adapters**: el **TestStore** (in-memory, para tests) y el **SQL event store** (producción) son **dos adapters del mismo puerto**.
- Por eso los tests fueron tan limpios: se intercambia el adapter sin tocar el dominio (inversión de dependencias).
