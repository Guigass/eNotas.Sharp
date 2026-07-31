# Domínio e Regras de Negócio

## Principais entidades/conceitos

| Conceito | Onde aparece | Notas |
|----------|--------------|--------|
| Empresa | Path `{empresaId}` | Identificador fornecido pela eNotas |
| Nota (NF-e/NFC-e) | `Models/Nota.cs` | Payload de emissão compartilhado; opcionais: `tipo`, `forcarEmissaoContingencia`, `emitidaEmContingencia`, `enviarPorEmail` |
| Item | `Models/Iten.cs` | CFOP, NCM, valores, impostos; opcionais Postman V2: `extipi`, `codigoBeneficioFiscal`, `quantidadeTributavel`, `unidadeMedidaTributavel`, `valorTotal` (item) |
| Impostos | `Models/Impostos.cs` | ICMS, PIS, COFINS, IPI, II, IBS/CBS |
| Cliente | `Models/Cliente.cs` | Destinatário |
| Pedido / Pagamento | `Pedido.cs`, `Pagamento.cs`, `Forma.cs` | Presença, formas, credenciadora, intermediador |
| Transporte | `Transporte.cs` e correlatos | Frete, volumes, veículo, transportadora |
| Inutilização | `Inutilizacao.cs` | Série e faixa numérica |
| Carta de Correção | `CartaCorrecao.cs` | Evento CC-e (NF-e) |
| Consulta | `Consulta.cs` | Status, chave, links DANFE/XML, protocolo |
| Webhook | `NotaWebhook.cs` | Payload tipado para o consumidor |

## Fluxos de negócio

### Emissão NF-e / NFC-e
1. Montar `Nota` (ambiente, natureza, cliente, itens, impostos, pagamento, etc.).
2. Chamar `EmitirNfe` ou `EmitirNfce` com `empresaId`.
3. Avaliar `ApiResponse.IsSuccess`, `Status`, `Message`.

**Sensibilidade:** alta — gera documento fiscal.

### Consulta
- Status/dados: `ConsultaNfe` / `ConsultaNfce` → `ApiResponse<Consulta>`
- XML autorizado: `ConsultaNfeXML` / `ConsultaNfceXML`
- XML cancelamento: `ConsultaNfeXMLCancelamento` / `ConsultaNfceXMLCancelamento`

### Cancelamento
`CancelaNfe` / `CancelaNfce` via HTTP DELETE.

**Sensibilidade:** alta — efeito fiscal.

### Inutilização
`InutilizacaoNfe` / `InutilizacaoNfce` + consultas e XML.

**Sensibilidade:** alta — inutiliza numeração.

### Carta de Correção (NF-e)
`CartaDeCorrecao`, `ConsultaCartaDeCorrecao`, `ConsultaCartaDeCorrecaoXml`.

**Sensibilidade:** média/alta — evento fiscal vinculado à chave.

## Regras explícitas encontradas

- Autenticação por API Key no client (**fato observado** no `RestService`).
- Paths versionados em `/v2/empresas/...` (**fato observado**).
- Campo `ambienteEmissao` nos modelos de emissão/inutilização (valores exatos **não documentados no código** — validar com API/Postman).
- Propriedades omitidas quando nulas (`NullValueHandling.Ignore`).

## Regras inferidas

- O mesmo modelo `Nota` serve NF-e e NFC-e; diferenças de regras ficam na API/gateway, não em tipos C# distintos.
- `consumidorFinal`, `finalidade`, `tipoOperacao`, `naturezaOperacao` influenciam o comportamento fiscal no gateway.
- Novos campos tributários (ex. IBS/CBS) acompanham evolução legislativa/API e devem permanecer opcionais para não quebrar clientes antigos.

## Pontos que precisam de validação humana

- Valores permitidos de enumeráveis enviados como `string`/`int` (CST, CFOP, presença do consumidor, etc.).
- Regras de prazo e elegibilidade para cancelamento e CC-e (não estão no código).
- Se NFC-e deve ou não aceitar os mesmos campos de transporte/referência que NF-e.

## Contingência (NF-e / NFC-e)

**Fato (KB NotaGateway):** `forcarEmissaoContingencia` (boolean) no request força emissão em contingência; omitir/`false` deixa o Gateway gerenciar. `emitidaEmContingencia` é campo de retorno, para identificar se houve contingência. Ambos existem em `Nota` e `Consulta` (nullable + Ignore).

## Áreas sensíveis

1. Qualquer alteração em `Emitir*`, `Cancela*`, `Inutilizacao*`, `CartaDeCorrecao`.
2. Alteração de nomes `JsonProperty` em models já publicados.
3. Mudança na serialização de datas (`CustomDateTimeConverter`).
4. Mudança no formato do header de autorização.
5. Bump de versão NuGet sem comunicar breaking changes.
