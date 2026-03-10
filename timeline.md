# ⏳ Mi Línea de Tiempo: Progreso y Conocimiento

> *"No recordamos días, recordamos momentos de aprendizaje."*

Este es mi registro diario de ejecución. Aquí guardo lo que hice, lo que aprendí y las decisiones técnicas que tomé.

---

## 🗓️ Marzo 2026

### Semana 1 (Mar 02 - Mar 08)
**Foco:** Terraform State y Configuración de Ecosistema de Estudio.

- **Vie 06/03**: 
  - [09:00 AM - 11:00 AM] | 🏢 **Oficina** | Sesión de **control-plane** (EventCatalog) con Felipe y Augusto.
    - **Resultado**: Avance en diagramación de arquitectura orientada a eventos. Pendiente: profundizar en conceptos de EventCatalog.
  - [11:00 AM - 12:55 PM] | 🏢 **Oficina** | **Terraform (Platzi)** - Capítulos 19 y 20 (Backend Remoto).
    - **Hito**: Configuración exitosa de Backend Remoto con Azure SAS Tokens.
    - **Aprendizaje**: Uso de Shared Access Signatures para acceso temporal y seguro de Terraform al Storage Account.
  - [02:36 PM - 04:10 PM] | 🏢 **Oficina** | Sesión de diseño con equipo **Cosmos** (EventCatalog).
    - **Actividad**: Continuación del trabajo en la estructura de eventos y documentación del control-plane.
  - [11:30 AM - Actualidad] | 🏢 **Oficina** | **Optimización mi-tracker**.
    - **Actividad**: Creación de sistema de sincronización (Bookmarklet v6), Bitácoras maestras en NotebookLM, Workflows de agente (/setup, /log, /track) e integración con GitHub.
  - **Hito Semanal**: Ecosistema de estudio 100% automatizado y portable.
- **Jue 05/03**: 
  - [08:30 AM - Todo el día] | 🏢 **Oficina** | Sesión de diseño con equipo **Cosmos Plataforma** (Felipe, Augusto y Felipe).
  - **Actividad**: Diseño del **control-plain**. Planeación de estructura **EDA** y **Serverless** para la base del ERP SaaS.
  - **Aprendizaje Clave**: Urgencia de aprender y dominar la estructura de **EventCatalog** para la documentación de eventos.
  - **Nota**: No se realizaron ejercicios de C# ni lectura del libro SaaS debido a la intensidad del diseño arquitectónico.
- **Mié 04/03**: 
  - [Todo el día] | 🏠 **Teletrabajo** | Integración de Tokens y Publicación de NuGet.
  - **Actividad**: Terminé la integración de tokens de autorización en el proyecto **Marco**.
  - **Hito Personal**: Logré publicar el **NuGet** y subirlo al repositorio, algo que quería hacer desde hace mucho tiempo.
  - **Estado técnico**: Los dos tokens quedaron enlazados al generarse. Por ahora están en el ambiente de pruebas. Pendiente: validación y asegurar endpoints.
  - **Aprendizaje**: Fundamentos de **RSA** para seguridad de tokens.
- **Mar 03/03**:
  - [Todo el día] | 🏠 **Teletrabajo** | Desarrollo Marco y Correcciones (HD).
  - **Actividad 1**: Inicio de desarrollo de tokens para Marco.
  - **Actividad 2 (HD)**: Solucionado error en cambio de contraseñas que permitía validar usuarios inactivos.
  - **Hito**: Uso de IA para acelerar la publicación del paquete NuGet.
- **Lun 02/03**: 
  - [08:30 AM - 05:30 PM] | 🏢 **Oficina** | Definiciones con equipo **Cosmos**.
  - **Actividad**: Trabajo en definiciones de arquitectura y lógica. (Inauguración del sistema de tracker).
  - **Hito**: Inicio formal del tracker y configuración de MCP/Git.
  - **Trayecto**: 1h 10min escuchando podcast/cursos de programación. (Llegada a casa con lluvia fuerte).

### Semana 2 (Mar 09 - Mar 15)
**Foco:** EventCatalog, C# Profundo y Seguridad en Marco.

- **Lun 09/03**: 
  - [08:30 AM - 09:40 AM] | 🚗 **Trayecto** | Video **EDA - The Basics** (David Boyne).
    - **Nota**: Video en inglés. El usuario reporta no haberlo comprendido plenamente. **Deuda técnica: Reforzar conceptos básicos de EDA en español.**
  - [09:40 AM - 01:10 PM] | 🏢 **Oficina** | Sesión de diseño y codificación **ControlPlane** (Cosmos).
    - **Hito**: Reestructuración profunda de la arquitectura de Onboarding.
    - **Cambios Clave**:
      - **Namespaces**: Unificación a `Onboarding.Entities` y `Onboarding.NotifyProgress`.
      - **Mensajería**: Implementación de una arquitectura de "Forwarding" en Service Bus (temas renamed con sufijo `Topic` reenvían a una cola única `onboardingQueue`).
      - **Core**: Extracción del `EventManager` como proyecto independiente.
      - **Refactor**: `TenantOnboarding` evolucionó a `TenantOnboardingAggregateRoot` con mejores métodos de notificación y emisión de eventos de finalización.
      - **Pruebas Técnicas**:
        - Consolidación de `NotifyProgressTests` y validación de emisión de `TenantOnboardingFinished`.
        - Migración a `CommandHandlerAsyncTestWithPublisher` para pruebas de integración de eventos.
      - **Middleware & Persistencia**:
        - Implementación de **UnitOfWorkMiddleware** en Onboarding y TenantProvisioning.
        - Configuración de **EventStore** e **IPrivateEventSender** en el módulo de `TenantProvisioning`.
        - Mejoras en la deserialización de mensajes (Case-insensitive y parseo de `Subject` de Wolverine).
  - [01:45 PM - 03:01 PM] | 🏠 **Teletrabajo** | **Terraform (Platzi)** - Capítulo 21: Data Sources.
    - **Nota**: El usuario reporta no haber comprendido el concepto de bloques `data`. **Deuda técnica: Reforzar Data Sources (Solo Lectura) en español.**
  - [07:22 PM - 08:20 PM] | 🏠 **Casa** | **Terraform (Platzi)** - Capítulo 22: FMT y Validate.
  - [08:20 PM - 08:36 PM] | 🏠 **Casa** | **Terraform (Platzi)** - Capítulo 23: Máquinas Virtuales.
    - **Aprendizaje**: Despliegue de VM en Azure. Dependencias: RG -> VNET -> Subnet -> NSG -> NIC -> VM.
  - [08:36 PM] | 🏠 **Casa** | **Cierre Total de Jornada.**

---

## 📊 Resúmenes de los Viernes (Friday Reviews)

### [Viernes 06/03/2026]
- **Logro principal**: Arquitectura de estudio 100% portable y automatizada + Publicación histórica de NuGet.
- **Top 3 Aprendizajes**:
  1. Seguridad de estado en IaC (Terraform) y RSA para tokens.
  2. Implementación de EDA y Serverless para ERP SaaS (Cosmos).
  3. Automatización de flujo de trabajo con Agentes IA.
- **Hábito de Valor**: Aprovechamiento de las 2h 20min diarias de trayecto a la oficina escuchando contenido técnico.
- **Dificultades**: Clima severo en desplazamientos y sincronización inicial de herramientas.
- **Plan para la próxima semana**: Profundizar en EventCatalog, asegurar los endpoints del proyecto Marco y atender reunión presencial de **SincoSoporte** (Vie 13/03).

---

## 📚 Biblioteca de Conocimiento (Links Rápidos)
- [Notebook: Mis Apuntes Terraform](https://notebooklm.google.com/notebook/164b395e-8c20-4d6e-b857-1bdd91782044)
- [Notebook: Mis Apuntes Azure](https://notebooklm.google.com/notebook/3d5334b0-642f-48d1-94e0-9c386e69f786)
- [Meta-Tracker (IA Insights)](https://notebooklm.google.com/notebook/309117f8-b478-4bb5-aa00-9a7c2d9b57c0)

---
*Usa el comando /timeline para que el agente me ayude a llenar el día actual.*
