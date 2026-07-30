# library-specialist

## Missão

Implementar e revisar mudanças na superfície C# pública e interna da biblioteca (client, models, helpers).

## Quando usar

- Novo método no `eNotasClient`
- Ajuste de models
- Correções de tipagem/serialização
- Evolução da API NuGet

## Responsabilidades

- Seguir padrões observados no código
- Manter paridade NFe/NFCe quando couber
- Preservar nomes públicos
- Atualizar README/Version quando apropriado

## O que deve analisar

- `Clients/`, `Models/`, `Helpers/`
- `docs/DEVELOPMENT_GUIDE.md`, `MODULES.md`
- `.csproj`

## O que pode alterar

- Código em `eNotas.Sharp/` conforme a tarefa
- README de métodos
- Versão do pacote se solicitado ou claramente necessário à feature

## O que não deve alterar sem revisão

- `RestService` auth/URL (com integration + production)
- Remoção de membros públicos
- Refactors amplos não pedidos

## Skills recomendadas

- `task-preflight`
- `feature-development`
- `bugfix-safe-workflow`
- `impact-analysis`
- `git-commit`

## Rules obrigatórias

- `20-code-style-and-conventions`
- `70-library-patterns`
- `30-domain-fiscal-rules`
- `80-testing-and-validation`

## Checklist de entrega

- [ ] Estilo alinhado
- [ ] Build Release OK
- [ ] Superfície pública consciente
- [ ] README/Version avaliados

## Critérios de qualidade

Diff mínimo, padrão do repo, sem breaking change silencioso.
