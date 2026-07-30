---
name: roadmap-implement
description: >-
  Seleciona um item do docs/ROADMAP.md e implementa no eNotas.Sharp
  (models, client, DX). Use when the user asks to pegar/implementar parte do
  roadmap, avançar backlog P0/P1/P2/P3, ou fechar lacunas do ROADMAP.
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

### 3. Implementação e fechamento

10. Implementar o mínimo necessário para o critério de aceite do item.
11. `dotnet build -c Release`.
12. Atualizar `docs/ROADMAP.md`: marcar checkbox do critério atendido e/ou ajustar status da linha (não apagar histórico útil).
13. `documentation-update` (README / docs tocados pela mudança).
14. Ao final, executar a skill `git-commit` e **criar o commit** com as mudanças do item (código + ROADMAP + docs relacionados). O uso desta skill implica pedido explícito de commit nesse fechamento.

## Checklist de segurança

- [ ] Um único item do ROADMAP (escopo atômico)
- [ ] Evidência Postman e/ou KB antes de codar
- [ ] Validação humana respeitada (não pular)
- [ ] Sem breaking change acidental NuGet
- [ ] Sem API Key / dados fiscais reais
- [ ] Build Release OK
- [ ] ROADMAP atualizado para refletir o que foi feito
- [ ] Commit criado via skill `git-commit`

## Resultado esperado

```
# Roadmap Implement
## Item escolhido
## Evidência (Postman / KB / código)
## Mudanças
## Critério de aceite (antes → depois)
## Validação (build / riscos residuais)
## Commit (hash / mensagem)
## Próximo item sugerido (opcional)
```

## Como validar

- Diff limitado ao item anunciado
- Critério de aceite do ROADMAP refletido no código e no próprio ROADMAP
- Assinaturas/models no padrão do projeto

## Sinais de alerta

- Tentar fechar um P inteiro numa sessão
- Item só com **Inferência** sem KB/Postman
- Item em “Itens que exigem validação humana” sem aprovação
- Usar Postman V1 como referência do client V2 atual
- Inventar endpoint ou renomear propriedade pública existente

## Agents recomendados

- `notagateway-docs-specialist` (contrato)
- `library-specialist` / `integration-specialist` (código)
- `domain-analyst` (fiscal)
- `qa-reviewer`
- `production-safety-officer` (emissão/cancelamento/auth/publish)
- `documentation-maintainer` (ROADMAP + README)
