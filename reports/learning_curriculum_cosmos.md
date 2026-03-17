# 🎓 Curriculum de Refuerzo: Cosmos & SaaS Architect

> Este es tu plan maestro para cerrar la brecha técnica y convertirte en una pieza clave en el diseño de **Cosmos Platform**. 

---

## 🎯 Objetivo: "De Aprendiz a Arquitecto del ControlPlane"
No se trata de estudiar todo a la vez, sino de estudiar lo que **se está integrando en el repo HOY**.

---

## 📅 Fase 1: Los Cimientos (Lo que el equipo usa hoy)
*Meta: Entender y poder modificar los Handlers y Pruebas que Luis Felipe y Camilo están subiendo.*

### 1. C# Moderno & Clean Code
- **Foco**: Records, Init-only properties, Expression-bodied members.
- **Por qué**: Es el estilo que se ve en `NotifyProgress` y `Entities`.
- **Recurso**: [Exercism C# Track](https://exercism.org/tracks/csharp) (Semanas 2 y 3).

### 2. TDD & Testing (Crítico 🔴)
- **Foco**: **xUnit v3** y **NSubstitute**.
- **Por qué**: Es la base de los últimos 10 commits. Necesitas saber cómo mockear un `IPrivateEventSender`.
- **Práctica**: Replicar un `NotifyProgressTests` desde cero.

### 3. Wolverine & Marten (El Puente a tu Repo de Dometrain)
- **Foco**: Event Sourcing básico y Proyecciones.
- **Por qué**: Es el motor de persistencia y mensajería del Control Plane.
- **Hack de Aprendizaje**: ¡Ya hiciste el curso de Dometrain! Tu repositorio `eventsourcing-workshops-basics` tiene los fundamentos exactos. Un `AggregateRoot` en Cosmos (como `TenantOnboarding`) sigue exactamente las mismas reglas que tu record `OrdenCompra` y sus métodos estáticos `Apply` y `Create`. Vuelve a leer tus apuntes sobre las Proyecciones "Inline", porque es el mismo código que usan hoy en el ERP.

---

## 📅 Fase 2: Arquitectura Avanzada (Semanas 3+)
*Meta: Participar en discusiones de diseño de alto nivel.*

### 1. EDA & Service Bus (Deep Dive)
- **Foco**: Forwarding, Topics vs Queues, Dead Lettering.
- **Por qué**: Es la infraestructura que Luis Felipe refactorizó hoy.

### 2. Event Sourcing & Projections
- **Foco**: Cómo transformar una lista de eventos en un estado actual.
- **Por qué**: Para entender cómo funciona el Onboarding de un Tenant de principio a fin.

### 3. SaaS Multitenant (Libro)
- **Lectura Prioritaria**: Capítulos sobre **Tenant Isolation** y **Provisioning Strategies**.
- **Conexión**: Comparar lo que dice el libro con cómo se está haciendo el `OnboardTenant` en el repo.

---

## 💡 Estrategia de Estudio "Paralela"
*Para no quemarte con tantos temas:*

1.  **Trayecto (🚗)**: Audio/Podcast de **SaaS Architecture** o **EDA Essentials**. (Teoría).
2.  **Bloque 1 (30m)**: **C# en Exercism**. (Músculo técnico).
3.  **Bloque 2 (45m)**: **Ingeniería Inversa del Repo**. Abre un commit de hoy, intenta entender el Test, y busca en Google/IA lo que no entiendas. (Contexto real).

---
*"El mejor momento para aprender un patrón es cuando lo estás viendo fallar o evolucionar en el código real."*
