---
description: validación interactiva de aprendizaje (Sincronización de Cursos)
---

Este workflow se activa cuando el usuario pega contenido desde el Bookmarklet (`--- SYNC DATA ---`).

1. **Agradecimiento y Pausa**: 
   - Notificar que se recibió la información de la clase.
   - **IMPORTANTE**: No actualizar los registros todavía.
   - Preguntar: "¡Genial! Lección capturada. Ahora, para que el conocimiento se fije bien, cuéntame en tus palabras: **¿Qué fue lo que entendiste de esta clase?**"

2. **Evaluación Comparativa**: 
   - Comparar el texto del usuario ("LO QUE ENTENDÍ") con el "CONTENIDO" técnico capturado.
   - **Acción del Agente**: 
     - ✅ **Validar**: Confirmar lo que el usuario entendió correctamente.
     - ⚠️ **Clarificar/Refutar**: Corregir errores conceptuales o vacíos técnicos basándose en el resumen de la clase.
     - ❓ **Pregunta de profundidad**: Hacer una pregunta pequeña para cerrar el concepto.

3. **Consolidación**:
   - Una vez el usuario y el agente están de acuerdo en el concepto, proceder a:
     - Actualizar `mi-traker.md` con el nuevo capítulo.
     - Actualizar la fuente maestra `📔 Mis Apuntes: ...` en NotebookLM.
     - Registrar el bloque de tiempo en `timeline.md`.
     - Hacer `commit` y `push` a GitHub.
