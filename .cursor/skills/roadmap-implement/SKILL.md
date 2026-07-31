---
name: roadmap-implement
description: >-
  Seleciona um item do docs/ROADMAP.md e implementa no eNotas.Sharp
  (models, client, DX). Antes do commit: test Release, verificar estado do
  projeto e documentation-update. Use when the user asks to pegar/implementar
  parte do roadmap, avançar backlog P0/P1/P2/P3, ou fechar lacunas do ROADMAP.
---

# Roadmap Implement

## Objetivo

Pegar **uma** parte do `docs/ROADMAP.md` e implementá-la de ponta a ponta, com evidência de contrato e sem inventar endpoints/campos.

## Quando usar

- Usuário pede para implementar / avançar / pegar item do roadmap
- Escolher próximo gap priorizado (P0 → P3) sem escopo definido
- Fechar critério de aceite de uma linha/tabela do ROADMAP

## Entradas esperadas

- Pedido do usuário (item explícito **ou** “pegue uma parte”)
- Prioridade opcional: P0 / P1 / P2 / P3
- Aprovação humana se o item estiver marcado como **Validação humana**

## Arquivos e áreas que devem ser analisados

- `docs/ROADMAP.md` (fonte do backlog e critérios de aceite)
- Postman V2 (client atual) ou V1 (só se o item for NFS-e / P1)
- `eNotas.Sharp/Clients/eNotasClient.cs`
- Models em `eNotas.Sharp/Models/`
- `eNotas.Sharp/Services/RestService.cs` (só se o item exigir transporte)
- KB NotaGateway via skill `notagateway-kb-lookup`
- `README.md`, `.csproj` quando a superfície pública crescer

## Passo a passo

### 1. Preflight e seleção

1. Executar `task-preflight`.
2. Ler `docs/ROADMAP.md` completo (estado atual + backlog + “Itens que exigem validação humana”).
3. Escolher **um** item atômico (uma linha da tabela, um critério de aceite, ou um gap concreto — não um P inteiro).
4. Preferência automática se o usuário não indicar item:
   - P0 gaps concretos com evidência **Fato** e sem bloqueio de validação humana
   - Depois P3 correções pontuais (DX) sem breaking
   - P1 / P2 / itens com **Validação humana** só com aprovação explícita do usuário
5. Anunciar ao usuário, antes de editar código:
   - Item escolhido (citação do ROADMAP)
   - Prioridade (P0–P3)
   - Classificação (Fato / Inferência / Validação humana)
   - Arquivos candidatos
   - Se precisa de validação humana → **parar e perguntar**

### 2. Evidência e impacto

6. Confirmar contrato: Postman da versão correta + `notagateway-kb-lookup` quando for campo/fluxo fiscal.
7. Executar `impact-analysis` (aditivo vs breaking; paridade NF-e/NFC-e).
8. Encadear a skill de execução:
   - Novo método/model/campo → `feature-development`
   - Correção de bug/transporte/DX pontual → `bugfix-safe-workflow` e/ou `integration-change-review`
9. Não inventar path, verbo HTTP ou `JsonProperty` sem evidência.

### 3. Implementação

10. Implementar o mínimo necessário para o critério de aceite do item.
11. Atualizar `docs/ROADMAP.md`: marcar checkbox do critério atendido e/ou ajustar status da linha (não apagar histórico útil).

### 4. Gate obrigatório antes do commit (nesta ordem)

**Não executar `git-commit` até todos os passos abaixo passarem.**

12. **Rodar os testes** (ver `docs/TESTING.md` e rule `80-testing-and-validation`):
    - `dotnet test eNotas.Sharp.sln -c Release`
    - Se falhar: corrigir e repetir até verde; não commitar com suite vermelha.
    - Se a mudança exigir checklist manual (fluxo fiscal/integração), listar pendências no resultado — não inventar “passou” sem execução.
13. **Verificar o estado do projeto**:
    - `git status` e `git diff`: diff limitado ao item anunciado; sem secrets, `.env`, nupkgs ou arquivos fora do escopo.
    - Confirmar que assinaturas/models públicos seguem o padrão e que não houve breaking acidental.
    - Riscos residuais e validação humana explícitos no resultado.
    - Se `dotnet test` não for aplicável (ex.: mudança só em skill/docs de governança sem código), justificar no resultado e ao menos `dotnet build eNotas.Sharp.sln -c Release` quando houver qualquer alteração em `.cs`/`.csproj`.
14. **Atualizar as docs** — executar a skill `documentation-update`:
    - `docs/ROADMAP.md` já refletindo o critério fechado.
    - `README.md`, `docs/TESTING.md`, `docs/DEVELOPMENT_GUIDE.md`, `docs/ARCHITECTURE.md`, `docs/PROJECT_OVERVIEW.md` (e demais docs tocados) alinhados ao código real da mudança.
    - Não documentar endpoint/campo não implementado; não deixar docs contradizendo o estado pós-item.
15. Só então executar a skill `git-commit` e **criar o commit** com as mudanças do item (código + ROADMAP + docs relacionados + testes se houver). O uso desta skill implica pedido explícito de commit nesse fechamento.

## Checklist de segurança

- [ ] Um único item do ROADMAP (escopo atômico)
- [ ] Evidência Postman e/ou KB antes de codar
- [ ] Validação humana respeitada (não pular)
- [ ] Sem breaking change acidental NuGet
- [ ] Sem API Key / dados fiscais reais
- [ ] `dotnet test eNotas.Sharp.sln -c Release` OK
- [ ] Estado do projeto verificado (`git status` / diff no escopo / sem artefatos indesejados)
- [ ] Docs atualizados via `documentation-update` (ROADMAP + docs impactados)
- [ ] Commit criado via skill `git-commit` **somente após** testes + estado + docs

## Resultado esperado

```
# Roadmap Implement
## Item escolhido
## Evidência (Postman / KB / código)
## Mudanças
## Critério de aceite (antes → depois)
## Validação (testes / estado do projeto / riscos residuais)
## Docs atualizados
## Commit (hash / mensagem)
## Próximo item sugerido (opcional)
```

## Como validar

- Diff limitado ao item anunciado
- Critério de aceite do ROADMAP refletido no código e no próprio ROADMAP
- Assinaturas/models no padrão do projeto
- Suite Release executada e verde antes do commit
- Docs alinhados ao código (sem lacunas óbvias introduzidas pelo item)

## Sinais de alerta

- Tentar fechar um P inteiro numa sessão
- Item só com **Inferência** sem KB/Postman
- Item em “Itens que exigem validação humana” sem aprovação
- Usar Postman V1 como referência do client V2 atual
- Inventar endpoint ou renomear propriedade pública existente
- Commitar sem `dotnet test` Release, com testes falhando, ou sem `documentation-update`
- Afirmar que testes/docs estão OK sem ter executado o gate

## Agents recomendados

- `notagateway-docs-specialist` (contrato)
- `library-specialist` / `integration-specialist` (código)
- `domain-analyst` (fiscal)
- `qa-reviewer`
- `production-safety-officer` (emissão/cancelamento/auth/publish)
- `documentation-maintainer` (ROADMAP + README)
