# 🗄️ Workshop de Marten + Event Sourcing — Consolidación

> Consolidación de tu workshop práctico **`eventsourcing-workshops-basics`** (repo propio: github.com/didierymartinez/eventsourcing-workshops-basics), complementado con el curso Dometrain "Event Sourcing con Marten & .NET". Lo trabajaste sobre todo el **17-18 mar** (Sección 03, 30+ commits). Conversación de origen archivada en [`../historial/2026-03-02_tracker-maestro-y-workshop-eventsourcing.md`](../historial/2026-03-02_tracker-maestro-y-workshop-eventsourcing.md).

---

## 🎯 La idea central
Aprender Event Sourcing modelando la **biografía de una persona (Jhon)** en lugar de una "foto" de su estado actual: en vez de sobrescribir datos, guardas la **secuencia de hechos** (`PersonaNacida`, `CumpleañosCelebrado`, `HijoNacido`, mudanzas…). Si tienes la lista de hechos, siempre reconstruyes el presente — y además conservas el pasado. Esa es la Fuente de la Verdad en Event Sourcing.

La conexión con tu trabajo real: un `AggregateRoot` en Cosmos (como `TenantOnboarding`) sigue exactamente las mismas reglas que tu `Persona` — métodos `Apply` y `Create`, y proyecciones inline.

---

## 🗺️ Hoja de ruta — 13 secciones en 4 fases

### Fase 1 — El motor puro en memoria
| # | Sección | Concepto |
|---|---------|----------|
| 01 | El diario de Jhon | **Biografía vs Foto** → qué es Event Sourcing |
| 02 | Preparando el lienzo | Setup del proyecto .NET |
| 03 | Vivir el pasado: el motor `Apply` | **Replay**: reconstruir estado aplicando eventos en orden (el corazón del workshop, 17-18 mar) |
| 04 | Refactorizando: el `AggregateRoot` | Extraer la clase base; `Persona` hereda de `AggregateRoot` |
| 05 | El flujo de vida: `EventStream` | Envoltorio lógico del stream de eventos |
| 06 | El almacén en memoria: Event Store | `InMemoryEventStore` (diccionario en RAM) |
| 07 | Decidir el futuro: emitir eventos | El agregado valida reglas y **emite** nuevos eventos |
| 08 | El Command Handler | **El Biógrafo Oficial**: Cargar → Actuar → Guardar. Comandos como `record` inmutables |

### Fase 2 — Transición a infraestructura .NET
| # | Sección | Concepto |
|---|---------|----------|
| 09 | El tiempo de espera | I/O, `async/await` y el riesgo de "olvidar" (no esperar) |
| 10 | El Recepcionista | **Inyección de Dependencias** (DI container) |

### Fase 3 — Persistencia avanzada (producción)
| # | Sección | Concepto |
|---|---------|----------|
| 11 | El archivero incombustible | **Docker + PostgreSQL** + tipo de dato JSONB |
| 12 | El Bibliotecario Experto | **Marten** reemplaza todo el motor hecho a mano |

### Fase 4 — Desacoplamiento
| # | Sección | Concepto |
|---|---------|----------|
| 13 | El correo interno | **Wolverine**: bus de mensajes que enruta comandos a handlers |
| 14 | El compromiso inquebrantable | **Transactional Outbox** *(próximamente)* |

---

## 🔑 El salto clave: de "hecho a mano" a Marten (Sección 12)

Marten convierte PostgreSQL en (1) base documental y (2) Event Store de producción. Reemplaza pieza por pieza lo que construiste a mano:

| Lo que construiste a mano | Lo que Marten te da |
|---|---|
| `EventoAlmacenado` (el sobre) | Marten lo maneja internamente |
| `InMemoryEventStore` (diccionario) | `IDocumentStore` |
| `EventStream<T>.Get()` (rehidratar) | `session.Events.AggregateStreamAsync<T>(id)` |
| `EventStream<T>.Append()` (guardar) | `session.Events.Append(id, events)` |
| `IEventStore` (interfaz) | `IDocumentSession` (súper-interfaz) |

Con **3 cambios quirúrgicos** pasaste de RAM a una DB empresarial: instalar `Marten` (NuGet) → registrarlo en el DI con la connection string (4 líneas, `AutoCreate.All` en dev) → cambiar `IEventStore` por `IDocumentSession` en los handlers. La serialización JSON, transacciones ACID, versionado y tablas las hace Marten solo.

Requisito para `AggregateStreamAsync<Persona>`: la clase debe tener **constructor vacío** y conservar sus métodos `Apply(Evento)` — Marten usa el mismo *dynamic dispatch* que diseñaste a mano. `StartStream` para crear el stream; `Append` para eventos posteriores.

---

## 🐺 Wolverine (Sección 13) — el puente a Cosmos
Wolverine es el bus de mensajes interno (el "cartero"): publicas `IMessageBus.SendAsync(comando)` y él descubre el handler, inyecta dependencias y, al integrarse con Marten, **comparte la misma transacción** (atomicidad) e incorpora **Outbox** nativo. Es exactamente la pieza que usa **Cosmos** (`CritterStack`) — por eso este workshop es tu base directa para entender el ControlPlane.

> **vs MediatR:** rol similar, pero Wolverine añade atomicidad transaccional con Marten y Outbox.

---

## ✅ Cómo repasarlo
- Relee la Sección 01 (biografía vs foto) para anclar el *por qué*.
- Reproduce de memoria el motor `Apply`/Replay (Sección 03) — es lo que se repite en todo Cosmos.
- Conecta cada concepto con `TenantOnboarding` real: mismo `AggregateRoot`, mismos `Apply`, mismas proyecciones.
- Pendiente del workshop: Sección 14 (Outbox) — aún por escribir en tu repo.
