--- NOTEBOOKLM SYNC ---
Notebook destino: 🧠 Mi Biblioteca de Conocimiento & Crónicas (ID: 6b703266-4050-4357-b010-ae7076119e5f)
Título de la fuente: Event Sourcing (Dometrain) — Commands vs Events (Full Transcript)
Acción: crear fuente nueva
---
CONTENIDO:
# Event Sourcing en .NET (Dometrain) — "Commands vs Events"

## Contexto
Comandos y eventos aparecen en todos lados (sistemas de bus, CQRS, Event Sourcing) y significan más o menos lo mismo. Definiciones que usa el curso:

## Comando
- Es una **petición** para que el sistema ejecute una operación. El origen no importa (otro sistema, un usuario, una cola…).
- Se modela en **tiempo imperativo** (`CrearCliente`) — no obligatorio, pero expresa la intención con claridad.
- Tiene **un solo procesador lógico**: un endpoint/cola/lugar único donde se recoge. Se centraliza la lógica de procesarlo en un sitio (puede ser una instancia load-balanced, por eso "lógico"). Todo servicio que quiera emitir el comando sabe a dónde mandarlo.

## Evento
- **Describe el pasado**: algo que ya ocurrió y por tanto **no se puede cambiar** (inmutable).
- Se modela en **tiempo pasado** (`ClienteCreado`) — hace evidente al leer el código que ya sucedió.
- No se envía a un lugar específico a procesarse: muchos componentes pueden reaccionar → mecanismo **publish/subscribe**. **Un solo publicador lógico**; cada suscriptor sabe cómo escucharlo.

## Resumen
Comandos = peticiones para que el sistema las ejecute. Eventos = describen el resultado de lógica ya ejecutada (en el pasado).
