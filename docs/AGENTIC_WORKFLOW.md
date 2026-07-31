# Workflow Agentico

## Como agentes devem trabalhar neste projeto

Este repositório é uma **biblioteca cliente NuGet** pequena. Agentes devem priorizar:

1. Não quebrar contratos públicos.
2. Não inventar endpoints ou campos sem evidência (código, Postman ou [KB NotaGateway](https://atendimento.notagateway.com.br/kb/pt-br)).
3. Tratar fluxos fiscais como sensíveis.
4. Não alterar código funcional quando a tarefa for só documentação/governança.
5. Manter a estrutura de governança enxuta — sem burocracia extra.

## Ordem recomendada de análise

1. Task Preflight
2. `docs/PROJECT_OVERVIEW.md` + `ARCHITECTURE.md`
3. `eNotasClient.cs` e models afetados
4. Postman V2 em `docs/` e/ou KB NotaGateway (se integração/contrato/regra fiscal)
5. Agent Router → escolher agent/skill (`notagateway-docs-specialist` para consulta à KB)
6. Implementar mudança mínima
7. `dotnet test eNotas.Sharp.sln -c Release` (se código)
8. Skill `documentation-update` se superfície/docs mudarem
9. Skill `git-commit` (revisar diff; commit só se o humano pedir)

## Como executar Task Preflight

Seguir `.cursor/skills/task-preflight/SKILL.md` e produzir o bloco:

- Objetivo entendido
- Escopo provável
- Riscos iniciais
- Restrições
- Próxima ação

## Como escolher agents

Ver `.cursor/agents/agent-router.md`.

Resumo rápido:
- Ambiguidade / estrutura → `project-architect`
- Regra fiscal / impacto de negócio → `domain-analyst`
- Client, models, RestService → `library-specialist`
- Contrato API / Postman → `integration-specialist`
- Documentação oficial Gateway / rejeições / campos → `notagateway-docs-specialist`
- Validação → `qa-reviewer`
- Emissão/cancelamento/inutilização/auth/versão NuGet → incluir `production-safety-officer`
- Docs/rules/skills → `documentation-maintainer`

## Como escolher skills

| Situação | Skill |
|----------|--------|
| Qualquer tarefa | `task-preflight` |
| Medir impacto | `impact-analysis` |
| Novo endpoint/campo | `feature-development` |
| Item do `docs/ROADMAP.md` | `roadmap-implement` |
| Correção | `bugfix-safe-workflow` |
| Mudança HTTP/contrato | `integration-change-review` |
| Consultar KB NotaGateway | `notagateway-kb-lookup` |
| Atualizar docs | `documentation-update` |
| Preparar commit | `git-commit` |

## Como aplicar rules

Rules em `.cursor/rules/` — especialmente:

- `00-project-context` — sempre
- `30-domain-fiscal-rules` — models/client fiscal
- `55-notagateway-kb` — consultar KB oficial antes de inventar contrato
- `50-integration-safety` — RestService/API
- `90-production-safety` — publish, secrets, ops fiscais
- `95-git-and-change-management` — antes de concluir

## Como revisar alterações

```powershell
git status
git diff
dotnet test eNotas.Sharp.sln -c Release
```

Classificar mudanças: documentação · contrato público · transporte · versão NuGet.

## Como preparar commits

Usar `.cursor/skills/git-commit/SKILL.md`.

Não misturar, sem intenção explícita:
- governança (docs/cursor) com
- alteração de models fiscais WIP

## Quando pedir revisão humana

- Cancelamento, inutilização, emissão, CC-e
- Mudança de autenticação ou base URL
- Breaking change de model/client
- Publicação NuGet
- Remoção de arquivos públicos
- Dúvida sobre contrato da API

## Como documentar alterações

1. Código refletido em `MODULES.md` / `DOMAIN.md` se mudar responsabilidade.
2. Novo método → `README.md`.
3. Novo padrão de trabalho agentico → este arquivo + router.
4. Usar skill `documentation-update`.

## Checklist padrão para qualquer tarefa

- [ ] Preflight feito
- [ ] Agent/skill corretos
- [ ] Código lido antes de editar
- [ ] Sem secrets
- [ ] `dotnet test` OK (se código)
- [ ] Docs atualizados se necessário
- [ ] Diff revisado
- [ ] Commit apenas se solicitado

## Como manter esta estrutura atualizada

- `documentation-maintainer` revisa docs quando a API pública mudar.
- Não criar rules/skills/agents para stacks inexistentes (frontend, DB, etc.).
- Preferir editar arquivos existentes a duplicar.
