---
description: validación interactiva de aprendizaje (Sincronización de Cursos)
---

Este workflow se activa cuando el usuario pega un bloque `--- SYNC DATA ---` de Udemy.

## ⚠️ Contexto Crítico: Técnica de Skimming

El usuario usa la técnica de **Skimming**: pega el resumen/transcripción del capítulo **ANTES de verlo** como adelanto previo al video. El bloque `--- SYNC DATA ---` llega en ese momento.

Por lo tanto, cuando se recibe un bloque `--- SYNC DATA ---`:
- El capítulo está **"En progreso"** (🟡), NO completado.
- NO preguntar qué entendió (aún no lo vio).
- NUNCA marcar el capítulo como completado (✅) hasta que el usuario lo confirme explícitamente.

---

## Pasos del Workflow (siempre en este orden)

// turbo-all

1. **Guardar la transcripción en NotebookLM**
   - Notebook destino: `🎓 Curso Udemy: Arquitectura de Sistemas (Transcripciones)` (ID: `310240db-9726-421f-b276-2f29c8d53695`)
   - Título de la fuente: `Capítulo N: [Nombre de la lección] (Full Transcript)`
   - Incluir los conceptos clave condensados del contenido.

2. **Actualizar `mi-traker.md`**
   - Cambiar el capítulo actual del curso al nuevo número → estado `🟡 En progreso`.

3. **Hacer `git commit` y `git push`** desde `/Users/didierymartinez/Documents/dev_didier` con mensaje: `track: cap N udemy ([tema]) en progreso`

4. **Responder al usuario** con:
   - Confirmación de lo guardado.
   - Resumen muy breve del capítulo (3-5 puntos clave) para que vea el video con contexto.
