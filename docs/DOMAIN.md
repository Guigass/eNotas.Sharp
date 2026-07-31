# Domínio e Regras de Negócio

## Principais entidades/conceitos

| Conceito | Onde aparece | Notas |
|----------|--------------|--------|
| Empresa | Path `{empresaId}` | Identificador fornecido pela eNotas |
| Nota (NF-e/NFC-e) | `Models/Nota.cs` | Payload de emissão compartilhado; opcionais: `tipo`, `forcarEmissaoContingencia`, `emitidaEmContingencia`, `enviarPorEmail` (request; retorno em `Consulta` usa `enviadaPorEmail`) |
| Item | `Models/Iten.cs` | CFOP, NCM, valores, impostos; opcionais Postman V2: `extipi`, `codigoBeneficioFiscal`, `quantidadeTributavel`, `unidadeMedidaTributavel`, `valorTotal` (item) |
| Impostos | `Models/Impostos.cs`, `Models/IbsCbs.cs`, `Models/ServicoIbsCbs.cs` | ICMS, PIS, COFINS, IPI, II; IBS/CBS NF-e/NFC-e tipado em `IbsCbs` (KB 595993: `classificacaoTributaria`, `ibs`/`cbs` aninhados); NFS-e Reforma em `Servico.IbsCbs` como `ServicoIbsCbs` (`classificacaoTributaria` + `codigoIndicadorOperacao` — sample Sandbox - Reforma / KB 595993 §2; distinto de `IbsCbs`) |
| Cliente | `Models/Cliente.cs` | Destinatário |
| Pedido / Pagamento | `Pedido.cs`, `Pagamento.cs`, `Forma.cs` | Presença, formas, credenciadora, intermediador |
| Transporte | `Transporte.cs` e correlatos | Frete, volumes, veículo, transportadora |
| Inutilização | `Inutilizacao.cs` | Série e faixa numérica |
| Carta de Correção | `CartaCorrecao.cs` | Evento CC-e (NF-e) |
| Consulta | `Consulta.cs` | Status, chave, `linkDanfe` (URL do PDF/DANFE NF-e/NFC-e — não há `GET .../pdf` na API V2), `linkDownloadXml`, protocolo |
| NFS-e | `Nfse.cs`, `Servico.cs`, `ConsultaNfse.cs`, `ListaNfse.cs` | Emissão/consulta/lista V1 (`/v1/.../nfes`); request `enviarPorEmail` (KB 170286); retorno `enviadaPorEmail` em `ConsultaNfse`; ≠ `Nota`/`Consulta` V2 |
| ConsultaNfse | `ConsultaNfse.cs` | Response V1: `motivoStatus` é **string** (portal eNotas Resultado GET; ≠ `Consulta.MotivoStatus` `object` da V2). Campos evidenciados no Resultado oficial: `id`, `tipo`, `idExterno`, `status`, `motivoStatus`, `cliente`, `servico`, `valorTotal`, `enviadaPorEmail`, `numero`, `codigoVerificacao`, `chaveAcesso`, `linkDownloadPDF`/`XML`. Residual sem sample Postman de response: `naturezaOperacao`, `valorIss`, `deducoes`, `descontos`, `descontoCondicionado`, `observacoes`, `dataCompetenciaRps`, `rpsGerenciado`, datas auxiliares — permanecem nullable; não remover sem major |
| Webhook | `NotaWebhook.cs` | Payload tipado para o consumidor |

## Fluxos de negócio

### Emissão NF-e / NFC-e (API V2)
1. Montar `Nota` (ambiente, natureza, cliente, itens, impostos, pagamento, etc.).
2. Chamar `EmitirNfe` ou `EmitirNfce` com `empresaId`.
3. Avaliar `ApiResponse.IsSuccess`, `Status`, `Message`.

**Sensibilidade:** alta — gera documento fiscal.

### Emissão / ciclo NFS-e (API V1)
1. Montar `Nfse` + `Servico` (não usar `Nota` de mercadoria).
2. `EmitirNfse` → `POST /v1/empresas/{empresaId}/nfes`.
3. Consultar: `ConsultaNfse` / `ConsultaNfsePorIdExterno`; listar: `ListarNfse`.
4. XML: `ConsultaNfseXML` / `ConsultaNfseXMLPorIdExterno` → string em `ApiResponse.Message` ([KB 173803](https://atendimento.notagateway.com.br/kb/pt-br/article/173803/baixar-o-pdf-ou-xml-de-uma-nota-fiscal)).
5. PDF: `ConsultaNfsePDF` / `ConsultaNfsePDFPorIdExterno` → `byte[]` em `Object`.
6. Cancelar: `CancelaNfse` / `CancelaNfsePorIdExterno` (DELETE V1).
7. Apoio municipal (read-only): `ConsultaServicosMunicipais`, `ConsultaServicosMunicipaisUnificados`, `ConsultaProvedorCidade`, `CriticarDadosObrigatorios` — body em `Message`.

**Fato:** paths V1 `nfes` ≠ V2 `nf-e`/`nfc-e`. Inventário e exemplo no README (seção NFS-e).

**Sensibilidade:** alta — gera/cancela documento de serviço.

### Consulta (NF-e / NFC-e)
- Status/dados: `ConsultaNfe` / `ConsultaNfce` → `ApiResponse<Consulta>`
- XML autorizado: `ConsultaNfeXML` / `ConsultaNfceXML`
- XML cancelamento: `ConsultaNfeXMLCancelamento` / `ConsultaNfceXMLCancelamento`

### Cancelamento (NF-e / NFC-e)
`CancelaNfe` / `CancelaNfce` via HTTP DELETE (V2 `nf-e`/`nfc-e`).

**Sensibilidade:** alta — efeito fiscal.

### Inutilização
`InutilizacaoNfe` / `InutilizacaoNfce` + consultas e XML.

**Sensibilidade:** alta — inutiliza numeração.

### Carta de Correção (NF-e)
`CartaDeCorrecao`, `ConsultaCartaDeCorrecao`, `ConsultaCartaDeCorrecaoXml`.

**Sensibilidade:** média/alta — evento fiscal vinculado à chave.

## Regras explícitas encontradas

- Autenticação por API Key no client (**fato observado** no `RestService`).
- Paths NF-e/NFC-e em `/v2/empresas/.../nf-e|nfc-e/...`; NFS-e em `/v1/empresas/.../nfes...` (**fato observado**).
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
