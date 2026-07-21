--- NOTEBOOKLM SYNC ---
Notebook destino: 🧠 Mi Biblioteca de Conocimiento & Crónicas (ID: 6b703266-4050-4357-b010-ae7076119e5f)
Título de la fuente: Event Sourcing (Dometrain) — Introduction to our Domain (BeerSender) (Full Transcript)
Acción: crear fuente nueva
---
CONTENIDO:
# Event Sourcing en .NET (Dometrain) — "Introduction to our Domain"

## La historia (dominio real)
- 2022, tercer lockdown en Bélgica. Boyne decide enviar cajas de cerveza belga a sus amigos para animarse (~200 botellas, cervecerías locales + clásicos difíciles de exportar).
- Botellas de vidrio → transporte delicado. Cajas de "Beer for Nature" (microcervecería que usa ganancias para comprar y abrir bosques al público) que aguantan hasta 24 botellas.
- **UPS bloqueó los envíos**: consideró que no tenía licencia para exportar alcohol fuera de Schengen (Noruega, Suiza, UK) → devolvió las cajas. La oficina de aduanas real decía que estaba en su derecho, pero fue la aduana **de UPS** la que bloqueó.
- Reenvío con **PostNL** (menos estrictos) → entregó casi todas al primer intento; una caja rebotó sin intento de entrega y se reenvió; la última llegó en junio.
- Moraleja: enviar cervezas es divertido; el seguimiento posterior no. → construir la app **para la parte divertida**: llenar y rastrear cajas hasta enviarlas.

## La aplicación: BeerSender
- Se llama **BeerSender** (no "BeerSender.net"). *Pet peeve* de Boyne: no metas ".net" en el nombre — a los usuarios no les importa con qué se construyó; .NET ya tiene 20+ años.

## Primer aggregate: Box (event storming)
- Post-its azules = **commands**. Comandos del aggregate **Box**:
  1. **Create box** — inicia un envío; caja con 6, 12 o 24 espacios.
  2. **Add beer bottles** — agrega botellas a la caja.
  3. **Apply shipping label** — pega la etiqueta de envío.
  4. **Close box** — cierra la caja.
  5. **Ship box** — intenta enviarla.
- **Algunas operaciones pueden fallar** según el **estado del aggregate** → un mismo command puede producir **eventos distintos** dependiendo del estado (éxito vs fallo).
- Complejidad suficiente para las primeras features del curso.
