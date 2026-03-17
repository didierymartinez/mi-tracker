# 🪐 Cosmos Platform: Memorias de Diseño y Evolución

> Este documento registra la historia, decisiones arquitectónicas y el progreso del equipo en la construcción del **ControlPlane** de Cosmos.

---

## 🏗️ Hitos de Arquitectura (ADRs Implícitos)

### 1. Mensajería y Event Sourcing: Wolverine + Marten
- **Decisión**: Adopción de **Wolverine** como Mediator/Message Bus y **Marten** para persistencia y Event Sourcing sobre PostgreSQL.
- **Razón**: Simplificar la gestión de comandos y eventos, asegurando que el estado del sistema sea reconstruible a través de logs de eventos.

### 2. Observabilidad: Migración a OpenTelemetry
- **Decisión**: Reemplazar la dependencia directa de Application Insights por **OpenTelemetry** con el exporter de Azure Monitor.
- **Razón**: Estandarización y flexibilidad. Permite activar/desactivar el tracing según variables de entorno y desacopla la aplicación del proveedor específico.

### 3. Estrategia de Mensajería: Forwarding en Service Bus
- **Decisión**: Implementar un patrón de reenvío (Forwarding) desde múltiples temas (`billingTopic`, `provisioningTopic`) hacia una cola central (`onboardingQueue`).
- **Razón**: Centralizar la lógica de negocio en una Function App de Onboarding sin perder el desacoplamiento de los servicios productores.

---

## ⏳ Cronología de Evolución (Semana a Semana)

### Marzo 2026: Ingeniería de Performance e Infraestructura Inmutable
- **12 de Marzo (Hito de Optimización y FinOps)**: 
  > [!IMPORTANT]
  > **Hallazgo Crítico: Cuello de botella en Service Bus Sessions**
  > Se descubrió que el throughput estaba limitado a ~8 msgs/min por instancia.
  > **Causa**: Azure ignora `messageHandlerOptions` cuando las sesiones están activas. Se requiere `sessionHandlerOptions`.
  > **Solución**: Ajuste de `messageWaitTimeout` de 7.5s (default) a 1s y aumento de `maxConcurrentSessions` a 16.
  > **Resultado**: Mejora inmediata a **48 msgs/min** total (2x de mejora medida).

  - **ADR: Terraform Layering (Separación de Preocupaciones)**:
    - **Commit `2916bfc`**: Didier Martinez.
    - **IaC: Terraform Layering**:
    - División de infraestructura en 4 capas (Base, Datos, Comunicacion, Aplicacion).
    - Implementación de `terraform_remote_state` para intercomunicación.
- **16 de Marzo (Intercomunicación de Capas y Serverless App)**:
  - **Application Plane**: Creación de Topic central para aprovisionamiento de tenants y exposición de configuration strings mediante `outputs.tf` para ser consumidos remotamente.
  - **Control Plane**: Consumo del Service Bus central usando `terraform_remote_state` (Verificación empírica del ADR de Layering).
  - **ADR / Workaround Serverless**: Durante el desarrollo de la Azure Function `HandleTenantProvisioning`, se detectó que el *binding* de salida (`ServiceBusOutput`) del Worker Aislado de .NET 10 "destruye" o no preserva el `SessionId` durante la serialización. 
    - **Solución**: Reemplazar binding declarativo por inyección de código imperativo (`ServiceBusSender` Singleton) para garantizar retención del Contexto FIFO de los Tenants.
  - **Pipeline**: CI/CD implementado para la nueva Function App (`tenant-provisioning-function-app-deploy.yml`).
- **13 de Marzo (Expansión de IaC y App Plane)**:
  - **Application Plane**: Configuración base de IaC habilitando el Service Bus y el Resource Group del plano operativo.
  - **Control Plane (ObligacionesPorPagar)**: Inicialización de Infraestructura serverless usando una Function App en `.NET 10 (Isolated)`, integrando configuración base de observabilidad (Log Analytics y App Insights).
  - > [!NOTE]
    > **Patrón Emergente**: Los dominios se manejan en repositorios independientes (ej. `ObligacionesPorPagar.ControlPlane`) pero comparten flujos de despliegue consistentes mediante GitHub Actions e IaC estandarizado por HashiCorp.
  - **Pipeline**: Nuevo workflow `.github/workflows/infraestructura-deploy.yml` con 5 jobs y despliegue paralelo por capas.
- **10-11 de Marzo (Evolución de Mensajería)**:
  - **ADR: Garantía de Orden FIFO por Tenant**:
    - **Commit `8815aaa`**: Activación de `SessionId` en Service Bus y Wolverine.
    - **Razón**: Asegurar que los eventos de un mismo Tenant se procesen en orden secuencial estricto, evitando colisiones en el Event Store de Marten.
  - **ADR: Migración a CritterStack**:
    - **Commit `133012d`**: Migración masiva a `Cosmos.EventDriven.CritterStack`.
    - **Acción**: Eliminación del `EventManager` interno a favor de la biblioteca estandarizada del equipo.
- **09 de Marzo (Refactor Core y Messaging)**: 
  - **Refactor**: Evolución de `TenantOnboarding` a `TenantOnboardingAggregateRoot`.
  - **Messaging**: Implementación de **UnitOfWorkMiddleware** y patrón de **Forwarding** en Service Bus.

---

## 🔍 Registro de Hallazgos y Lecciones Aprendidas

### 🛡️ Idempotencia y Consistencia (ADR Implícito)
- **Aprendizaje**: La idempotencia es el "seguro de vida" en EDA. Marten gestiona esto mediante versiones de agregados, pero el uso de **Sessions en Service Bus** es lo que previene el entrelazado de mensajes de un mismo proceso.

### ⚡ Rendimiento Serverless
- **Lección**: El `messageWaitTimeout` es el enemigo silencioso de las funciones con sesiones. Configurar un timeout de 1s es vital para que un worker no quede "atrapado" esperando una sesión inexistente mientras hay backlog.

### 💵 FinOps y Gobierno de Infraestructura
- **ADR**: Los logs de diagnostico de Azure deben estar filtrados.
- **Hallazgo**: Log Analytics es el costo #1. Se debe evitar el log de "Verbose" en producción y usar muestreo adaptativo en Application Insights.

---

## 👥 Gobernanza y Colaboradores
- **Luis Felipe Díaz (Líder Técnico)**: Arquitectura CritterStack, Estrategia de Sesiones FIFO y EDA.
- **Didier Martinez (Ingeniería de Sistemas)**: Multi-Layer Terraform, Gestión de Sesiones, Lógica de Onboarding y Tracking Maestro.
- **Augusto Romero (Infraestructura)**: EventCatalog y Estándares de Documentación de Eventos.
- **Camilo Barreto (DevOps/QA)**: Automatización de Calidad y Pipelines de CI/CD.

---
*Última actualización: 13 de Marzo, 2026 (Auditoría Profunda de Commits)*
