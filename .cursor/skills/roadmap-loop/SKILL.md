---
name: roadmap-loop
description: >-
  Orquestra um loop plan→exec sobre docs/ROADMAP.md: subagent planner
  (cursor-grok-4.5-high-fast) escolhe um item; executor (Auto / sem model)
  implementa via roadmap-implement; repete até esgotar Agente-pronto.
  Use when the user asks for roadmap loop, /roadmap-loop, fechar backlog
  em série, ou plan+exec até completar o ROADMAP.
---

# Roadmap Loop

## Objetivo

Fechar itens **Aberto** + **Agente-pronto** do `docs/ROADMAP.md` em série, **um ID por ciclo**, com planejamento e execução separados por model.

## Quando usar

- Usuário pede loop / série / “feche o roadmap” / `/roadmap-loop`
- Quer plan (Grok) + exec (Auto) até esgotar backlog executável
- Não usar para um único ID conhecido → preferir só `roadmap-implement`

## Modelos (fixo)

| Papel | Model no `Task` | Função |
|-------|-----------------|--------|
| **Planner** | `cursor-grok-4.5-high-fast` | Só escolher e planejar **um** ID; **não** editar arquivos |
| **Executor** | omitir `model` (Auto / herda o pai) | Implementar o plano via skill `roadmap-implement` |

O orquestrador (agente pai) **não** implementa código no ciclo — só lança Task, valida gates de stop e anuncia progresso. Se `Task` indisponível, o pai pode fazer o papel do executor (Auto), mas o planner continua via Task com o model acima.

## Entradas

- Pedido do usuário (autoriza o loop + commits por item, como em `roadmap-implement`)
- Limite opcional: `max_ciclos` (default **20**)
- Filtro opcional: só P1 / só P2 / INFRA
- Aprovação explícita **só** se for preciso tocar item **Bloqueado**

## Stop (parar o loop)

Parar e reportar ao usuário quando:

1. Não há mais ID **Aberto** + **Agente-pronto** com Depends ok
2. Próximo candidato é **Bloqueado** ou está em “Itens que exigem validação humana”
3. Planner retorna `STOP` / sem item seguro
4. Executor falha gate (`dotnet test` vermelho, sem evidência, breaking) e não recupera em 1 retry
5. Atingiu `max_ciclos`
6. Usuário pediu stop

**Não** afirmar “roadmap 100% completo” se restarem Bloqueado, Inferência sem ID, ou gaps P0/P3 sem linha Agente-pronto.

## Passo a passo

### 0. Bootstrap

1. Ler esta skill + `docs/ROADMAP.md` (estado + backlog + validação humana).
2. Anunciar: max_ciclos, models (plan/exec), ordem sugerida do ROADMAP, que P2-12 e similares serão pulados/stop.
3. `ciclo = 0`.

### 1. Ciclo — Plan

4. `ciclo += 1`. Se `ciclo > max_ciclos` → stop.
5. Lançar **um** `Task` com:
   - `model`: `cursor-grok-4.5-high-fast`
   - `subagent_type`: `generalPurpose` (ou `explore` se só leitura ampla)
   - `run_in_background`: `false` (aguardar plano)
   - `prompt`: template **Planner** abaixo (colar o ROADMAP atual ou pedir leitura do arquivo)
6. Validar plano:
   - Exatamente **um** ID
   - Status Aberto + Agente-pronto (ou gap P0/P3 concreto sem Bloqueado)
   - Depends satisfeitos (Feito) ou planner pediu implementar Depends primeiro → aceitar o Depends como item do ciclo
   - Sem pedido de editar código no plano
7. Se plano inválido → um retry de plan; se falhar de novo → stop.

### 2. Ciclo — Exec

8. Lançar **um** `Task` **sem** `model` (Auto):
   - `subagent_type`: `generalPurpose`
   - `run_in_background`: `false`
   - `prompt`: template **Executor** abaixo + plano JSON/markdown do planner
9. Conferir resultado: critério do ROADMAP marcado, testes Release, docs, commit (hash).
10. Se falhou → um retry de exec com o mesmo plano + erro; se falhar de novo → stop.

### 3. Continuar

11. Relatar ao usuário em 3–6 linhas: ID fechado, commit, próximo candidato.
12. Voltar ao passo 1 **sem** esperar novo pedido (salvo stop).
13. Opcional: skill `/loop` só como heartbeat se a sessão dormir; o fluxo preferido é ciclos síncronos plan→exec na mesma conversa.

## Template — Planner (colar no `Task` prompt)

```
Você é o PLANNER do eNotas.Sharp. NÃO edite arquivos. NÃO rode git commit. NÃO implemente.

Leia e siga a skill .cursor/skills/roadmap-implement/SKILL.md (só a parte de seleção + evidência).
Leia docs/ROADMAP.md completo.

Escolha EXATAMENTE UM item atômico executável agora:
- Preferência: P0 gap concreto aberto (Fato) → INFRA aberto → menor ID Aberto+Agente-pronto em P2 → depois P1 → P3 pontual
- Respeite Depends (se Depends aberto, escolha o Depends, não o item filho)
- NUNCA escolha Bloqueado ou “Validação humana” sem aprovação humana nesta conversa (não há)
- NUNCA planeje um P inteiro

Confirme evidência: Postman V2 (NF-e/NFC-e/empresas) ou V1 (só NFS-e/P1) + KB se o item citar KB.
Não invente path/campo.

Responda APENAS neste formato:

# Roadmap Plan
## status: OK | STOP
## item_id: (ex. P2-06 ou STOP)
## citacao_roadmap: (uma linha)
## prioridade: INFRA|P0|P1|P2|P3
## classificacao: Agente-pronto|...
## depends: (lista ou —)
## depends_ok: sim|nao
## evidencia: (Postman path / KB / arquivo)
## arquivos_candidatos:
- path
## criterio_aceite: (checklist do item)
## notas_execucao: (passos mínimos, sem código)
## motivo_stop: (só se status STOP)
```

## Template — Executor (colar no `Task` prompt)

```
Você é o EXECUTOR do eNotas.Sharp. Implemente UM item do roadmap de ponta a ponta.

Siga integralmente a skill .cursor/skills/roadmap-implement/SKILL.md
(incluindo task-preflight, evidência, impact-analysis, feature-development ou bugfix,
dotnet test eNotas.Sharp.sln -c Release, documentation-update, git-commit).

Plano aprovado pelo planner (não mude de ID sem Depends obrigatório):

---
{{COLAR_PLANO_AQUI}}
---

Regras:
- Escopo = só este item_id (+ arquivos necessários)
- Não inventar endpoints/campos
- Não implementar Bloqueado
- Sem secrets / API Key real
- Ao terminar, responda no formato “Resultado esperado” da skill roadmap-implement
  (Item, Evidência, Mudanças, Critério, Validação, Docs, Commit hash, Próximo sugerido)
```

## Checklist de segurança

- [ ] Um ID por ciclo
- [ ] Planner = `cursor-grok-4.5-high-fast`, sem edits
- [ ] Executor = sem `model` (Auto), via `roadmap-implement`
- [ ] Bloqueado / validação humana → stop
- [ ] Depends respeitado
- [ ] Test Release verde antes do commit de cada ciclo
- [ ] Diff por ciclo limitado ao item
- [ ] max_ciclos respeitado

## Resultado esperado (fim do loop)

```
# Roadmap Loop
## Ciclos executados
## Itens fechados (ID + commit)
## Parou porque
## Itens restantes (Aberto / Bloqueado)
## Riscos / validação humana pendente
```

## Sinais de alerta

- Planner ou executor fechando vários IDs num ciclo
- Executor ignorando o plano e pegando outro épico
- Continuar após P2-12 / Bloqueado sem aprovação
- Usar Postman V1 como contrato de paths V2 (e o inverso)
- Commit sem `dotnet test` Release
- Loop infinito sem releitura do ROADMAP entre ciclos

## Skills / agents relacionados

- `roadmap-implement` (execução de um item)
- `task-preflight`, `impact-analysis`, `feature-development`, `documentation-update`, `git-commit`
- Agents: `notagateway-docs-specialist`, `library-specialist`, `integration-specialist`, `qa-reviewer`, `documentation-maintainer`
