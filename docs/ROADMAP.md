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
| Raiz `Nota` | `tipo` | **Fato:** no sample Postman; ausente em `Nota.cs` (existe em `Consulta`) |
| Raiz `Nota` | `forcarEmissaoContingencia`, `emitidaEmContingencia` | **Fato:** no sample / resposta; ausentes em `Nota.cs` |
| Raiz `Nota` | `enviarPorEmail` vs `enviadaPorEmail` | **Fato:** model usa `enviarPorEmail`; sample Postman usa `enviadaPorEmail` — **validação humana** do nome oficial |
| `itens[]` (`Iten`) | `codigoBeneficioFiscal`, `extipi`, `quantidadeTributavel`, `unidadeMedidaTributavel`, `valorTotal` | **Fato:** no sample Postman; ausentes em `Iten.cs` |
| Impostos | `ibsCbs` tipado como `Imposto` genérico | **Fato:** propriedade existe; **inferência:** subcampos da reforma (além de `situacaoTributaria` / `porAliquota` / `classificacaoTributaria`) podem exigir model próprio — **validação humana** |
| Demais aninhados | cliente, pedido, pagamento, transporte, referências, impostos | **Inferência:** Postman é amostra incompleta; auditar V2 + [KB NotaGateway](https://atendimento.notagateway.com.br/kb/pt-br) para opcionais restantes |

#### Critério de aceite

- [ ] Properties aditivas em `Nota`, `Iten` e aninhados alinhadas ao contrato oficial
- [ ] Paridade considerada para NF-e e NFC-e (mesmo model compartilhado)
- [ ] Sem remoção/renomeação de propriedades públicas existentes
- [ ] README / versão NuGet atualizados quando a superfície pública crescer

### P1 — NFS-e (API V1)

**Fato:** coleção `docs/API - eNotas - V1 - NFS-e.postman_collection.json` cobre o ciclo NFS-e; o client C# **não** implementa paths `/v1/.../nfes` nem models de serviço.

#### Escopo previsto

| Item | Detalhe |
|------|---------|
| Models | Payload com `servico.*` (ISS, código municipal, LC116, etc.); sem model `Servico` hoje |
| Operações NFS-e | Emitir, listar, consultar (id / idExterno), cancelar (id / idExterno), download XML, download **PDF** |
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
| Manifestação de Destinatário NF-e | Postman: `GET https://api2.enotasgw.com.br/v3/empresas/{empresaid}/nf-e/manifestacao/{chaveacesso}` | Ausente — **validação humana** de host (`api2`) e versão (`v3`) |
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
| Sem testes automatizados | Fato | `TESTING.md`; sem projeto de testes no repo |
| Pasta `Exemplos/` vazia | Fato | Diretório existe sem samples |
| Sem `CancellationToken` | Fato | `eNotasClient` / `RestService` |
| `RestService.Put` sem consumidor | Fato | Método interno não usado pelo client |
| `Delete` com URL absoluta vs Post/Get relativos | Fato | `ARCHITECTURE.md` / `RestService` |
| Catch vazio na deserialização do `Get` | Fato | Pode mascarar falha de parse |
| `Version` ≠ `AssemblyVersion` no csproj | Fato | Empacotamento NuGet |
| Base URL hardcoded | Fato | `eNotasClient` |
| Timeout / retry / HttpClient factory | Não identificado / inferência | Sem API de configuração |
| `NotaWebhook` só como DTO | Fato | Sem listener nem config de webhook no client |
| Inconsistências do README | Fato | Ex.: XML cancelamento NF-e citando método incorreto |

#### Critério de aceite (incremental)

- [ ] Suite mínima (serialização de models + smoke de paths) quando houver decisão de framework
- [ ] Exemplos compiláveis sem secrets
- [ ] Correções de transporte documentadas e sem breaking silencioso
- [ ] README alinhado aos métodos reais do client

## Matriz Postman × Client (resumo)

| Origem | Cobertura no client |
|--------|---------------------|
| Postman V2 — pasta NF-e (emitir/consultar/cancelar/inutilizar/CC-e/XML cancelamento) | Implementado |
| Postman V2 — pasta NFC-e (ciclo equivalente sem CC-e) | Implementado |
| Postman V2 — XML da nota (`.../xml`) | Implementado no client (`ConsultaNfeXML` / `ConsultaNfceXML`); nem sempre listado na collection |
| Postman V2 — pasta empresas (CRUD, certificado, logo, SAT) | Não implementado |
| Postman V2/V3 — manifestação destinatário | Não implementado |
| Postman V1 — NFS-e + apoio municipal + PDF | Não implementado |

## Fontes de verdade

1. Código: `eNotas.Sharp/Clients/eNotasClient.cs`, `eNotas.Sharp/Models/`
2. Postman V2 (client atual): `docs/API - eNotas - V2 - NF-e - NFC-e.postman_collection.json`
3. Postman V1 (referência NFS-e / futuro): `docs/API - eNotas - V1 - NFS-e.postman_collection.json`
4. KB oficial: [Central de ajuda NotaGateway](https://atendimento.notagateway.com.br/kb/pt-br) (skill `notagateway-kb-lookup` / agent `notagateway-docs-specialist`)
5. Docs internos: `ARCHITECTURE.md`, `DOMAIN.md`, `MODULES.md`, `TESTING.md`

## Itens que exigem validação humana

- Contrato completo IBS/CBS para emissão NF-e/NFC-e (além do `Imposto` genérico).
- Nome oficial do campo de e-mail na emissão (`enviarPorEmail` vs `enviadaPorEmail`).
- Host/versão da Manifestação de Destinatário (`api2` / `v3`).
- Prioridade real entre P1 (NFS-e) e P2 (empresas / manifestação / SAT).
- Se PDF de NF-e/NFC-e existe na API (hoje PDF aparece no Postman V1 NFS-e).
- Processo de publicação NuGet e política de bump de versão para mudanças aditivas grandes.

## Fora de escopo deste roadmap

- Implementar a feature em si (este arquivo só rastreia o backlog).
- Tratar NFS-e como disponível na lib.
- Inventar endpoints ou campos sem evidência em Postman, KB ou código.
- Assumir CI/CD, Docker ou stacks que não existem no repositório.
