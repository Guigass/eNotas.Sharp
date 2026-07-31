# Roadmap eNotas.Sharp

Backlog de lacunas da library cliente em relação à API eNotas Gateway e à qualidade do SDK.

Este documento **não** descreve features já disponíveis como se estivessem prontas. NFS-e e gestão de empresas estão no Postman / README, mas **não** no `eNotasClient` atual.

## Como ler este documento

| Classificação | Significado |
|---------------|-------------|
| **Fato** | Evidência no código, Postman versionado em `docs/` ou README |
| **Inferência** | Conclusão razoável a partir das fontes; pode mudar com a API oficial |
| **Validação humana** | Precisa confirmação (KB NotaGateway, contrato estável, prioridade de negócio) antes de implementar |

## Estado atual (o que a lib já cobre)

**Fato:** `eNotasClient` expõe o ciclo operacional NF-e e NFC-e em `/v2/empresas/{empresaId}/...` (base `https://api.enotasgw.com.br`):

| Área | Métodos |
|------|---------|
| NF-e | Emitir, consultar, XML, cancelar, XML cancelamento, inutilização (+ consulta/XML), carta de correção (+ consulta/XML) |
| NFC-e | Emitir, consultar, XML, cancelar, XML cancelamento, inutilização (+ consulta/XML) |

Models públicos em `eNotas.Sharp/Models/` cobrem emissão (`Nota`, `Iten`, impostos, pagamento, transporte, etc.), consultas e XML fiscal.

**Fato:** não há métodos de NFS-e, empresas, certificado, logo, SAT nem manifestação no client.

## Backlog priorizado

### P0 — Completude do contrato NF-e / NFC-e

**Objetivo:** permitir preencher e serializar todos os campos opcionais do payload de emissão aceitos pela API, sem breaking change NuGet (somente adições nullable + `NullValueHandling.Ignore`).

#### Gaps concretos (Postman V2 × models)

| Área | Campo / tema | Situação |
|------|--------------|----------|
| Raiz `Nota` | `tipo` | **Feito:** presente em `Nota.cs` (e em `Consulta`); sample Postman V2 |
| Raiz `Nota` | `forcarEmissaoContingencia`, `emitidaEmContingencia` | **Feito:** presentes em `Nota.cs` (e já em `Consulta`); KB: request vs retorno |
| Raiz `Nota` | `indicadorPresencaConsumidor` | **Feito:** presente em `Nota.cs`; sample Postman V2 `Emitir NF-e - api11` (coexiste com `pedido.presencaConsumidor`) |
| Raiz `Nota` | `enviarPorEmail` vs `enviadaPorEmail` | **Feito:** request = `enviarPorEmail` ([KB 170286](https://atendimento.notagateway.com.br/kb/pt-br/article/170286/campo-enviarporemail)); retorno = `enviadaPorEmail` em `Consulta`. Sample Postman V2 com `enviadaPorEmail` no body de emissão diverge da KB |
| `itens[]` (`Iten`) | `codigoBeneficioFiscal`, `extipi`, `quantidadeTributavel`, `unidadeMedidaTributavel`, `valorTotal` | **Feito:** presentes em `Iten.cs`; sample Postman V2 Emitir NF-e |
| Impostos | `ibsCbs` tipado como `Imposto` genérico | **Feito:** tipo dedicado `IbsCbs` (`ibs.uf` / `ibs.municipio` / `cbs`) alinhado à [KB 595993](https://atendimento.notagateway.com.br/kb/pt-br/article/595993/como-enviar-ibs-e-cbs-ao-emitir-uma-nf-via-api); `Imposto` permanece para PIS/COFINS/IPI |
| Demais aninhados | cliente, pedido, pagamento, transporte, referências, impostos | **Inferência:** Postman é amostra incompleta; auditar V2 + [KB NotaGateway](https://atendimento.notagateway.com.br/kb/pt-br) para opcionais restantes |

#### Critério de aceite

- [x] Properties aditivas em `Nota`, `Iten` e aninhados alinhadas ao contrato oficial — parcial: `Nota.tipo` + contingência + `indicadorPresencaConsumidor` + campos `Iten` + `enviarPorEmail` (request) + `IbsCbs` dedicado (KB 595993); demais aninhados/opcionais ainda em auditoria
- [x] Paridade considerada para NF-e e NFC-e (mesmo model compartilhado) — `Nota`/`Iten` compartilhados; campos aditivos cobrem ambos
- [x] Sem remoção/renomeação de propriedades públicas existentes
- [x] README / versão NuGet atualizados quando a superfície pública crescer — Version **1.5.0** (minor: tipo público `IbsCbs` no lugar de `Imposto` em `Impostos.IbsCbs`)

### P1 — NFS-e (API V1)

**Fato:** coleção `docs/API - eNotas - V1 - NFS-e.postman_collection.json` cobre o ciclo NFS-e; o client C# **não** implementa paths `/v1/.../nfes` nem models de serviço.

#### Escopo previsto

| Item | Detalhe |
|------|---------|
| Models | Payload com `servico.*` (ISS, código municipal, LC116, etc.); sem model `Servico` hoje |
| Operações NFS-e | Emitir, listar, consultar (id / idExterno), cancelar (id / idExterno), download XML, download **PDF** |
| PDF / XML (download) | **Fato ([KB 173803](https://atendimento.notagateway.com.br/kb/pt-br/article/173803/baixar-o-pdf-ou-xml-de-uma-nota-fiscal)):** base `https://api.enotasgw.com.br/v1`; paths `/v1/empresas/{empresaId}/nfes/{nfeId}/pdf\|xml` e `.../porIdExterno/{idExterno}/pdf\|xml` — alinhado ao Postman V1 (`nfes`). Escopo **NFS-e V1**, não path V2 `nf-e`/`nfc-e` |
| Apoio municipal | Serviços municipais, dados obrigatórios, características da prefeitura (Postman V1 pasta empresas) |
| Reforma / sandbox | Sample `Sandbox - Reforma` (IBS/CBS em serviço) — contrato a validar na KB |

#### Critério de aceite

- [ ] Região ou métodos públicos NFS-e em `eNotasClient` (ou superfície explícita documentada)
- [ ] Models de request/response tipados
- [ ] Documentação clara: V1 NFS-e ≠ V2 NF-e/NFC-e
- [ ] Exemplos sem API Key real

**Validação humana:** prioridade de negócio e estabilidade do contrato V1 antes de publicar no NuGet.

### P2 — Endpoints V2/V3 listados no README e ausentes no client

| Item | Evidência | Status no client |
|------|-----------|------------------|
| Manifestação de Destinatário NF-e | **Consulta (Postman):** `GET https://api2.enotasgw.com.br/v3/empresas/{empresaid}/nf-e/manifestacao/{chaveacesso}`. **Envio ([KB 409178](https://atendimento.notagateway.com.br/kb/pt-br/article/409178/duvidas-frequentes-sobre-a-manifestacao-do-destinatario-de-notas)):** body `{"tipo":"CienciaDaOperacao","justificativa":null}` — path/método HTTP **não** documentados no artigo; justificativa 15–255 só em “Operação não Realizada”; troca de status permitida conforme evento anterior; limite SEFAZ 20 req/h (`consChNFe`/`distNSU`). Docs V2 emissão: 0 endpoints | Ausente — produto Consulta/Manifestação ≠ API V2 emissão; **validação humana** do path de **POST/envio** (FAQ só traz body) |
| Incluir/Alterar empresa | `POST /v2/empresas` | Ausente |
| Consultar empresa / listar empresas | `GET /v2/empresas...` | Ausente |
| Vincular certificado digital | `POST /v2/empresas/{id}/certificadoDigital` | Ausente |
| Vincular logotipo | `POST /v2/empresas/{id}/logo` | Ausente |
| SAT (setup / download EXE) | Postman V2 pasta empresas + README | Ausente |
| Habilitar / desabilitar empresa | Paths v1 no Postman | Ausente |

#### Critério de aceite

- [ ] Métodos async retornando `ApiResponse` / `ApiResponse<T>`
- [ ] Models de empresa/certificado/logo quando o endpoint exigir body multipart ou JSON
- [ ] README atualizado (passar de “futuro” para “disponível”)

### P3 — Qualidade e DX da library

| Lacuna | Classificação | Evidência |
|--------|---------------|-----------|
| Suite mínima (gaps de cobertura) | Fato (parcialmente resolvido) | xUnit em `eNotas.Sharp.Tests`; lacunas em `TESTING.md` (ex.: integração API real) |
| Pasta `Exemplos/` vazia | Fato | Diretório existe sem samples |
| Sem `CancellationToken` | **Feito** | Parâmetro opcional em todos os métodos públicos + `RestService`; cancelamento relança `OperationCanceledException` |
| `RestService.Put` sem consumidor | Fato | Método interno não usado pelo client |
| `Delete` com URL absoluta vs Post/Get relativos | **Feito** | Path relativo via `SendAsync` + `HttpMethod.Delete`, igual a Post/Get/Put |
| Catch vazio na deserialização do `Get` | **Feito** | Falha de parse JSON/XML preenche `ApiResponse.Exception` |
| `Version` ≠ `AssemblyVersion` no csproj | Fato | Empacotamento NuGet |
| Base URL hardcoded | Fato | `eNotasClient` |
| Timeout / retry / HttpClient factory | Não identificado / inferência | Sem API de configuração |
| `NotaWebhook` só como DTO | Fato | Sem listener nem config de webhook no client |
| README vs client | Fato (parcialmente resolvido) | Lista de métodos alinhada; P0 models refletidos; manter sync em cada release |

#### Critério de aceite (incremental)

- [x] Suite mínima (serialização de models + smoke de paths) — xUnit em `eNotas.Sharp.Tests`
- [ ] Exemplos compiláveis sem secrets
- [x] Correções de transporte documentadas e sem breaking silencioso — parcial: catch do `Get` preenche `Exception` em falha de parse; `CancellationToken` aditivo feito; `Delete` alinhado a path relativo; demais gaps abertos (`Put` sem uso, base URL hardcoded)
- [x] README alinhado aos métodos reais do client — lista de métodos e nota de P0; revisar a cada bump
- [x] `CancellationToken` opcional (`= default`) em `eNotasClient` / `RestService` — source-compatible; cancelamento não é engolido em `ApiResponse`

## Matriz Postman × Client (resumo)

| Origem | Cobertura no client |
|--------|---------------------|
| Postman V2 — pasta NF-e (emitir/consultar/cancelar/inutilizar/CC-e/XML cancelamento) | Implementado |
| Postman V2 — pasta NFC-e (ciclo equivalente sem CC-e) | Implementado |
| Postman V2 — XML da nota (`.../xml`) | Implementado no client (`ConsultaNfeXML` / `ConsultaNfceXML`); nem sempre listado na collection |
| PDF NF-e/NFC-e (V2) | **Sem** endpoint `/pdf` na referência oficial V2; PDF via `linkDanfe` (consulta) e `nfeLinkDanfe` (webhook) — já tipados em `Consulta` / `NotaWebhook`. Docs: [Consultar Nota Fiscal](https://docs.notagateway.com.br/v2/reference/consultar-nota-fiscal-1), [Webhook](https://docs.notagateway.com.br/v2/docs/webhook), [Status](https://docs.notagateway.com.br/v2/docs/status-da-nota-fiscal) (`Autorizada` = PDF pronto) |
| Postman V2 — pasta empresas (CRUD, certificado, logo, SAT) | Não implementado |
| Postman V2/V3 — manifestação destinatário | Não implementado; **fora** da API V2 de emissão (busca “manifesta” em docs.notagateway.com.br/v2 = 0 resultados). Postman GET `api2`/`v3` + FAQ [KB 409178](https://atendimento.notagateway.com.br/kb/pt-br/article/409178/duvidas-frequentes-sobre-a-manifestacao-do-destinatario-de-notas) (body sem path) |
| Postman V1 — NFS-e + apoio municipal + PDF | Não implementado; PDF/XML V1 evidenciados na [KB 173803](https://atendimento.notagateway.com.br/kb/pt-br/article/173803/baixar-o-pdf-ou-xml-de-uma-nota-fiscal) (`/v1/.../nfes/.../pdf`) |

## Fontes de verdade

1. Código: `eNotas.Sharp/Clients/eNotasClient.cs`, `eNotas.Sharp/Models/`
2. Postman V2 (client atual): `docs/API - eNotas - V2 - NF-e - NFC-e.postman_collection.json`
3. Postman V1 (referência NFS-e / futuro): `docs/API - eNotas - V1 - NFS-e.postman_collection.json`
4. KB oficial: [Central de ajuda NotaGateway](https://atendimento.notagateway.com.br/kb/pt-br) (skill `notagateway-kb-lookup` / agent `notagateway-docs-specialist`)
5. Docs oficiais ReadMe: [API V2](https://docs.notagateway.com.br/v2/docs/sobre-a-api) (NF-e/NFC-e/DC-e) e [API V1](https://docs.notagateway.com.br/docs) (NFS-e)
6. Docs internos: `ARCHITECTURE.md`, `DOMAIN.md`, `MODULES.md`, `TESTING.md`

## Itens que exigem validação humana

- Manifestação de Destinatário — [KB 409178](https://atendimento.notagateway.com.br/kb/pt-br/article/409178/duvidas-frequentes-sobre-a-manifestacao-do-destinatario-de-notas) fecha regras de negócio + shape do body de **envio** (`tipo`, `justificativa`), mas **não** publica URL/verbo. Postman só tem **GET** consulta em `api2`/`v3`. Falta path oficial do envio e lista completa de `tipo` (só `CienciaDaOperacao` aparece no exemplo). Fora da API V2 de emissão.
- Prioridade real entre P1 (NFS-e) e P2 (empresas / manifestação / SAT).
- Processo de publicação NuGet e política de bump de versão para mudanças aditivas grandes.

### Validações fechadas (evidência)

- **PDF NF-e/NFC-e (V2):** não existe `GET /v2/.../pdf` na referência oficial. Download do DANFE/PDF = URL em `linkDanfe` (consulta) / `nfeLinkDanfe` (webhook → `.../file/(...)/pdf`). Status `Autorizada` = PDF pronto ([Status](https://docs.notagateway.com.br/v2/docs/status-da-nota-fiscal), [Webhook](https://docs.notagateway.com.br/v2/docs/webhook), [Consultar NF-e](https://docs.notagateway.com.br/v2/reference/consultar-nota-fiscal-1)). Já coberto por `Consulta.LinkDanfe` e `NotaWebhook.NfeLinkDanfe` — **não** adicionar método `Consulta*Pdf` no client V2.
- **PDF/XML NFS-e (V1):** [KB 173803](https://atendimento.notagateway.com.br/kb/pt-br/article/173803/baixar-o-pdf-ou-xml-de-uma-nota-fiscal) — `/v1/empresas/{empresaId}/nfes/{nfeId|porIdExterno}/{id}/pdf|xml` (escopo P1, não client atual).

## Fora de escopo deste roadmap

- Implementar a feature em si (este arquivo só rastreia o backlog).
- Tratar NFS-e como disponível na lib.
- Inventar endpoints ou campos sem evidência em Postman, KB ou código.
- Assumir CI/CD, Docker ou stacks que não existem no repositório.
