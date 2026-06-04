# 🔁 Protocolo del Loop (estudio con validación — Socratic Sync)

> El Loop es la unidad anti-procrastinación. **Arranca cuando Didier pega la transcripción de un video** (bloque `--- SYNC DATA ---` o el texto del transcript). El cronómetro del dashboard es opcional, encima de esto.

## Secuencia exacta (siempre en este orden)

**1. Didier pega la transcripción del video.**

**2. El asistente responde con (ANTES de que vea el video):**
   - ✅ Confirmación breve de qué se guardó (curso + lección).
   - 🎯 **UNA pregunta de foco**: algo concreto en qué fijarse mientras ve el video (formulada como pregunta, no como instrucción).
   - 📝 **Skimming**: resumen de 3-5 puntos clave del capítulo, para que lo vea con contexto previo.
   - Actualizar `mi-traker.md`/`metas.md`: capítulo → 🟡 **En progreso** (NUNCA ✅ todavía — técnica de skimming: pega ANTES de ver).
   - Generar el bloque `--- NOTEBOOKLM SYNC ---` (ver `notebooklm_sync.md`) para que él lo suba al cerebro.

**3. Didier ve el video y dice "ya" / "listo" / "terminé".**

**4. El asistente valida comprensión:**
   - Hace **2-3 preguntas** sobre el tema.
   - Compara las respuestas con el contenido técnico: ✅ valida lo correcto, ⚠️ corrige/completa lo que falte.
   - Da el OK final → recién ahí el capítulo pasa a ✅ **consolidado**.
   - Cuenta el Loop (suma al contador del día; 3 Loops = 1 Sprint).

## Reglas
- **Mínimo diario:** 1 Loop completo (transcripción → foco → ver → validación).
- Nada se marca ✅ sin pasar el paso 4.
- Al cerrar: recordar `bash sync.sh` (o Claude Code CLI) para git, y entregar el payload de NotebookLM.

---
*Basado en `.agents/workflows/study_sync.md`, adaptado a Cowork (yo preparo el payload de NotebookLM; el push lo haces tú/Antigravity/CLI).*
