# 📊 Bulletin Friday - Week 2 (Mar 09 - Mar 13, 2026)

> **Misión Crítica Semanal**: Diseño y construcción de arquitectura resiliente (Cosmos Control Plane) y fortalecimiento de fundamentos técnicos (Udemy, Terraform).

---

## 🚀 Logros Principales
1. **Infraestructura como Código (Cosmos)**: Evolución masiva de la IaC en el ecosistema Cosmos. Implementación del **Terraform Layering** (4 capas desacopladas usando `terraform_remote_state`) y estandarización de dominios `ApplicationPlane` y `ObligacionesPorPagar` con Serverless Aislando (Functions .NET 10, Service Bus, observabilidad independiente).
2. **Mensajería y Performance**: Resolución del cuello de botella en Service Bus mediante el ajuste a `sessionHandlerOptions`, multiplicando x6 la capacidad de procesamiento (de 8 a 48 msgs/minuto). Integración de patrón "Forwarding".
3. **Maratón Arquitectónica**: Sólido progreso en entrenamiento, completando los Capítulos del 1 al 10 del curso de "Diseño de Sistemas a Gran Escala" (Load Balancers, API Gateways, Brokers, Cachés).
4. **App Integradores & Tokens**: Refactorización del manejo de JWT en el proyecto Marco y diseño táctico de la plataforma de Integradores ERP.

---

## 🧠 Top 3 Aprendizajes / Hallazgos

1. **El poder de los `Data Sources` y el Remote State**: Para interconectar repositorios independientes sin acoplarlos, el uso de datos en modo lectura (Data Sources) y el consumo de la salida de otros estados (terraform_remote_state) es vital en infraestructuras maduras.
2. **FinOps y Observabilidad**: El "costo del silencio". Log Analytics representa el 86% de los gastos ($329 USD), lo que fuerza la necesidad de reducir la retención y auditar los pipelines de telemetría inútil (Verbose logs).
3. **Trade-offs de Escalabilidad**: Lecciones clave sobre GSLB para evitar SPOF, la necesidad absoluta de la Idempotencia al procesar eventos y el riesgo de inconsistencia al usar estrategias de caché como *Write Behind*.

---

## 🛡️ Hábito de Valor
- **Micro-Sprints en Teletrabajo**: La técnica de aislar el aprendizaje en bloques de 15-20 minutos enfocados (como el utilizado para la maratón de Udemy) ha demostrado ser superior al estudio pasivo continuo.

---

## 🚧 Dificultades / Retos
- Comprensión inicial de `data sources` en Terraform y el concepto básico de Event-Driven Architecture (EDA) en inglés de David Boyne (foco claro para repasar).
- El error de `TypeLoadException` en WolverinePublicEventSender que sigue persistente y requiere atención quirúrgica.

---

## 🎯 Plan para la Semana 3 (Próximos pasos)
1. **SincoSoporte**: Priorizar y programar los ajustes y nuevas funcionalidades derivados de la reunión del viernes 13/03.
2. **Cosmos Bugfix**: Atacar y exterminar la `TypeLoadException`.
3. **Educación Continua**: Completar el Bootcamp de Terraform (Platzi) y avanzar al capítulo 11 de Arquitectura de Sistemas (Udemy).
4. **C# Deep Tech**: Retomar la práctica diría para cerrar brechas de Deuda Técnica.
