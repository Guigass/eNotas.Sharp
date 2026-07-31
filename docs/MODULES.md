# Módulos

## Client — eNotasClient

### Responsabilidade
Fachada pública da biblioteca: métodos async para NF-e e NFC-e.

### Caminhos principais
- `eNotas.Sharp/Clients/eNotasClient.cs`

### Dependências
- `RestService`
- Models: `Nota`, `Consulta`, `Inutilizacao`, `CartaCorrecao`, wrappers XML

### Fluxos relacionados
Emissão, consulta, cancelamento, inutilização, carta de correção, download XML.

### Pontos de atenção
- Base URL hardcoded: `https://api.enotasgw.com.br`
- Implementa `IDisposable`
- Lista de métodos públicos deve permanecer alinhada ao `README.md`

### Como alterar com segurança
1. Manter assinaturas públicas estáveis.
2. Novos métodos: adicionar na região correta (NFe/NFCe).
3. Reutilizar `Post`/`Get`/`Delete` existentes.
4. Atualizar README (lista de métodos) e, se aplicável, versão do pacote.

### Informações incertas
Escopo e prioridade de “Manifestação de Destinatário” e métodos futuros do README.

---

## Service — RestService

### Responsabilidade
Transporte HTTP, headers, serialização JSON, deserialização JSON/XML, GET binário (`GetBytes`) e POST multipart (`PostMultipart`).

### Caminhos principais
- `eNotas.Sharp/Services/RestService.cs`

### Dependências
- Newtonsoft.Json, HttpClient, XmlSerializer, `ApiResponse` / `ApiResponse<T>`

### Fluxos relacionados
Todas as chamadas de rede da biblioteca.

### Pontos de atenção
- Classe `internal` — não expor publicamente.
- Exceções engolidas e expostas em `Exception` / `Message` (exceto `OperationCanceledException`, que é relançada).
- `CancellationToken` opcional propagado até `SendAsync` (Post/PostMultipart/Get/GetBytes/Put/Delete).
- `Delete` usa path relativo ao `BaseAddress`, igual aos demais verbos.
- `GetBytes` retorna `ApiResponse<byte[]>`: sucesso → `Object` com bytes; falha HTTP → `Message` com body UTF-8 (pré-req PDF NFS-e / P1-09).
- `Post(string, CancellationToken)` envia POST sem body (usado por `DesabilitarEmpresa` / P2-07 e `HabilitarEmpresa` / P2-08; Postman formdata vazio).
- `PostMultipart` aceita `MultipartFormDataContent` montado pelo caller; Content-Type com boundary vem do conteúdo (não força `application/json`); retorno `ApiResponse` como `Post` (usado por `VincularCertificadoDigital` / P2-05 e `VincularLogotipo` / P2-06).
- Header `Accept: application/json` e `Authorization: Basic {apiKey}`.

### Como alterar com segurança
1. Avaliar impacto em **todos** os métodos do client.
2. Não quebrar formato de serialização sem versionamento.
3. Validar Get JSON, Get XML, GetBytes e PostMultipart após mudanças.
4. Revisar com skill `integration-change-review`.

---

## Models — DTOs de emissão e consulta

### Responsabilidade
Representar payloads de request/response da API v2.

### Caminhos principais
- `eNotas.Sharp/Models/Nota.cs` (agregado raiz de emissão)
- `Iten.cs`, `Impostos.cs`, `Icms.cs`, `Cofins.cs` (`class Imposto` para PIS/COFINS/IPI), `IbsCbs.cs` (IBS/CBS NF-e/NFC-e), `Cliente.cs`, `Pedido.cs`, `Pagamento.cs`, `Transporte.cs`, …
- `Consulta.cs`, `ConsultaInutilizacao.cs`, `Inutilizacao.cs`, `CartaCorrecao.cs`, `CorrecaoResponse.cs`
- `ApiResponse.cs`, `NotaWebhook.cs`

### Dependências
Newtonsoft.Json attributes; `CustomDateTimeConverter` em campos de data.

### Fluxos relacionados
Serialização na emissão; deserialização na consulta.

### Pontos de atenção
- Nomenclatura `Iten` / `Adicoe` (pluralização atípica) — **preservar** para não quebrar consumidores.
- Arquivo `Cofins.cs` define `class Imposto` (PIS/COFINS/IPI).
- `Impostos.IbsCbs` usa tipo dedicado `IbsCbs` ([KB 595993](https://atendimento.notagateway.com.br/kb/pt-br/article/595993/como-enviar-ibs-e-cbs-ao-emitir-uma-nf-via-api)); não reutiliza `Imposto`/`porAliquota`.
- Propriedades em geral nullable + `NullValueHandling.Ignore`.

### Como alterar com segurança
1. Preferir adicionar propriedades opcionais.
2. Conferir nome JSON (`JsonProperty`) com Postman/API oficial.
3. Não renomear classes públicas sem major version.
4. Ao alterar impostos, verificar `Iten.Impostos` e usos em emissão.

### Informações incertas
Cobertura completa do schema oficial da eNotas versus models atuais.

---

## Models — XML fiscal

### Responsabilidade
Deserializar XML retornado pelos endpoints `/xml`, `/xmlCancelamento`, etc.

### Caminhos principais
- `Xml.cs`, `XmlCancelamento.cs`, `XmlCorrecao.cs`, `XmlInutilizacao.cs`

### Dependências
XmlSerializer; estruturas aninhadas (`ProcEventoNFe`, `NfeProc`, `ProcInutNFe`, …)

### Pontos de atenção
Arquivos grandes e sensíveis a mudanças de schema. Alterar só com amostra real de XML.

### Como alterar com segurança
Validar deserialização com XML real de homologação antes de publicar pacote.

---

## Helpers

### Responsabilidade
Converter `DateTimeOffset` no JSON.

### Caminhos principais
- `eNotas.Sharp/Helpers/CustomDateTimeConverter.cs`
- Namespace: `eNotas.Sharp` (não `eNotas.Sharp.Models`)

### Pontos de atenção
Escrita em UTC formato `"o"`; leitura converte de UTC para fuso local.

---

## docs (referência de API)

### Responsabilidade
Coleções Postman como referência de contrato.

### Caminhos principais
- `docs/API - eNotas - V1 - NFS-e.postman_collection.json`
- `docs/API - eNotas - V2 - NF-e - NFC-e.postman_collection.json`

### Pontos de atenção
V1 NFS-e **não** está implementada no client C# atual. Usar V2 como referência primária para o código existente.
