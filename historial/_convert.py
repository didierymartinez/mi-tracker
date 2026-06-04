#!/usr/bin/env python3
import json, re, sys

def clean_user(c):
    c = re.sub(r'<ADDITIONAL_METADATA>.*', '', c, flags=re.S)
    c = c.replace('<USER_REQUEST>', '').replace('</USER_REQUEST>', '')
    return c.strip()

def to_md(path, title, intro):
    rows = []
    for l in open(path, encoding='utf-8', errors='replace'):
        l = l.strip()
        if not l:
            continue
        try:
            rows.append(json.loads(l))
        except:
            pass
    rows.sort(key=lambda r: (r.get('created_at', ''), r.get('step_index', 0)))
    out = [f"# {title}\n", intro, ""]
    # date range
    dates = [r['created_at'][:10] for r in rows if r.get('created_at')]
    if dates:
        out.append(f"> **Periodo:** {dates[0]} → {dates[-1]}  ·  **Mensajes:** {len(rows)}  ·  *Archivado desde el historial de Antigravity.*\n")
    out.append("---\n")
    cur_day = None
    for r in rows:
        typ = r.get('type')
        src = r.get('source', '')
        day = r.get('created_at', '')[:10]
        tm = r.get('created_at', '')[11:16]
        if day and day != cur_day:
            cur_day = day
            out.append(f"\n## 📅 {day}\n")
        c = r.get('content', '')
        c = c if isinstance(c, str) else json.dumps(c, ensure_ascii=False)
        c = c.replace('\\n', '\n')
        if typ == 'USER_INPUT':
            t = clean_user(c)
            if t:
                out.append(f"\n**🧑 Didier · {tm}**\n\n{t}\n")
        elif typ == 'PLANNER_RESPONSE':
            if 'tool_calls' in r and not c.strip():
                tcs = r.get('tool_calls', [])
                names = ", ".join(tc.get('name', '?') for tc in tcs)
                out.append(f"\n<sub>🔧 *{tm} · acción: {names}*</sub>\n")
            elif c.strip():
                trunc = ""
                m = re.search(r'<truncated (\d+) bytes>', c)
                if m:
                    c = c.split('<truncated')[0].rstrip()
                    trunc = f"\n\n> ⚠️ *(respuesta truncada en el log original — ~{m.group(1)} bytes omitidos)*"
                out.append(f"\n**🤖 Asistente · {tm}**\n\n{c}{trunc}\n")
        elif typ in ('VIEW_FILE', 'LIST_DIRECTORY', 'RUN_COMMAND', 'GREP_SEARCH', 'CODE_ACTION'):
            first = c.strip().split('\n')[0][:120]
            out.append(f"\n<sub>📄 *{tm} · {typ}: {first}*</sub>\n")
        elif typ == 'SYSTEM_MESSAGE':
            continue
    return "\n".join(out)

if __name__ == '__main__':
    src, dst, title, intro = sys.argv[1], sys.argv[2], sys.argv[3], sys.argv[4]
    md = to_md(src, title, intro)
    open(dst, 'w', encoding='utf-8').write(md)
    print(f"OK {dst} ({len(md)} chars)")
