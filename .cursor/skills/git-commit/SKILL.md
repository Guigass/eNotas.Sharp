# Git Commit

## Objetivo

Padronizar a criação de commits seguros, pequenos, revisáveis e alinhados às alterações reais feitas no projeto.

## Quando usar

- Tarefa concluída com arquivos criados/alterados/removidos
- Preparar commit para revisão humana
- Separar mudanças grandes em commits menores
- Alteração em documentação, rules, skills, agents, código ou `.csproj`

## Entradas esperadas

- Objetivo da tarefa
- Lista de arquivos alterados
- Resumo das mudanças
- Riscos ou áreas sensíveis
- Testes/validações executadas
- Pendências / validação humana

## Arquivos e áreas que devem ser analisados

- `git status`
- `git diff`
- `git diff --staged`
- Arquivos criados/alterados/removidos
- Docs, rules, skills, agents impactados

## Passo a passo

1. `git status`
2. `git diff` e `git diff --staged`
3. Separar mudanças não relacionadas (ex.: governança vs models fiscais WIP).
4. Confirmar ausência de secrets, `.env`, tokens, nupkgs acidentais.
5. Confirmar que não há alteração funcional inesperada.
6. Verificar se docs/rules precisam atualizar.
7. Verificar build/validação quando aplicável.
8. Identificar padrão de commit do repo (histórico informal PT-BR).
9. Preparar mensagem clara; recomendar Conventional Commits para commits novos.
10. **Criar commit somente se o usuário pedir explicitamente.**
11. Informar o que foi validado e o que precisa de revisão humana.

## Padrão de mensagem recomendado

```
<tipo>(escopo opcional): resumo curto no imperativo
```

Tipos: `feat`, `fix`, `docs`, `refactor`, `test`, `chore`, `build`, `ci`, `perf`, `style`

Exemplos:

```
docs: add project governance documentation
docs(cursor): add agent router and operational skills
feat(models): add optional ibsCbs tax field
chore(nuget): bump package version to 1.4.5
fix(client): correct inutilizacao XML path
```

## Checklist de segurança

- [ ] Apenas mudanças relacionadas
- [ ] Sem secrets
- [ ] Sem artefatos temporários
- [ ] Diff revisado
- [ ] Mensagem descreve a mudança real
- [ ] Validações aplicáveis feitas
- [ ] Áreas sensíveis marcadas
- [ ] Docs atualizados ou pendência explícita

## Resultado esperado

- Resumo das alterações
- Lista de arquivos do commit
- Mensagem recomendada
- Validações executadas
- Riscos/pendências
- Indicação: pronto / precisa revisão humana / aguardando pedido de commit

## Como validar

`git status`, `git diff`, `git diff --staged`, build se código, revisão manual do diff.

## Sinais de alerta

- Auth, base URL, fluxos fiscais críticos
- Publicação/versão NuGet
- Remoção de arquivos
- Grande volume em um commit
- Possível segredo
- Mistura de WIP de models com docs sem intenção

## Agents recomendados

- documentation-maintainer
- qa-reviewer
- project-architect
- production-safety-officer (risco elevado)
