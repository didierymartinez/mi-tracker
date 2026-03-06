# 🛠️ Mi Toolbox de Seguimiento

Este repositorio contiene las herramientas necesarias para mantener mi tracker sincronizado entre dispositivos.

> [!TIP]
> **Si eres un Agente de IA (Antigravity/Cursor)**: Puedes inicializar este entorno automáticamente ejecutando el workflow `/setup` definido en `.agents/workflows/setup.md`.

## 🚀 Sync Bookmarklet (Navegador)

Para sincronizar tu progreso desde Platzi, Udemy o Dometrain, usa el siguiente Bookmarklet.

### Instalación:
1. Crea un nuevo marcador en tu barra de favoritos.
2. En el campo de URL, pega el siguiente código:

```javascript
javascript:(function(){try{var platform=window.location.hostname;var course="Curso",lesson="Lección",content="";if(platform.includes('platzi')){course=document.querySelector('h1, .CourseTitle, [class*="CourseTitle"]')?.innerText?.trim()||"Platzi Course";lesson=document.querySelector('h2, .Material-title, [class*="MaterialTitle"]')?.innerText?.trim()||"Clase";var mainContent=document.querySelector('.Resources_Resources__Articlass__layout__q1nVI, [class*="Resources_Resources__Articlass"]');var summary=document.querySelector('[data-class="resources-summary"], .resources-summary');if(mainContent){content=mainContent.innerText.trim()}else if(summary){content=summary.innerText.trim()}if(!content){content=document.querySelector('.Material-transcript, .Transcript-content')?.innerText?.trim()||""}}var syncData="--- SYNC DATA ---\nPlataforma: "+platform+"\nCurso: "+course+"\nLección: "+lesson+"\nFecha: "+new Date().toLocaleString()+"\n---\nCONTENIDO:\n"+(content||"No se detectó el contenido ni el resumen.");var el=document.createElement('textarea');el.value=syncData;document.body.appendChild(el);el.select();document.execCommand('copy');document.body.removeChild(el);alert(content?'✅ ¡CONTENIDO CAPTURADO! Pégalo en Antigravity.':'⚠️ Se copió la info básica, pero el área de contenido '+ (mainContent?'estaba vacía':'no se encontró')+'.');}catch(e){alert('Error: '+e.message)}})();
```

---

## 🤖 Configuración de Antigravity (MCP y Comandos Proactivos)

Para que Antigravity pueda:
- `/log`: Reporte diario detallado y Friday Review.
- `/study_sync`: (Automático al pegar `--- SYNC DATA ---`). Validación interactiva de aprendizaje (Socratic Sync) para asegurar el dominio de los temas antes de archivar.
- `/track [texto]`: Registrar una actividad técnica inmediata en el Timeline (ej: `/track @oficina Refactor API`).
- `/todo [texto]`: Agregar algo a la lista de pendientes de `mi-traker.md`.
- `/idea [texto]`: Capturar una idea o distracción en el `inbox.md`.
...y leer tus notebooks, debes tener configurado el servidor MCP de NotebookLM.

### Instalación del Servidor:
Ejecuta estos comandos en tu terminal si cambias de máquina:

```bash
# 1. Crear entorno y entrar a la carpeta
cd ~/Documents/dev_didier
python3 -m venv .mcp_venv
source .mcp_venv/bin/activate

# 2. Instalar el servidor (asumiendo que tienes el paquete/script)
pip install notebooklm-mcp  # O el comando de instalación que corresponda
```

### Configuración de Antigravity:
Asegúrate de que tu archivo `~/.gemini/antigravity/mcp_config.json` se vea así:

```json
{
    "mcpServers": {
        "notebooklm": {
            "command": "/Users/TU_USUARIO/Documents/dev_didier/.mcp_venv/bin/notebooklm-mcp",
            "args": []
        }
    }
}
```
> **Nota**: Recuerda actualizar `/Users/TU_USUARIO/` con tu nombre de usuario local.

## 🔗 Accesos Rápidos
- [⏳ Mi Línea de Tiempo (Diario)](file:///Users/didierymartinez/Documents/dev_didier/timeline.md)
- [📥 Inbox (Ideas y Distracciones)](file:///Users/didierymartinez/Documents/dev_didier/inbox.md)
- [🧠 Biblioteca de Conocimiento (NotebookLM)](https://notebooklm.google.com/notebook/6b703266-4050-4357-b010-ae7076119e5f)
