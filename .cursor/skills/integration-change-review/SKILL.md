# Integration Change Review

## Objetivo

Revisar com segurança mudanças que afetam o contrato com a API eNotas.

## Quando usar

- Editar `RestService`
- Mudar headers, base URL, serialização
- Alterar paths ou verbos HTTP
- Mudar interpretação JSON vs XML
- Alinhar código ao Postman

## Entradas esperadas

- Diff pretendido ou descrição da mudança
- Métodos do client afetados
- Amostras de request/response se disponíveis (redigidas)

## Arquivos e áreas que devem ser analisados

- `eNotas.Sharp/Services/RestService.cs`
- `eNotas.Sharp/Clients/eNotasClient.cs`
- `docs/API - eNotas - V2 - NF-e - NFC-e.postman_collection.json`
- `ApiResponse.cs`
- Helpers de data

## Passo a passo

1. Task Preflight (risco elevado se auth/URL).
2. Listar todos os callers de `Post`/`Get`/`Put`/`Delete`.
3. Diff mental: antes vs depois do contrato HTTP.
4. Conferir Postman V2.
5. Verificar efeitos colaterais em XML e JSON.
6. Exigir validação humana se auth, URL ou serialização global mudar.
7. Só aplicar mudança mínima após revisão.
8. Build + checklist em `docs/TESTING.md`.

## Checklist de segurança

- [ ] Callers mapeados
- [ ] Postman V2 comparado
- [ ] Auth intacta ou mudança aprovada
- [ ] Sem secrets no diff
- [ ] Revisāo humana se necessário

## Resultado esperado

Parecer de risco + lista de callers + go/no-go + mudanças aplicadas se aprovado pelo fluxo.

## Como validar

Nenhum método do client ficou com path/verbo inconsistente; build OK.

## Sinais de alerta

- Mudança no header Authorization
- Concatenação de URL inconsistente
- Catch que engole falhas de parse
- Usar V1 NFS-e como referência do client atual

## Agents recomendados

- integration-specialist
- library-specialist
- production-safety-officer
- qa-reviewer
