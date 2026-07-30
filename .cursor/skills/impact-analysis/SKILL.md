# Impact Analysis

## Objetivo

Mapear o impacto de uma alteração na superfície pública, models compartilhados e integração eNotas antes de editar.

## Quando usar

- Novo campo em model
- Mudança em `RestService` ou autenticação
- Alteração de método existente no client
- Bump de versão / possível breaking change
- Renomeação ou remoção de membro público

## Entradas esperadas

- Descrição da mudança pretendida
- Arquivos candidatos
- Se a mudança é aditiva ou breaking

## Arquivos e áreas que devem ser analisados

- `eNotasClient.cs` (métodos irmãos NFe/NFCe)
- Models referenciados (`Nota`, `Iten`, `Impostos`, etc.)
- `RestService.cs` se transporte mudar
- `README.md` e `.csproj` (Version)
- Postman V2
- Usos internos via busca no repositório (`Grep`)

## Passo a passo

1. Executar Task Preflight.
2. Listar símbolos públicos afetados.
3. Buscar referências no repo.
4. Verificar paridade NF-e/NFC-e.
5. Classificar: aditivo / breaking / apenas interno.
6. Listar riscos fiscais e de consumidores NuGet.
7. Recomendar ordem de alteração e validações.
8. Só então editar.

## Checklist de segurança

- [ ] Superfície pública mapeada
- [ ] Paridade NFe/NFCe avaliada
- [ ] Breaking change explícito se houver
- [ ] Secrets não envolvidos
- [ ] Validação humana se sensível

## Resultado esperado

Relatório curto: impacto, arquivos, riscos, plano de mudança, necessidade de revisão humana.

## Como validar

Nenhum consumidor óbvio (método/model irmão) foi esquecido na lista de impacto.

## Sinais de alerta

- Mudança em `JsonProperty` existente
- Mudança em serialização de datas
- Mudança em header Authorization
- Remoção de propriedade

## Agents recomendados

- project-architect
- library-specialist
- integration-specialist
- production-safety-officer
- qa-reviewer
