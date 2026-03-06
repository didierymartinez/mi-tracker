---
description: actualizar línea de tiempo diaria y aprendizajes
---

Este workflow ayuda al usuario a registrar su actividad diaria en la línea de tiempo.

1. Identificar el día actual y la semana correspondiente en `timeline.md`.
2. Pedir al usuario:
   - ¿Qué actividades realizó hoy?
   - ¿Cuál fue el aprendizaje clave?
   - ¿Alguna nota técnica o enlace relevante?
3. Escribir la entrada en `timeline.md` bajo la fecha actual.
4. Si es viernes, generar automáticamente la sección de **Friday Review** basada en las entradas de la semana.
5. Sincronizar el contenido de `timeline.md` con el Notebook **"🧠 Mi Biblioteca de Conocimiento & Crónicas"** reemplazando la fuente anterior.
6. Hacer `git commit` y `push` con el resumen del día.
