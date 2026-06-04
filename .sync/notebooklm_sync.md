# 🧠 Convención de sincronización a NotebookLM (el "cerebro")

> Cowork **no** puede escribir en NotebookLM directamente (el MCP `notebooklm-mcp-server` vive en Antigravity con tu sesión de Google). Modo acordado: **Cowork prepara el payload exacto → Antigravity/tú lo suben.**

## Cómo funciona
Cuando registremos algo que deba ir al cerebro (un `/log`, un `/study_sync`, una consolidación), Cowork genera un bloque con este formato listo para pegar/subir:

```
--- NOTEBOOKLM SYNC ---
Notebook destino: <nombre> (ID: <id>)
Título de la fuente: <título>
Acción: [crear fuente nueva | reemplazar fuente anterior]
---
CONTENIDO:
<markdown completo>
```

## Mapa de notebooks (de los workflows existentes)
| Disparador | Notebook destino | Acción |
|-----------|------------------|--------|
| `/log` (timeline diario / Friday Review) | 🧠 Mi Biblioteca de Conocimiento & Crónicas | Reemplazar fuente anterior |
| `/study_sync` (transcripción de curso) | 🎓 Curso Udemy: Arquitectura de Sistemas (Transcripciones) — ID `310240db-9726-421f-b276-2f29c8d53695` | Crear fuente: `Capítulo N: [nombre] (Full Transcript)` |
| Consolidaciones Cosmos | Notebook de Cosmos correspondiente | Crear fuente nueva |

## Notebook principal de conocimiento
https://notebooklm.google.com/notebook/6b703266-4050-4357-b010-ae7076119e5f

> Si más adelante conectas un MCP de NotebookLM a Cowork, este paso se vuelve automático como el de git.
