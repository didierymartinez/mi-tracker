---
description: inicializar entorno de estudio y MCP de NotebookLM
---

Este workflow automatiza la configuración de este proyecto cuando se abre en un nuevo entorno o máquina.

// turbo-all
1. Crear el entorno virtual de Python
```bash
python3 -m venv .mcp_venv
```

2. Instalar dependencias necesarias
```bash
./.mcp_venv/bin/pip install -r requirements.txt
```

3. Configurar el archivo mcp_config.json de Antigravity
   - Lee el contenido de `mcp_config.json.example` en este directorio.
   - Identifica la ruta absoluta de este proyecto.
   - Escribe (o actualiza) el archivo `~/.gemini/antigravity/mcp_config.json` reemplazando `TU_USUARIO` por el usuario actual y asegurando que la ruta al ejecutable `notebooklm-mcp` sea correcta.

4. Verificar la conexión con NotebookLM
   - Intenta listar los notebooks usando el servidor MCP recién configurado.
```bash
# El agente debe usar la herramienta mcp_notebooklm_notebook_list para validar
```

5. Sincronizar estado inicial
   - Leer `mi-traker.md` para entender el estado actual del usuario.

Una vez completado, el agente está listo para recibir comandos de sincronización desde el bookmarklet.
