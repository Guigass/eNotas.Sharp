# Roadmap eNotas.Sharp

Backlog de lacunas da library cliente em relação à API eNotas Gateway e à qualidade do SDK.

Este documento **não** descreve features já disponíveis como se estivessem prontas. O envio de manifestação (P2-12) e parte dos métodos NFS-e (P1-09+) ainda não estão no `eNotasClient`. Já disponíveis: CRUD empresas/certificado/logo/desabilitar/habilitar/setup SAT/consultar SAT/consultar manifestação, emitir NFS-e (**P1-02**), consultar NFS-e por id GW (**P1-03**) e por idExterno (**P1-04**), listar NFS-e (**P1-05**), cancelar NFS-e por id GW (**P1-06**) e por idExterno (**P1-07**), download XML NFS-e por id GW e idExterno (**P1-08**).

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
| NF-e | Emitir, consultar, XML, cancelar, XML cancelamento, inutilização (+ consulta/XML), carta de correção (+ consulta/XML); Consultar manifestação (`ConsultaManifestacao` → `GET` absoluto `https://api2.enotasgw.com.br/v3/empresas/{empresaId}/nf-e/manifestacao/{chaveAcesso}`, body em `ApiResponse.Message`) |
| NFC-e | Emitir, consultar, XML, cancelar, XML cancelamento, inutilização (+ consulta/XML) |
| NFS-e | Emitir (`EmitirNfse` → `POST /v1/empresas/{empresaId}/nfes`); Consultar por id GW (`ConsultaNfse` → `GET /v1/empresas/{empresaId}/nfes/{nfeId}`); Consultar por idExterno (`ConsultaNfsePorIdExterno` → `GET /v1/empresas/{empresaId}/nfes/porIdExterno/{idExterno}`); Listar (`ListarNfse` → `GET /v1/empresas/{empresaId}/nfes?...`); Cancelar por id GW (`CancelaNfse` → `DELETE /v1/empresas/{empresaId}/nfes/{nfeId}`); Cancelar por idExterno (`CancelaNfsePorIdExterno` → `DELETE /v1/empresas/{empresaId}/nfes/porIdExterno/{idExterno}`); XML por id GW (`ConsultaNfseXML` → `GET /v1/empresas/{empresaId}/nfes/{nfeId}/xml`, body em `Message`); XML por idExterno (`ConsultaNfseXMLPorIdExterno` → `GET .../porIdExterno/{idExterno}/xml`, body em `Message`); demais métodos P1-09+ ainda abertos |
| Empresas | Incluir/Alterar (`IncluirAlterarEmpresa` → `POST /v2/empresas`); Consultar por id (`ConsultaEmpresa` → `GET /v2/empresas/{empresaId}`); Listar (`ListarEmpresas` → `GET /v2/empresas?...`); Vincular certificado (`VincularCertificadoDigital` → `POST /v2/empresas/{empresaId}/certificadoDigital` multipart); Vincular logo (`VincularLogotipo` → `POST /v2/empresas/{empresaId}/logo` multipart `logotipo`); Desabilitar (`DesabilitarEmpresa` → `POST /v1/empresas/{empresaId}/desabilitar`); Habilitar (`HabilitarEmpresa` → `POST /v1/empresas/{empresaId}/habilitar`); Setup SAT (`SetupSat` → `GET /v2/empresas/{empresaId}/sat/setup`, body em `ApiResponse.Message`); Consultar SAT (`ConsultaSat` → `GET /v2/sat/{satId}/all`, parâmetro `satId`, body em `ApiResponse.Message`) |

Models públicos em `eNotas.Sharp/Models/` cobrem emissão (`Nota`, `Iten`, impostos, pagamento, transporte, etc.), emissão NFS-e DTO (`Nfse`, `Servico` — **P1-01**), consulta NFS-e (`ConsultaNfse` — **P1-03**), lista NFS-e (`ListaNfse` — **P1-05**), consultas NF-e/NFC-e, XML fiscal e empresa (`Empresa`, `ConfiguracoesNfse`, `ListaEmpresas`).

**Fato:** `EmitirNfse` (**P1-02**), `ConsultaNfse` (**P1-03**), `ConsultaNfsePorIdExterno` (**P1-04**), `ListarNfse` (**P1-05**), `CancelaNfse` (**P1-06**), `CancelaNfsePorIdExterno` (**P1-07**), `ConsultaNfseXML` e `ConsultaNfseXMLPorIdExterno` (**P1-08**) estão no client; demais métodos NFS-e (P1-09+) e envio de manifestação (P2-12) ainda abertos/bloqueados. Models de emissão NFS-e (**P1-01**) já existem. `SetupSat` (**P2-09**), `ConsultaSat` (**P2-10**) e `ConsultaManifestacao` (**P2-11**, host `api2`) já estão no client.

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

### Como agentes devem executar P1 / P2

1. Escolher **um** item `P1-xx` / `P2-xx` / `INFRA-xx` com status **Aberto** e classificação **Agente-pronto**.
2. Respeitar **Depends** (não pular pré-requisito aberto).
3. Evidência obrigatória: Postman citado + KB quando o item mencionar KB.
4. Implementar só o critério de aceite daquela linha; marcar `[x]` e status **Feito** ao concluir.
5. Itens **Bloqueado** exigem aprovação humana explícita na conversa — não implementar.
6. Publicação NuGet / bump major continua fora do escopo do agente (ver “Validação humana”).

Ordem sugerida se o usuário disser “pegue o próximo”: `INFRA-01` → `INFRA-02` → menor ID aberto de **P2** (empresas, mesma base URL V2) → menor ID aberto de **P1** (NFS-e) → itens Bloqueado só se o humano pedir.

### INFRA — Pré-requisitos de transporte (`RestService`)

Hoje `RestService` tem JSON `Post`/`Get`/`Get<T>`/`Put`/`Delete`, **`GetBytes`** (INFRA-01 Feito) e **`PostMultipart`** (INFRA-02 Feito). Certificado/logo (**P2-05**/**P2-06** Feito) usam multipart; `SetupSat` (**P2-09**) e `ConsultaSat` (**P2-10**) usam `Get` não tipado (body em `Message`).

| ID | Item | Depends | Classificação | Evidência | Critério de aceite | Status |
|----|------|---------|---------------|-----------|--------------------|--------|
| INFRA-01 | GET binário (bytes) | — | **Agente-pronto** | PDF NFS-e Postman V1 + [KB 173803](https://atendimento.notagateway.com.br/kb/pt-br/article/173803/baixar-o-pdf-ou-xml-de-uma-nota-fiscal) | Método interno em `RestService` que retorna `ApiResponse` com conteúdo binário (ex.: `byte[]` em `Object` tipado ou tipo dedicado); sem breaking na API pública; testes de smoke | Feito |
| INFRA-02 | POST multipart/form-data | — | **Agente-pronto** | Postman V2 certificado (`senha`+`arquivo`) e logo (`logotipo`) | Método interno `PostMultipart` (ou equivalente) em `RestService`; não expor `HttpClient`; testes com `HttpMessageHandler` fake | Feito |

### P1 — NFS-e (API V1)

**Fato:** coleção `docs/API - eNotas - V1 - NFS-e.postman_collection.json`; base host **igual** ao client atual (`https://api.enotasgw.com.br`); paths `/v1/empresas/{empresaId}/nfes...`. Não confundir com V2 `nf-e`/`nfc-e`.

**Convenções de implementação:** `#region NFSe` em `eNotasClient`; models novos em `Models/` (não reutilizar `Nota` de mercadoria sem necessidade); nomes públicos no estilo existente (`EmitirNfse`, `ConsultaNfse`, …); `CancellationToken` opcional; README + Version ao fechar superfície pública.

| ID | Item | Method + path | Depends | Classificação | Critério de aceite | Status |
|----|------|---------------|---------|---------------|--------------------|--------|
| P1-01 | Models emissão NFS-e | — (DTO) | — | **Agente-pronto** | Models tipados espelhando sample Postman **Emitir NFS-e**: raiz (`tipo`, `idExterno`, `ambienteEmissao`, email, `cliente`, `servico`, `valorTotal`) + `servico.*` (`descricao`, `aliquotaIss`, `issRetidoFonte`, `codigoServicoMunicipio`, `itemListaServicoLC116`, `cnae`, `municipioPrestacaoServico`); nullable + `JsonProperty` + `NullValueHandling.Ignore`; teste de serialização | Feito |
| P1-02 | Emitir NFS-e | `POST /v1/empresas/{empresaId}/nfes` | P1-01 | **Agente-pronto** | Método público async → `ApiResponse`; path exato Postman; smoke de path | Feito |
| P1-03 | Consultar por id GW | `GET /v1/empresas/{empresaId}/nfes/{nfeId}` | P1-01 | **Agente-pronto** | Método tipado (`ApiResponse<T>` com model de consulta NFS-e mínimo alinhado ao retorno conhecido / campos do sample); smoke | Feito |
| P1-04 | Consultar por idExterno | `GET /v1/empresas/{empresaId}/nfes/porIdExterno/{idExterno}` | P1-03 | **Agente-pronto** | Paridade com P1-03 | Feito |
| P1-05 | Listar NFS-e | `GET /v1/empresas/{empresaId}/nfes?pageNumber&pageSize&sortBy&sortDirection&filter` | P1-03 | **Agente-pronto** | Método com parâmetros de paginação/filtro espelhando query Postman; model de lista tipado o suficiente para deserializar | Feito |
| P1-06 | Cancelar por id GW | `DELETE /v1/empresas/{empresaId}/nfes/{nfeId}` | P1-02 | **Agente-pronto** | Método → `ApiResponse`; smoke | Feito |
| P1-07 | Cancelar por idExterno | `DELETE /v1/empresas/{empresaId}/nfes/porIdExterno/{idExterno}` | P1-06 | **Agente-pronto** | Paridade com P1-06 | Feito |
| P1-08 | Download XML (id + idExterno) | `GET .../nfes/{nfeId}/xml` e `.../porIdExterno/{idExterno}/xml` | P1-03 | **Agente-pronto** | Dois métodos (ou overload claro); retorno string/`ApiResponse` sem inventar schema XML se não houver model; [KB 173803](https://atendimento.notagateway.com.br/kb/pt-br/article/173803/baixar-o-pdf-ou-xml-de-uma-nota-fiscal) | Feito |
| P1-09 | Download PDF (id + idExterno) | `GET .../nfes/{nfeId}/pdf` e `.../porIdExterno/{idExterno}/pdf` | INFRA-01, P1-03 | **Agente-pronto** | Dois métodos retornando bytes via INFRA-01; [KB 173803](https://atendimento.notagateway.com.br/kb/pt-br/article/173803/baixar-o-pdf-ou-xml-de-uma-nota-fiscal) | Aberto |
| P1-10 | Apoio municipal | `GET /v1/estados/{uf}/cidades/{nome}/servicos`, `GET /v1/servicos/cidades`, `GET /v1/estados/cidades/{codigoIBGECidade}/provedor`, `GET /v1/empresas/{empresaId}/criticardadosobrigatorios` | — | **Agente-pronto** | Métodos read-only tipados o suficiente; pode ser 1 commit por endpoint se preferir atomicidade | Aberto |
| P1-11 | Campos Reforma em `servico` | — (DTO) | P1-01 | **Agente-pronto** (aditivo) | Adicionar ao model de serviço os campos do sample **Sandbox - Reforma**: `codigoNBS`, `codigoTributacaoNacional`, `ibsCbs` (shape do sample); nullable; sem apontar sandbox como base URL padrão | Aberto |
| P1-12 | Docs NFS-e | — | P1-02..P1-09 (mínimo emitir+consulta+cancel+xml) | **Agente-pronto** | README: seção NFS-e ≠ NF-e/NFC-e; lista de métodos reais; sem API Key; `docs/` alinhados (`MODULES`/`ARCHITECTURE` se tocados) | Aberto |

**Critério de aceite do épico P1** (fechado quando todos Agente-pronto acima estiverem Feito):

- [x] `#region NFSe` (ou superfície equivalente) em `eNotasClient` — parcial: região + `EmitirNfse` (**P1-02**) + `ConsultaNfse` (**P1-03**) + `ConsultaNfsePorIdExterno` (**P1-04**) + `ListarNfse` (**P1-05**) + `CancelaNfse` (**P1-06**) + `CancelaNfsePorIdExterno` (**P1-07**) + `ConsultaNfseXML`/`ConsultaNfseXMLPorIdExterno` (**P1-08**); demais métodos P1-09+
- [x] Models request/response tipados — parcial: emissão (`Nfse`/`Servico`, **P1-01**) + consulta (`ConsultaNfse`, **P1-03**/**P1-04**) + lista (`ListaNfse`, **P1-05**); XML (**P1-08**) retorna string em `ApiResponse.Message` (sem schema tipado)
- [ ] Documentação clara V1 ≠ V2
- [x] Exemplos/testes sem API Key real — parcial: `NfseSerializationTests` + fixture Postman + smoke `EmitirNfse`/`ConsultaNfse`/`ConsultaNfsePorIdExterno`/`ListarNfse`/`CancelaNfse`/`CancelaNfsePorIdExterno`/`ConsultaNfseXML`/`ConsultaNfseXMLPorIdExterno` (sem API Key); exemplos de uso NFS-e ainda em P1-12

### P2 — Empresas V2 + SAT + habilitação (+ manifestação)

**Fato:** pasta `empresas` do Postman V2; host padrão `api.enotasgw.com.br`. Habilitar/desabilitar usam path **v1** no Postman (mesmo host).

| ID | Item | Method + path | Depends | Classificação | Critério de aceite | Status |
|----|------|---------------|---------|---------------|--------------------|--------|
| P2-01 | Model empresa | — (DTO) | — | **Agente-pronto** | Model(s) alinhados ao body Postman **Incluir/Alterar empresa** (endereço, CNPJ, IE/IM, razões, flags, `ConfiguracoesNFSeHomologacao`/`Producao`, etc.); nullable + Ignore; teste serialização | Feito |
| P2-02 | Incluir/Alterar empresa | `POST /v2/empresas` | P2-01 | **Agente-pronto** | Método público → `ApiResponse`; smoke | Feito |
| P2-03 | Consultar empresa por id | `GET /v2/empresas/{empresaId}` | P2-01 | **Agente-pronto** | `ApiResponse<T>` com model de empresa | Feito |
| P2-04 | Listar empresas | `GET /v2/empresas?pageNumber&pageSize&searchBy&searchTerm&sortBy&sortDirection` | P2-03 | **Agente-pronto** | Query params como Postman; model de lista | Feito |
| P2-05 | Vincular certificado | `POST /v2/empresas/{empresaId}/certificadoDigital` multipart (`senha`, `arquivo`) | INFRA-02 | **Agente-pronto** | Método aceita stream/bytes + senha; usa INFRA-02; sem logar certificado/senha | Feito |
| P2-06 | Vincular logo | `POST /v2/empresas/{empresaId}/logo` multipart (`logotipo`) | INFRA-02 | **Agente-pronto** | Método aceita stream/bytes imagem; formatos JPG/PNG/GIF (doc Postman) | Feito |
| P2-07 | Desabilitar empresa | `POST /v1/empresas/{empresaId}/desabilitar` | — | **Agente-pronto** | Método → `ApiResponse`; path v1 conforme Postman | Feito |
| P2-08 | Habilitar empresa | `POST /v1/empresas/{empresaId}/habilitar` | — | **Agente-pronto** | Paridade com P2-07 | Feito |
| P2-09 | Setup SAT | `GET /v2/empresas/{empresaId}/sat/setup` | — | **Agente-pronto** | Método; retorno tipado ou `ApiResponse` com message se schema incerto — não inventar campos | Feito |
| P2-10 | Consultar SAT | `GET /v2/sat/{satId}/all` (Postman) | P2-09 | **Agente-pronto** | Path conforme Postman; documentar parâmetro `satId` | Feito |
| P2-11 | Consultar manifestação | `GET https://api2.enotasgw.com.br/v3/empresas/{empresaId}/nf-e/manifestacao/{chaveAcesso}` | — | **Agente-pronto** (cuidado host) | Requer chamada com **host `api2`** (não o BaseAddress atual). Preferir overload/path absoluto mínimo documentado; não alterar base URL padrão do client sem nota no README | Feito |
| P2-12 | Enviar manifestação | path/verbo **não** fechados | — | **Bloqueado** | Só após validação humana do POST oficial (FAQ [KB 409178](https://atendimento.notagateway.com.br/kb/pt-br/article/409178/duvidas-frequentes-sobre-a-manifestacao-do-destinatario-de-notas) tem body, sem URL) | Bloqueado |
| P2-13 | Docs empresas/SAT | — | P2-02..P2-08 (mínimo CRUD + cert/logo ou habilitar) | **Agente-pronto** | README: tirar “futuro” do que estiver implementado; sem secrets | Feito |

**Critério de aceite do épico P2** (itens Agente-pronto Feito; P2-12 permanece Bloqueado até humano):

- [x] Métodos async `ApiResponse` / `ApiResponse<T>`
- [x] Models empresa + suporte multipart onde exigido
- [x] README atualizado para métodos disponíveis

### P3 — Qualidade e DX da library

| Lacuna | Classificação | Evidência |
|--------|---------------|-----------|
| Suite mínima (gaps de cobertura) | Fato (parcialmente resolvido) | xUnit em `eNotas.Sharp.Tests`; lacunas em `TESTING.md` (ex.: integração API real) |
| Pasta `Exemplos/` | **Feito** | Console `Exemplos/EmissaoNfeHomologacao` (net8.0); credenciais via `ENOTAS_API_KEY` / `ENOTAS_EMPRESA_ID` |
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
- [x] Exemplos compiláveis sem secrets — `Exemplos/EmissaoNfeHomologacao` (env vars; sem API Key no código)
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
| Postman V2 — pasta empresas (CRUD, certificado, logo, SAT) | Models `Empresa`/`ConfiguracoesNfse`/`ListaEmpresas` (**P2-01** Feito); `IncluirAlterarEmpresa` (**P2-02** Feito); `ConsultaEmpresa` (**P2-03** Feito); `ListarEmpresas` (**P2-04** Feito); `VincularCertificadoDigital` (**P2-05** Feito); `VincularLogotipo` (**P2-06** Feito); `DesabilitarEmpresa` (**P2-07** Feito); `HabilitarEmpresa` (**P2-08** Feito); `SetupSat` (**P2-09** Feito — body em `Message`, schema Postman vazio); `ConsultaSat` (**P2-10** Feito — `GET /v2/sat/{satId}/all`, body em `Message`, schema Postman vazio); **P2-13** Feito (README/docs sem “futuro” no que já está no client); pré-req **INFRA-02** Feito |
| Postman V2/V3 — manifestação destinatário | Consulta: **P2-11** Feito (`ConsultaManifestacao`, host `api2`/`v3`, body em `Message`); Envio: **P2-12 Bloqueado**. FAQ [KB 409178](https://atendimento.notagateway.com.br/kb/pt-br/article/409178/duvidas-frequentes-sobre-a-manifestacao-do-destinatario-de-notas) (body sem path de POST) |
| Postman V1 — NFS-e + apoio municipal + PDF | Models emissão (**P1-01** Feito: `Nfse`/`Servico`); `EmitirNfse` (**P1-02** Feito); `ConsultaNfse` (**P1-03** Feito: model `ConsultaNfse`); `ConsultaNfsePorIdExterno` (**P1-04** Feito); `ListarNfse` (**P1-05** Feito: model `ListaNfse`); `CancelaNfse` (**P1-06** Feito); `CancelaNfsePorIdExterno` (**P1-07** Feito); `ConsultaNfseXML`/`ConsultaNfseXMLPorIdExterno` (**P1-08** Feito — XML em `Message`); demais itens **P1-09..P1-12** abertos; **INFRA-01** Feito (pré-req PDF); evidência [KB 173803](https://atendimento.notagateway.com.br/kb/pt-br/article/173803/baixar-o-pdf-ou-xml-de-uma-nota-fiscal) |

## Fontes de verdade

1. Código: `eNotas.Sharp/Clients/eNotasClient.cs`, `eNotas.Sharp/Models/`
2. Postman V2 (client atual): `docs/API - eNotas - V2 - NF-e - NFC-e.postman_collection.json`
3. Postman V1 (NFS-e; `EmitirNfse`/`ConsultaNfse`/`ConsultaNfsePorIdExterno`/`ListarNfse`/`CancelaNfse`/`CancelaNfsePorIdExterno`/`ConsultaNfseXML`/`ConsultaNfseXMLPorIdExterno` no client; demais P1-09+): `docs/API - eNotas - V1 - NFS-e.postman_collection.json`
4. KB oficial: [Central de ajuda NotaGateway](https://atendimento.notagateway.com.br/kb/pt-br) (skill `notagateway-kb-lookup` / agent `notagateway-docs-specialist`)
5. Docs oficiais ReadMe: [API V2](https://docs.notagateway.com.br/v2/docs/sobre-a-api) (NF-e/NFC-e/DC-e) e [API V1](https://docs.notagateway.com.br/docs) (NFS-e)
6. Docs internos: `ARCHITECTURE.md`, `DOMAIN.md`, `MODULES.md`, `TESTING.md`

## Itens que exigem validação humana

- **P2-12** Envio de Manifestação de Destinatário — [KB 409178](https://atendimento.notagateway.com.br/kb/pt-br/article/409178/duvidas-frequentes-sobre-a-manifestacao-do-destinatario-de-notas) fecha body (`tipo`, `justificativa`), mas **não** publica URL/verbo. Postman só tem **GET** (P2-11, Agente-pronto). Não implementar POST sem path oficial.
- Publicação NuGet / política de bump para releases grandes (P1+P2 juntos) — implementação aditiva no código **não** espera essa validação; só o publish.
- Ordem de negócio P1 vs P2: agentes seguem a ordem sugerida acima salvo pedido explícito do humano.

### Validações fechadas (evidência)

- **PDF NF-e/NFC-e (V2):** não existe `GET /v2/.../pdf` na referência oficial. Download do DANFE/PDF = URL em `linkDanfe` (consulta) / `nfeLinkDanfe` (webhook → `.../file/(...)/pdf`). Status `Autorizada` = PDF pronto ([Status](https://docs.notagateway.com.br/v2/docs/status-da-nota-fiscal), [Webhook](https://docs.notagateway.com.br/v2/docs/webhook), [Consultar NF-e](https://docs.notagateway.com.br/v2/reference/consultar-nota-fiscal-1)). Já coberto por `Consulta.LinkDanfe` e `NotaWebhook.NfeLinkDanfe` — **não** adicionar método `Consulta*Pdf` no client V2.
- **PDF/XML NFS-e (V1):** [KB 173803](https://atendimento.notagateway.com.br/kb/pt-br/article/173803/baixar-o-pdf-ou-xml-de-uma-nota-fiscal) — XML no client (**P1-08**: `ConsultaNfseXML` / `ConsultaNfseXMLPorIdExterno`, body em `Message`); PDF ainda aberto (**P1-09**, paths `/pdf`).

## Fora de escopo deste roadmap

- Implementar a feature em si (este arquivo só rastreia o backlog).
- Tratar o ciclo NFS-e completo como disponível na lib (emitir + consultar + listar + cancelar + XML estão no client; PDF/apoio municipal ainda abertos).
- Inventar endpoints ou campos sem evidência em Postman, KB ou código.
- Assumir CI/CD, Docker ou stacks que não existem no repositório.
