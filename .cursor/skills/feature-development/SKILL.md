# Feature Development

## Objetivo

Adicionar endpoint, model ou campo seguindo os padrões reais do eNotas.Sharp.

## Quando usar

- Novo método em `eNotasClient`
- Novo model ou propriedade
- Suporte a novo retorno XML/JSON
- Evolução listada no README (futuros) quando for implementada de fato

## Entradas esperadas

- Endpoint/path desejado
- Evidência (Postman V2 ou doc oficial)
- Request/response esperados
- Escopo NF-e, NFC-e ou ambos

## Arquivos e áreas que devem ser analisados

- Postman V2
- `eNotasClient.cs`
- Models similares existentes
- `ApiResponse.cs`
- `README.md`, `.csproj`

## Passo a passo

1. Task Preflight + impact-analysis.
2. Consultar KB NotaGateway (`notagateway-kb-lookup`) e confirmar contrato no Postman V2 (não inventar).
3. Criar/estender models com `JsonProperty` + `NullValueHandling.Ignore`.
4. Adicionar método async na região correta do client.
5. Reutilizar `Post`/`Get`/`Delete`.
6. Atualizar README (métodos disponíveis).
7. Avaliar `Version` no csproj.
8. `dotnet build -c Release`.
9. documentation-update se arquitetura/domínio mudou.
10. git-commit (preparar; commit se o usuário pedir).

## Checklist de segurança

- [ ] Contrato evidenciado
- [ ] Sem breaking change acidental
- [ ] Sem API Key em exemplos
- [ ] Build OK
- [ ] README alinhado

## Resultado esperado

Código no padrão do projeto + docs mínimas atualizadas + resumo de validação.

## Como validar

Build OK; assinatura consistente com métodos irmãos; nomes JSON conferidos.

## Sinais de alerta

- Endpoint só existe no Postman V1 (NFS-e) — pode ser fora do escopo atual
- Requer mudança em `RestService`
- Campo obrigatório novo em model já publicado

## Agents recomendados

- library-specialist
- integration-specialist
- domain-analyst
- qa-reviewer
- documentation-maintainer
