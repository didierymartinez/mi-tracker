# 🚀 Bulletin Friday — Semana 3 (Marzo 16 - Marzo 20)

Esta semana fue de **desacoplamiento arquitectónico y validación real**. Pasamos de conceptos teóricos de Event Sourcing a implementaciones de infraestructura y pruebas que tocan el bus de mensajes real.

### 🎯 Hitos Técnicos
- **Identidad Moderna**: Se integró **WorkOS** en `UserManagement`. Se implementó el patrón de **Puertos y Adaptadores** para proteger el dominio de la dependencia directa con el SDK de WorkOS.
- **Testing de Calidad**: Refactor de `Onboarding.AcceptanceTests` para usar `ServiceBusSessionProcessor`. Ya no mockeamos el bus; las pruebas ahora validan la asincronía real con Service Bus y la consistencia eventual con polling.
- **Aprendizaje Continuo**: Consolidación de los pilares de Event Sourcing (Aggregate Roots, Hanlders) mediante el workshop de Dometrain.

### 🛠️ Logros de Infraestructura
- **Terraform**: Estandarización de secretos de identidad (`workos_api_key`) en el workflow de despliegue de `user-management-function-app`.
- **Remote State**: Solución al intercambio de secretos entre capas de Terraform usando `terraform_remote_state`.

### 🧠 Aprendizajes de la Semana
- La importancia del **Aggregate Root** como frontera de consistencia.
- Manejo de **Sesiones en Service Bus** para garantizar el orden de procesamiento en entornos multitenant.
- Cómo WorkOS facilita el onboarding de administradores mediante sus APIs de Organizaciones y Usuarios.

---
*"La arquitectura es el arte de posponer decisiones, pero la implementación es el arte de validarlas racionalmente."*
