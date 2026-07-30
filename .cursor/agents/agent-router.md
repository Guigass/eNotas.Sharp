# Agent Router

## Objetivo

Escolher o melhor agent (ou combinação) para cada tarefa neste repositório.

## Agents disponíveis

| Agent | Missão |
|-------|--------|
| `project-architect` | Arquitetura, fronteiras, consistência estrutural |
| `domain-analyst` | Domínio fiscal NF-e/NFC-e e impacto de negócio |
| `library-specialist` | Client, models, helpers, API pública NuGet |
| `integration-specialist` | Contrato HTTP com eNotas, Postman, RestService |
| `qa-reviewer` | Validação, build, regressão, checklists |
| `production-safety-officer` | Riscos fiscais, secrets, publish NuGet |
| `documentation-maintainer` | docs, README, rules, skills, agents |
| `notagateway-docs-specialist` | Consulta à KB oficial NotaGateway e confronto com o código |

## Roteamento por tipo de tarefa

### Tarefa inicial ou ambígua

- Agents: `project-architect`, `documentation-maintainer`
- Skills: `task-preflight`, (leitura de `docs/PROJECT_OVERVIEW.md`)
- Risco: médio se escopo unclear
- Escalar: produção, auth, fiscal crítico, publish

### Dúvida de documentação / regra da API NotaGateway

- Agents: `notagateway-docs-specialist` (+ `domain-analyst` ou `integration-specialist` se for implementar)
- Skills: `task-preflight`, `notagateway-kb-lookup`
- Fonte: https://atendimento.notagateway.com.br/kb/pt-br
- Risco: baixo na consulta; elevado se virar mudança fiscal no client
- Escalar: `production-safety-officer` se emissão/cancelamento/inutilização/CC-e

### Implementar item do ROADMAP

- Agents: `notagateway-docs-specialist`, `library-specialist`, `integration-specialist`, `qa-reviewer`, `documentation-maintainer`
- Skills: `roadmap-implement` (orquestra `task-preflight`, evidência, `impact-analysis`, `feature-development` / `bugfix-safe-workflow`, `documentation-update`)
- Fonte: `docs/ROADMAP.md`
- Risco: conforme prioridade do item (P0 campos aditivos = médio; P1/P2/validação humana = elevado)
- Escalar: `production-safety-officer` se fiscal crítico, auth ou publish; parar se item exigir validação humana sem aprovação

### Novo campo ou model

- Agents: `notagateway-docs-specialist`, `library-specialist`, `domain-analyst`, `qa-reviewer`
- Skills: `task-preflight`, `notagateway-kb-lookup`, `impact-analysis`, `feature-development`
- Risco: médio; alto se imposto/emissão

### Novo endpoint no client

- Agents: `notagateway-docs-specialist`, `library-specialist`, `integration-specialist`, `qa-reviewer`
- Skills: `notagateway-kb-lookup`, `feature-development`, `integration-change-review`
- Escalar: `production-safety-officer` se emissão/cancelamento/inutilização

### Bug em serialização / parse

- Agents: `library-specialist`, `qa-reviewer`
- Skills: `bugfix-safe-workflow`, `impact-analysis`

### Bug / mudança em HTTP, auth, URL

- Agents: `integration-specialist`, `production-safety-officer`, `qa-reviewer`
- Skills: `integration-change-review`, `bugfix-safe-workflow`
- Risco: elevado

### Mudança em fluxo crítico (emitir/cancelar/inutilizar/CC-e)

- Agents: `notagateway-docs-specialist`, `domain-analyst`, `library-specialist`, `production-safety-officer`, `qa-reviewer`
- Skills: `notagateway-kb-lookup`, `impact-analysis`, `feature-development` ou `bugfix-safe-workflow`
- Risco: elevado — validação humana

### Rejeição, status ou erro da API/SEFAZ

- Agents: `notagateway-docs-specialist`, `integration-specialist`, `qa-reviewer`
- Skills: `notagateway-kb-lookup`, `bugfix-safe-workflow`
- Risco: médio a elevado

### Atualização de documentação / governança Cursor

- Agents: `documentation-maintainer`
- Skills: `task-preflight`, `documentation-update`
- Risco: baixo (não alterar código funcional)

### Preparar publicação NuGet / bump Version

- Agents: `library-specialist`, `production-safety-officer`, `qa-reviewer`, `documentation-maintainer`
- Skills: `impact-analysis`, `git-commit`
- Risco: elevado

### Preparar commit

- Agents: `documentation-maintainer`, `qa-reviewer`
- Skills: `git-commit`
- Risco: baixo a médio; escalar se fiscal/auth/versão

## Regras de combinação

- Sempre comece com mentalidade de `task-preflight`.
- Dúvida de contrato/regra do Gateway: comece por `notagateway-docs-specialist` + skill `notagateway-kb-lookup`.
- Se houver contrato API + model, combine `integration-specialist` + `library-specialist`.
- Se houver risco fiscal ou publish, inclua `production-safety-officer`.
- `documentation-maintainer` entra no fim se a superfície pública ou o fluxo agentico mudou.

## Escalonamento de risco

Chamar `production-safety-officer` quando:

- Auth / base URL
- Emissão, cancelamento, inutilização, CC-e
- Breaking change NuGet
- Publish / Version
- Possível exposição de secret

## Checklist antes de começar

- [ ] Preflight feito
- [ ] Agent(s) escolhidos
- [ ] Skills escolhidas
- [ ] Rules aplicáveis identificadas
- [ ] Critério de validação humana definido
