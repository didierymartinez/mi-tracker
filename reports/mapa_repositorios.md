# 🗺️ Mapa de Repositorios — Sincosoft / Cosmos

> Inventario de tu workspace `~/Documents/Sincosoft/Cosmos` (revisado 03 jun 2026). Objetivo: tener una vista única para saber qué hay, qué está activo y dónde poner el foco — en vez de "abrir una cosa y otra". Las fechas son del último commit local.

---

## 🪐 Cosmos — el producto ERP (org `Cosmos-SincoERP`)

### Infraestructura y plataforma (lo transversal)
| Repo | Último commit | Qué es |
|------|--------------|--------|
| **ApplicationPlane** | 2026-05-05 | Plano de aplicación: IaC + workflows reusables (`_reusable-deploy-swarm.yml`) |
| **ControlPlane** | 2026-05-15 | Plano de control: gestión de tenants (Functions + Service Bus) |
| **architecture** | 2026-05-06 | ADRs, vistas de arquitectura, glosario (la fuente de verdad del diseño) |
| **Cosmos.BuildingBlocks** | 2026-05-06 | Bloques base compartidos (CritterStack, EDA) |
| **Cosmos.CrossCuttingConcerns** | 2026-05-04 | Preocupaciones transversales (logging, seguridad, etc.) |
| Plataforma · erp-definiciones · diseno-modelo | 2026-01/05 | Definiciones de producto y modelo del ERP |

### 🔴 Bounded Context activo: **ObligacionesPorPagar (OxP)** — Cuentas por Pagar
*El más activo del momento (build-out en mayo).*
| Repo | Último commit | Servicio |
|------|--------------|----------|
| ObligacionesPorPagar.Entradas | 2026-05-15 | Ingreso de documentos |
| ObligacionesPorPagar.Radicacion | 2026-05-15 | Radicación |
| ObligacionesPorPagar.Reconocimiento | 2026-05-15 | Reconocimiento (OCR/datos) |
| ObligacionesPorPagar.ConciliacionInteligente | 2026-03-16 | Conciliación |
| ObligacionesPorPagar.Notificaciones | 2026-05-15 | Notificaciones |
| ObligacionesPorPagar.Gateway | 2026-05-15 | Gateway del BC (YARP) |
| ObligacionesPorPagar.Infraestructura | 2026-05-19 | IaC del BC |
| ObligacionesPorPagar.ControlPlane | 2026-03-16 | Control plane del BC (tu análisis en dev_didier) |
| ObligacionesPorPagar.Front | 2026-05-06 | Frontend |
| Analisis.ObligacionesPorPagar · .POC.ContainerApps · ObligacionesPorPagar (base) | varios | Análisis / POCs / legacy |

### Otros Bounded Contexts
| Repo | Último commit | BC |
|------|--------------|-----|
| Cosmos.Contabilidad (+ .Front) | 2026-05-19 | Contabilidad |
| Cosmos.Impuestos (+ .Front) | 2026-05-15 | Impuestos |
| Cosmos.Terceros · ThirdParties | 2026-01 | Terceros |
| Cosmos.Asistente | — | Asistente |
| OCR · Entradas · SistemaRespiratorio | 2025-12/2026-02 | Servicios de apoyo |

### Legacy / migración
CuentasPorPagar · ObligacionesPorPagar (base) — origen que se está migrando al modelo nuevo.

### Docs, bootcamps y plantillas
architecture · blog-ingenieria (2026-03-25) · Bootcamp · Bootcamp_Battleship · CuentasPorPagar_Bootcamp · template-progressive-tdd-katas · demo-repository · Bletchley · **cosmos-dev-to-prod-workshop** (tu workshop, ya consolidado) · cosmos-reconstruction-guide

---

## 🎓 Aprendizaje personal (Labs / TDD / workshops)
| Repo | Último commit | Qué es |
|------|--------------|--------|
| **EventSourcing/eventsourcing-workshops-basics** | 2026-04-12 | Tu workshop Marten/ES (consolidado en reports/) |
| EventSourcing/VendingMachine · workshop | — | Ejercicios ES adicionales |
| katas-tdd-didierymartinez | 2025-12-15 | Tu viaje TDD (katas progresivos) |
| katas-tdd-jaimeforerog | 2025-12-05 | Katas TDD (de Jaime, referencia) |
| katas | — | Katas varios |
| level-up | 2025-12-16 | LearningPlatform — TDD Kata Management (Azure DevOps Labs) |
| rag-workshop | 2025-10-27 | Clasificador visual de embeddings / RAG (Labs) |
| cosmos-route | 2025-12-10 | Análisis del proyecto Labs (Azure DevOps) |
| Terraform | — | Práctica de Terraform |

## 🧑‍💻 Proyectos propios
| Repo | Último commit | Qué es |
|------|--------------|--------|
| smart-budget-app | 2026-03-08 | App React + Vite (presupuesto) — tuyo |
| fit-tracker | — | (vacío / por arrancar) |

---

## 🎯 Lectura para el foco
- **Trabajo real activo:** el BC de **ObligacionesPorPagar** (toca casi todo en mayo) + Contabilidad e Impuestos arrancando. Si vas a aportar, ahí está el movimiento.
- **Transversal que conviene dominar:** `architecture` (ADRs), `ApplicationPlane` y `ControlPlane` — es justo lo que cubre tu workshop dev-to-prod.
- **Aprendizaje que alimenta el trabajo:** `eventsourcing-workshops-basics` → conecta directo con CritterStack/Wolverine de Cosmos.
- **Dispersión a vigilar:** muchos repos de bootcamp/katas/labs dormidos. No son deuda; solo decide conscientemente cuáles retomar y cuáles archivar mentalmente (anti-procrastinación).

> Repos en Azure DevOps (Labs): cosmos-route, level-up, rag-workshop. El resto en GitHub `Cosmos-SincoERP`.
