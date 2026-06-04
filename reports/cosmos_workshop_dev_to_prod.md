# 🪐 Cosmos: De Dev a Producción — Consolidación del Workshop

> Documento de aprendizaje que consolida el workshop **`cosmos-dev-to-prod-workshop`** (diseñado 8-12 May 2026). Une la *narrativa de autor*, la referencia de arquitectura y los 10 labs en una sola guía para retomar y revivir el conocimiento. Fuente viva: `~/Documents/Sincosoft/Cosmos/cosmos/cosmos-dev-to-prod-workshop/`. Conversación de origen archivada en [`../historial/2026-05-08_workshop-cosmos-dev-to-prod.md`](../historial/2026-05-08_workshop-cosmos-dev-to-prod.md).

---

## 🎯 Propósito

Que cualquier desarrollador de Cosmos pueda **leer, entender y modificar cualquier módulo Terraform de producción**, y enfrentar un requerimiento nuevo del Application Plane o del Control Plane sabiendo exactamente dónde tocar y por qué. El taller construye, paso a paso, desde una suscripción de Azure vacía hasta una plataforma SaaS multitenant, seguro e inmutable.

## ❓ Las 9 preguntas que debes poder responder al terminar

1. ¿Por qué hay **dos Service Buses**? (Lab 8 — ADR-009)
2. ¿Por qué **YARP** y no Azure Application Gateway? (Lab 7 — ADR-003)
3. ¿Por qué la VM tiene **SystemAssigned Identity**? (Lab 4)
4. ¿Cómo el Runner conoce el **PAT sin tenerlo en código**? (Lab 4 — cloud-init + Key Vault)
5. ¿Por qué el NSG solo permite `AzureFrontDoor.Backend:80`? (Lab 7 — ADR-007)
6. ¿Cómo el frontend recibe la URL de la API **sin recompilarse**? (Lab 6 — env.js desde KV)
7. ¿Qué hace **Docker Swarm** y por qué no contenedores directos? (Lab 3)
8. ¿Qué pasa cuando un **nuevo tenant** se registra? (Lab 8 — Control Plane)
9. ¿Por qué **no puedo conectarme a la base de datos** desde mi casa? (Lab 9 — Hardening)

---

## 🧭 La historia en cuatro fases

El hilo conductor: empezamos en el vacío y el reto inicial no es técnico sino de **orden, seguridad y escala**. Cada lab resuelve un problema concreto que el anterior dejó abierto.

### Fase 1 — Los cimientos (red privada + contenedor)

| Lab | Construyes | Concepto clave | Problema que resuelve |
|---|---|---|---|
| **1 · Foundation** | Resource Group (CAF) + VNet + Subnets | Segmentación de red y aislamiento por Bounded Context | Evitar un laberinto de recursos sin nombre ni dueño. El RG es el límite de seguridad/costos; cada subred aísla un BC. (ADR-001) |
| **2 · Basic Compute** | VM Linux + Docker | El contenedor como unidad de despliegue | Correr apps sin "ensuciar" el SO con dependencias manuales. Reto real: el SKU de IP pública debe ser **Standard**, no Basic (límite de cuota). |

### Fase 2 — Orquestación y automatización segura

| Lab | Construyes | Concepto clave | Problema que resuelve |
|---|---|---|---|
| **3 · Orchestration** | Docker Swarm + redes Overlay | Service discovery y aislamiento de tráfico | Cosmos vive en varios repos (OXP, Contabilidad, Radicación) → hace falta orquestar múltiples servicios. `oxp-public` expone a internet; `oxp-internal` mantiene la conversación privada entre servicios. |
| **4 · Automation & Identity** | ACR + GitHub Runner + Key Vault + Managed Identity | **Zero Secrets** | La VM descarga imágenes del ACR **sin contraseñas** (Managed Identity + rol `AcrPush`, que incluye `AcrPull`). El Runner corre *dentro* de la VM para que GitHub no entre a la red privada. El **PAT** no se escribe en código: se guarda en Key Vault y `cloud-init` lo lee en el "Día 0". (ADR-004) |

> **El flujo de `cloud-init` (Día 0)**: instala Docker + Azure CLI → login con Managed Identity → obtiene el PAT del Key Vault → registra el Runner en GitHub. Así la máquina queda lista para compilar y publicar imágenes sola.

### Fase 3 — Estado, frontend y perímetro

| Lab | Construyes | Concepto clave | Problema que resuelve |
|---|---|---|---|
| **5 · Persistence** | PostgreSQL Flexible Server (PaaS) | Desacoplamiento de estado | El contenedor es efímero; los datos no pueden vivir en él. La cadena de conexión va al Key Vault y el Runner la inyecta en el despliegue. |
| **6 · Frontend Immutable** | Storage Account (static website) + `env.js` | Configuración de runtime | El frontend sabe a qué API llamar **sin recompilar**: un `env.js` se genera dinámicamente en el deploy con secretos del KV. |
| **7 · Edge Gateway** | Azure Front Door + YARP + NSG | Sello de seguridad perimetral | Front Door es el ingreso global; **YARP** (reverse proxy en la VM) rutea al servicio correcto del Swarm. El NSG sella la VM para que *solo* acepte tráfico de `AzureFrontDoor.Backend`. (ADR-003, ADR-007) |

### Fase 4 — Escalabilidad y hardening

| Lab | Construyes | Concepto clave | Problema que resuelve |
|---|---|---|---|
| **8 · Control Plane** | Service Bus + Function Apps | **App Plane vs Control Plane** | Dos cerebros separados, sin acoplamiento síncrono. (ver abajo) (ADR-009) |
| **9/10 · Hardening** | Private Endpoints + Private DNS | Zero Trust | Se eliminan las IPs públicas de Key Vault y DB; el tráfico viaja por túneles privados dentro de la red de Microsoft. Por eso no te puedes conectar a la DB desde casa. (ADR-007) |

---

## 🧠 La decisión central: los dos planos (ADR-009)

Es la decisión arquitectónica más importante de Cosmos. El sistema tiene **dos cerebros que no se hablan de forma síncrona**:

| | **Application Plane** | **Control Plane** |
|---|---|---|
| Qué hace | Sirve las APIs del ERP en tiempo real | Gestiona el ciclo de vida de tenants y plataforma |
| Compute | VM con Docker Swarm + YARP | Function Apps (onboarding, billing, user-mgmt) |
| Mensajería | Service Bus para eventos entre Bounded Contexts | Service Bus propio para eventos de tenants |
| Datos | Un Postgres por Bounded Context | Postgres dedicado del Control Plane |
| Repo Terraform | `ApplicationPlane/infraestructure/` | `ControlPlane/infraestructura/aplicacion/` |

**La regla de oro:** si el runtime del ERP necesita datos de un tenant (tier, permisos), los lee del **token JWT** — nunca con una llamada HTTP al Control Plane. Esto evita que un bug en billing tumbe el ERP de todos, o que un pico de onboardings ralentice las facturas. Cuando un tenant nuevo se registra, el Control Plane lo procesa de forma **asíncrona por eventos** y avisa al Application Plane por el Service Bus.

Esto responde directamente a las **dos preguntas sobre los Service Buses**: hay dos porque hay dos planos, cada uno con su propio bus para no acoplarse.

---

## 🔁 Transversalidad del CI/CD (el patrón que escala Cosmos)

El secreto de la escalabilidad operativa es **Reusable Workflows** de GitHub Actions (`workflow_call`). Cualquier Bounded Context (p. ej. `Cosmos.Contabilidad/.github/workflows/main-deploy-dev.yml`) **no tiene lógica de build/deploy**: solo "llama" a un workflow maestro del repo `ApplicationPlane` y le pasa variables:

```yaml
deploy:
  uses: Cosmos-SincoERP/ApplicationPlane/.github/workflows/_reusable-deploy-swarm.yml@main
  with:
    stack_name: contabilidad
    acr_name: crcontdeveus2001
    repository_prefix: cont
```

Así, una mejora en el pipeline maestro beneficia a todos los BCs sin tocar cada repo. El Runner se registra en el grupo `swarm-deploy-oxp` y los workflows lo referencian con `runner_group: swarm-deploy-oxp`.

---

## 🗺️ Mapa de ADRs (puente código ↔ decisión)

| Lab | ADR | Decisión |
|---|---|---|
| 1 | ADR-001 | Aislamiento por Bounded Context |
| 2 & 3 | ADR-003 | Contenedores y orquestación ligera (YARP como gateway) |
| 4 | ADR-004 | Managed Identity + Zero Secrets |
| 5, 9 | ADR-007 | Zero Public IP / desacoplamiento de estado |
| 7 | ADR-003 | Ruteo con YARP + Front Door |
| 8 | ADR-009 | Separación Application Plane / Control Plane |

Repo de arquitectura: `github.com/Cosmos-SincoERP/architecture` (ADRs, vistas de red/tráfico/mensajería/identidad, glosario de negocio).

---

## ✅ Cómo usar esto para "vivir del aprendizaje"
- **Repaso rápido:** lee las 9 preguntas y responde de memoria; las que falles, ve al lab correspondiente.
- **Contexto real:** cada concepto del taller existe en el código de Cosmos — abre el repo del BC y busca el patrón.
- **Conversación completa:** si quieres ver el *por qué* de cada decisión y los retos que surgieron al construir el taller, está en el historial archivado.
