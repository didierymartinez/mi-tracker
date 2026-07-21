--- NOTEBOOKLM SYNC ---
Notebook destino: 🧠 Mi Biblioteca de Conocimiento & Crónicas (ID: 6b703266-4050-4357-b010-ae7076119e5f)
Título de la fuente: Event Sourcing (Dometrain) — The relationship with DDD (Full Transcript)
Acción: crear fuente nueva
---
CONTENIDO:
# Event Sourcing en .NET (Dometrain) — "The relationship with DDD"

## Tesis
Event Sourcing ≠ DDD. **ES es solo un patrón de almacenamiento**; **DDD** es un conjunto de herramientas/técnicas para construir software de negocio. Se citan juntos porque encajan muy bien.

## Por qué encajan
- DDD busca **quitar fricción y malinterpretación** entre negocio y código.
- El negocio ya razona en **acciones y reacciones**: "si tomo esta acción y ya ocurrió X, Y, Z → la reacción debe ser esta". Acción = **comando**; reacciones = **eventos**. La lógica valida contra el **estado**, que a su vez se deriva de eventos pasados.
- Los programadores fuimos entrenados a pensar en **estado**; el negocio piensa en **eventos** — ES cierra esa brecha.

## Event Storming
Técnica que la comunidad DDD adoptó: mapear procesos de negocio con **post-its de colores** (comandos, eventos, lógica, view models) en el lenguaje del negocio. Luego se traducen **1-a-1** esos comandos/eventos a clases `Command`/`Event` en el código → menos fricción y menos room para malinterpretar.

## Acciones compensatorias (clave)
Los eventos son inmutables: el pasado no se edita. Ejemplo de la **factura**: si el número es incorrecto, no envías una factura corregida (en Bélgica sería ilegal = manipular números); envías una **nota de crédito** (factura compensatoria). Patrón general: pasa algo → acción → si el resultado no es el deseado → **acción compensatoria** → y así. Influimos un **stream de eventos**; el **estado es un subproducto**.

## Conexión
Comando vs evento + lenguaje ubicuo; las acciones compensatorias son la base de las **Sagas / procesos largos**.
