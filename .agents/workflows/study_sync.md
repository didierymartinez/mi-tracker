---
description: validación interactiva de aprendizaje (Sincronización de Cursos)
---

Este workflow se activa cuando el usuario pega un bloque `--- SYNC DATA ---` de Udemy.

## ⚠️ Contexto Crítico: Técnica de Skimming

El usuario usa la técnica de **Skimming**: pega el resumen/transcripción del capítulo **ANTES de verlo**. 

Por lo tanto:
- El capítulo está **"En progreso"** (🟡), NO completado.
- NUNCA marcar el capítulo como completado (✅) hasta que el usuario confirme explícitamente ("ya lo vi").

---

## Pasos del Workflow (siempre en este orden)

// turbo-all

1. **Guardar la transcripción en NotebookLM**
   - Notebook destino: `🎓 Curso Udemy: Arquitectura de Sistemas (Transcripciones)` (ID: `310240db-9726-421f-b276-2f29c8d53695`)
   - Título de la fuente: `Capítulo N: [Nombre de la lección] (Full Transcript)`

2. **Actualizar `mi-traker.md`**
   - Cambiar el capítulo actual del curso al nuevo número → estado `🟡 En progreso`.

3. **Hacer `git commit` y `git push`** desde `/Users/didierymartinez/Documents/dev_didier` con mensaje: `track: cap N udemy ([tema]) en progreso`

4. **Responder al usuario** con:
   - Confirmación breve de lo guardado.
   - **Resumen de 3-5 puntos clave** para que vea el video con contexto previo.
   - **UNA pregunta de enfoque** ("Mientras lo ves, presta atención a: ...") para que observe el video activamente. Formular como pregunta concreta, no como instrucción.

5. **Esperar a que el usuario diga que ya vio el video** (ej. "ya lo vi", "listo", "terminé").

6. **Cuando confirme que ya lo vio**, hacer 2-3 preguntas de validación de comprensión para confirmar lo aprendido. Comparar sus respuestas con el contenido técnico del capítulo y:
   - ✅ Validar lo correcto.
   - ⚠️ Corregir o completar lo que falte.
   - Dar el OK final para cerrar el capítulo como consolidado.
