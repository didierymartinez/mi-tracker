# 💬 Conversación clave: Consumo de eventos, ACL, Envelope y procesamiento

> Conversación técnica del proceso Cosmos (transcrita de audio, decodificada el 04/06/2026). Detonó conceptos fundamentales que se integraron al workshop de Event Sourcing. Origen: discusión de diseño sobre cómo consumir eventos correctamente entre Bounded Contexts.

## Pregunta detonante
*¿Cuándo manejar un evento entrante directamente con un Event Handler y cuándo traducirlo a un comando interno?* — abrió cinco discusiones de arquitectura.

## Los 5 conceptos (decodificados)

1. **Anti-Corruption Layer (ACL): evento público → comando interno.**
   Manejar directo un evento público acopla tu dominio a la firma de otro equipo. Para **públicos** → ACL que valida y traduce a comando interno. Para **privados** (controlas las firmas, mismo equipo) → handler directo está bien.

2. **No procesar eventos "en memoria".**
   Un evento privado sin canal de salida se procesa in-process. Peligroso: Wolverine resuelve el contexto desde el HTTP request o desde el **envelope de la cola**; en memoria no hay envelope → **TenantId DEFAULT** → revienta o cruza tenants. Versión técnica de una regla filosófica.

3. **Una transacción = un agregado (con matices).**
   Si un handler toca >1 agregado, normalmente "hace demasiado" → partir en varios comandos. Excepción real: `Extracto`/`Movimientos` (jerarquía de agregados). Riesgo de 2 commits: el primero pasa, el segundo falla → **pérdida de datos sin reproceso**. Defensa: **Outbox/Inbox** (reproceso durable al reiniciar).

4. **Patrón Envelope: el contexto viaja aparte del mensaje.**
   Mensaje = **payload** (datos de negocio) + **envelope/sobre** (tenant, usuario, remitente, correlación). No meter tenant/usuario en el payload; van en los headers del envelope. `InvokeForTenantAsync` firma el sobre. Crece con mesura.

5. **`FetchForWriting` recomendado.**
   Hace lo mismo que rehidratar pero devuelve un envoltorio con `AppendOne`/`AppendMany`, optimizado para guardado más rápido. Es la cáscara imperativa que mantiene puro el `decide` (núcleo funcional).

## Qué produjo en el workshop
- **§24 — Anti-Corruption Layer** (nueva): evento público → comando interno, con arco de código.
- **§25 — Envelope y contexto** (nueva): payload vs sobre, multi-tenancy, por qué no en memoria.
- Reforzó: §14 (domain vs integration, Outbox/Inbox, DLQ), §18 (un agregado/transacción, sagas, FetchForWriting), §10 (TenantId DEFAULT).

Ruta: `~/Documents/Sincosoft/Cosmos/EventSourcing/eventsourcing-workshops-basics/secciones/`
