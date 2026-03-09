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

### Marzo 2026: Refactor Estructural y Documentación
- **09 de Marzo**: 
  - **Refactor Core**: Evolución de `TenantOnboarding` a `TenantOnboardingAggregateRoot`.
  - **Modularización**: Extracción de `EventManager` como proyecto independiente.
  - **Unificación**: Namespaces reorganizados bajo `Onboarding.Entities` y `Onboarding.NotifyProgress`.
  - **Validación**: Cobertura robusta de handlers de notificación.
- **06 de Marzo**:
  - **EventCatalog**: Integración formal del CLI de EventCatalog para documentar eventos.
  - **Skills**: Migración de lógica de comandos a "Skills" en el repositorio.

### Febrero 2026: Cimientos e Infrastructura
- **19 de Febrero**:
  - **CI/CD**: Integración de **SonarCloud** en los pipelines para análisis estático y cobertura.
  - **Testing**: Adopción de **xUnit v3** con MTP (Microsoft Testing Platform).
- **18 de Febrero**:
  - **Service Bus**: Creación de los primeros temas y suscripciones en Terraform para `TenantProvisioning`.
  - **OpenTelemetry**: Limpieza de tracing manual a favor de las capacidades nativas del SDK.

---

## 👥 Colaboradores Clave
- **Luis Felipe Díaz**: Liderazgo técnico en refactorización y mensajería.
- **Didier Martinez**: Implementación de subdominios, eventos y sistema de tracking.
- **Augusto Romero**: Diseño de infraestructura y EventCatalog.
- **Camilo Barreto**: DevOps, CI/CD y Calidad (SonarCloud).

---
*Última actualización: 09 de Marzo, 2026*
