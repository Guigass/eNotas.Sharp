# Módulos

## Client — eNotasClient

### Responsabilidade
Fachada pública da biblioteca: métodos async para NF-e, NFC-e, NFS-e (emitir), empresas (CRUD, certificado, logo, habilitar/desabilitar), SAT e consulta de manifestação.

### Caminhos principais
- `eNotas.Sharp/Clients/eNotasClient.cs`

### Dependências
- `RestService`
- Models: `Nota`, `Nfse`, `Consulta`, `Inutilizacao`, `CartaCorrecao`, `Empresa`, `ListaEmpresas`, wrappers XML

### Fluxos relacionados
Emissão NF-e/NFC-e/NFS-e, consulta, cancelamento, inutilização, carta de correção, download XML; incluir/alterar/consultar/listar empresa; certificado/logo multipart; setup/consulta SAT; consulta de manifestação (host `api2`).

### Pontos de atenção
- Base URL hardcoded: `https://api.enotasgw.com.br` (`ConsultaManifestacao` usa URL absoluta em `api2`)
- Implementa `IDisposable`
- Lista de métodos públicos deve permanecer alinhada ao `README.md`

### Como alterar com segurança
1. Manter assinaturas públicas estáveis.
2. Novos métodos: adicionar na região correta (NFe/NFCe/NFSe/Empresas).
3. Reutilizar `Post`/`Get`/`Delete`/`PostMultipart` existentes.
4. Atualizar README (lista de métodos) e, se aplicável, versão do pacote.

### Informações incertas
Envio de Manifestação de Destinatário (**P2-12**, bloqueado — path/verbo não oficiais). NFS-e: `EmitirNfse` (**P1-02** Feito); demais métodos (**P1-03+**) ainda abertos. DTO emissão `Nfse`/`Servico` — **P1-01** Feito. Consulta de manifestação (**P2-11** / `ConsultaManifestacao`) já está.

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
- `CancellationToken` opcional propagado até `SendAsync` (Post/PostMultipart/Get tipado e não tipado/GetBytes/Put/Delete).
- `Delete` usa path relativo ao `BaseAddress`, igual aos demais verbos.
- `Get` (não tipado) retorna `ApiResponse` com body em `Message` (usado por `SetupSat` / P2-09, `ConsultaSat` / P2-10 e `ConsultaManifestacao` / P2-11 quando schema de resposta é incerto).
- `ConsultaManifestacao` chama URL absoluta em `api2.enotasgw.com.br` (v3); a base padrão do client permanece `api.enotasgw.com.br`.
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
Representar payloads de request/response da API (V2 NF-e/NFC-e; DTO emissão NFS-e V1 em `Nfse`/`Servico`).

### Caminhos principais
- `eNotas.Sharp/Models/Nota.cs` (agregado raiz de emissão NF-e/NFC-e)
- `Nfse.cs`, `Servico.cs` (emissão NFS-e V1 — **P1-01**; reutiliza `Cliente`/`Endereco`; consumidos por `EmitirNfse` / **P1-02**)
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
V1 NFS-e: DTO emissão (`Nfse`/`Servico`, **P1-01**) e `EmitirNfse` (**P1-02**) no pacote; demais métodos P1-03+. V2 permanece referência primária para NF-e/NFC-e/empresas.
