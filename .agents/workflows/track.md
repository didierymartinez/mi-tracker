---
description: seguimiento inteligente de actividades (Quick Track)
---

Este workflow se activa cuando el usuario usa `/track`.

1. **Captura Inicial**: Recibir el texto de la actividad.
2. **Validación de Escenario**: Preguntar proactivamente:
   - "¿Es esta la hora en la que iniciaste o ya terminaste?" (Si es pasado, pedir la hora aproximada).
   - "¿Cuáles son los objetivos principales para esta actividad?"
   - (Si ya terminó) "¿A qué conclusiones llegaste o qué quedó pendiente?"
3. **Contexto de Ubicación**: Preguntar si se encuentra en:
   - 🏢 Oficina
   - 🏠 Casa / Remoto
   - 🚗 Trayecto (Conmuting)
4. **Registro**: Inserción en `timeline.md` con el formato:
   - `[Hora Inicio - Hora Fin] | [Ubicación] | [Actividad]`
   - `  - Objetivos: ...`
   - `  - Conclusiones/Notas: ...`
5. **Sincronización**: Al final de la interacción, hacer commit y push.
