# 🔁 Loop 1 — Fundamentos: qué es un evento y dónde vive

> Unidad de estudio sobre **tu propio workshop** (`eventsourcing-workshops-basics`). Sigue el protocolo de Loops: foco → leer → validar. Nada se marca ✅ hasta responder el quiz del final.

**Fecha:** 2026-06-09 · **Estado:** 🟡 En progreso · **Curso:** Workshop Event Sourcing (propio)

---

## 🎯 Objetivo de la sesión
Quedarte con la **intuición fundacional** del Event Sourcing antes de tocar código: *no guardas el estado actual, guardas la secuencia de hechos y reconstruyes el estado reproduciéndolos*. Y entender **dónde vive** ese modelo: dentro de una frontera (Bounded Context) que se comunica con el mundo de afuera.

## 📖 Qué leer (en orden del ROADMAP)
1. `secciones/01-el-diario-de-jhon.md` — la metáfora del diario: hechos que pasan, no estado que se sobrescribe.
2. `secciones/01b-mapa-de-contextos.md` — el mapa: tu Bounded Context y los hechos que se quedan vs los que cruzan.
3. `secciones/03-vivir-el-pasado.md` — *replay*: reconstruir el estado aplicando los eventos uno por uno.

*(Tiempo estimado: 20-30 min de lectura activa. Si fluye, encadenas el Loop 2 hacia §04-§05.)*

---

## ❓ Pregunta de foco (mientras lees)
Mientras lees, persigue **una** idea: **¿por qué reproducir eventos da más información que guardar solo el estado final?** Busca el ejemplo concreto en §01/§03 que lo demuestre (¿qué pregunta puedes responder con el historial que con el estado actual *no*?).

## 📝 Skimming — 5 puntos clave (contexto previo)
1. **Evento = hecho pasado e inmutable** (`PersonaNacida`, `PersonaCasada`): nombrado en pasado, nunca se edita ni se borra.
2. **El estado es derivado**, no almacenado: lo calculas reproduciendo (*replay*) los eventos desde cero.
3. **Bounded Context** = frontera con su propio lenguaje. Todo lo que construyes vive dentro de uno.
4. **Dos clases de hecho:** el que se queda adentro (privado, p. ej. `NoviazgoIniciado`) y el que cruza a otro sistema (integración, p. ej. `MatrimonioCelebrado` → Registro Civil). El criterio NO es social: es si *otro sistema debe reaccionar* (efecto legal/oficial).
5. **El replay es un `foreach` que aplica cada evento** sobre un objeto vacío hasta dejarlo en su estado actual — eso es la "rehidratación".

---

## ✅ Validación (responder DESPUÉS de leer — esto cierra el Loop)
Responde con tus palabras, sin volver al texto:

1. Jhon nació, cumplió 3 años y se mudó dos veces. Si solo guardaras su **estado actual**, ¿qué información perderías que el **historial de eventos** sí conserva? Da un ejemplo de pregunta que el historial responde y el estado no.
2. ¿Por qué `MatrimonioCelebrado` cruza la frontera del Bounded Context pero `NoviazgoIniciado` no? Formula el criterio en una frase.
3. Describe en 2-3 pasos qué hace el *replay* para reconstruir a "Jhon hoy" a partir de su lista de eventos.

> Cuando termines, escribe **"ya"** o pega tus respuestas y valido comprensión (✅ lo correcto, ⚠️ lo que falte). Recién ahí el Loop pasa a ✅ y suma al contador del día.

---
*Al cerrar: `bash sync.sh` (o Claude Code CLI) para versionar, y subo el payload a NotebookLM si quieres.*
