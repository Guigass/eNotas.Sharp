# NotaGateway KB Lookup

## Objetivo

Consultar de forma sistemática a Central de Ajuda oficial do Nota Gateway e transformar o resultado em evidência acionável para evolução ou correção do `eNotas.Sharp`.

## Quando usar

- Dúvida sobre campo, endpoint, status, rejeição ou fluxo fiscal
- Antes de adicionar propriedade/método alinhado à API
- Investigar erro retornado pela API/SEFAZ
- Esclarecer homologação, contingência, CC-e, inutilização, cancelamento, webhook, impostos
- Quando Postman/código não bastam para decidir

## Entradas esperadas

- Pergunta ou sintoma (ex.: “como informar ICMS no JSON?”, “rejeição ao cancelar”)
- Escopo: NF-e, NFC-e, NFS-e, empresa, SAT, consulta, etc.
- Contexto no repo (arquivo/método/model), se houver
- Trecho de erro/payload redigido (sem API Key / dados sensíveis)

## Arquivos e áreas que devem ser analisados

**Externos (obrigatório tentar):**

- Home: https://atendimento.notagateway.com.br/kb/pt-br
- Categorias Gateway (prefixo `https://atendimento.notagateway.com.br`):
  - `/kb/category/endpoints`
  - `/kb/category/gw-funcionalidades-da-api`
  - `/kb/category/gw-emissao-e-cancelamento`
  - `/kb/category/inutilizacao-e-cc-e`
  - `/kb/category/painel-gw`
  - `/kb/category/agente-enotas-sat-mf-e-primeiros-passos`
  - `/kb/category/agente-enotas-sat-mf-e-rejeicoes-e-erros`
  - `/kb/category/particularidade-de-municipios-nfs-e`
  - `/kb/category/consulta-primeiros-passos`
  - `/kb/category/consulta-erros-e-rejeicoes`

**Internos (confrontar depois):**

- `eNotas.Sharp/Clients/eNotasClient.cs`
- Models em `eNotas.Sharp/Models/`
- `docs/API - eNotas - V2 - NF-e - NFC-e.postman_collection.json`
- `docs/DOMAIN.md`, `docs/TROUBLESHOOTING.md`

## Passo a passo

1. Executar Task Preflight (escopo = consulta KB; risco baixo se só leitura).
2. Classificar o tema e escolher a categoria da tabela acima.
3. Abrir a home e/ou a categoria com fetch/navegação (`WebFetch` / browser).
4. Localizar o artigo pelo título (busca por palavras-chave: campo JSON, CST, rejeição, status, etc.).
5. Abrir o artigo e extrair:
   - regra ou procedimento
   - nomes de campos/tags citados
   - limitações e exceções
   - URL canônica
6. Confrontar com o código e o Postman V2:
   - campo já existe no model?
   - endpoint já existe no client?
   - nome JSON bate com `JsonProperty`?
7. Produzir o resultado no formato abaixo.
8. Se for implementar algo: encadear `impact-analysis` / `feature-development` e rule `55-notagateway-kb`.
9. Se não achar na KB: declarar **não encontrado** e sugerir ticket/portal — não inventar.

## Roteamento rápido por palavra-chave

| Palavras | Categoria sugerida |
|----------|-------------------|
| endpoint, XML, PDF, certificado, empresa, status da nota, Postman | `endpoints` |
| campo JSON, ICMS, PIS, COFINS, pagamento, email, FCP, DIFAL, indPres, finNFe | `gw-funcionalidades-da-api` |
| rejeição emissão/cancelamento, denegada | `gw-emissao-e-cancelamento` |
| inutilização, carta de correção, CC-e | `inutilizacao-e-cc-e` |
| painel, freemium, cadastro UI | `painel-gw` |
| SAT, MF-e, CSC, contingência SP | categorias agente SAT/MF-e |
| município, NFS-e, RPS, ISS prefeitura | `particularidade-de-municipios-nfs-e` / NFS-e |
| consulta nota, pooling, webhook consulta | `consulta-primeiros-passos` |

## Checklist de segurança

- [ ] Nenhuma API Key ou dado fiscal real colado na busca/evidência
- [ ] URLs dos artigos citados
- [ ] Distinção KB vs código vs inferência
- [ ] Gap de implementação no `eNotas.Sharp` explícito
- [ ] Sem alteração de código nesta skill (só consulta), salvo tarefa pedir implementação depois

## Resultado esperado

```markdown
# Consulta KB NotaGateway

## Pergunta
...

## Artigos consultados
- [Título](URL)

## O que a KB diz (fato)
...

## O que o código/Postman têm (fato)
...

## Lacunas / divergências
...

## Inferências
...

## Recomendação
- Só documentar / implementar campo / abrir ticket / validação humana

## Próximo agent/skill
...
```

## Como validar

- Pelo menos uma URL da KB foi aberta e citada.
- A resposta não afirma contrato sem fonte.
- Gaps NFS-e vs client C# não foram escondidos.

## Sinais de alerta

- Artigo fala de NFS-e/empresas e a tarefa assume que já existe no NuGet
- Divergência KB × model publicado (breaking risk)
- Artigo desatualizado ou ambíguo → validação humana
- Conteúdo atrás de login inacessível → registrar bloqueio e pedir humano

## Agents recomendados

- `notagateway-docs-specialist` (principal)
- `domain-analyst`
- `integration-specialist`
- `library-specialist` (se for implementar depois)
- `production-safety-officer` (se fluxo crítico)
