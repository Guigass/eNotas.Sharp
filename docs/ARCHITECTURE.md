# Arquitetura

## Visão geral

**Fato observado:** arquitetura em camadas simples de uma biblioteca cliente:

```
Consumidor (app externo)
        │
        ▼
 eNotasClient (público)     ← Clients/
        │
        ▼
 RestService (internal)     ← Services/
        │
        ▼
 API eNotas GW (HTTPS)      ← https://api.enotasgw.com.br
                                 (+ api2.enotasgw.com.br só em ConsultaManifestacao)
```

Modelos em `Models/` são serializados/deserializados nas bordas HTTP. Não há camada de domínio rica, persistência ou UI.

## Estrutura de pastas

```
eNotas.Sharp.sln
README.md
.gitignore
docs/                          # Postman + documentação operacional
eNotas.Sharp/
  eNotas.Sharp.csproj
  Clients/eNotasClient.cs
  Services/RestService.cs
  Helpers/CustomDateTimeConverter.cs
  Models/                      # DTOs JSON + classes XML
  Exemplos/EmissaoNfeHomologacao/  # console NF-e homologação (env vars)
eNotas.Sharp.Tests/            # xUnit (serialização + smoke HTTP)
Solution Items/enotas.png      # ícone do pacote NuGet
.cursor/                       # rules, skills, agents
```

## Principais camadas

| Camada | Visibilidade | Papel |
|--------|--------------|--------|
| Client | `public` | Orquestra paths REST e tipagem de retorno |
| Service | `internal` | HTTP, headers, serialize/deserialize |
| Models | `public` | Contratos alinhados ao JSON/XML da API |
| Helpers | `public` | Converter de data para JSON |

## Fluxo de dados

1. Consumidor instancia `eNotasClient(apiKey)`.
2. Construtor cria `RestService` com base URL fixa e header `Authorization: Basic {apiKey}`.
3. Métodos do client montam o path (`/v2/empresas/{empresaId}/...` ou `/v2/empresas` para incluir/alterar/listar) e chamam `Post` / `Get` / `Delete`, com `CancellationToken` opcional. Exceção: `ConsultaManifestacao` usa URL absoluta em `https://api2.enotasgw.com.br/v3/...` (host distinto; base padrão inalterada).
4. Request: objeto → `JsonConvert.SerializeObject` (UTC).
5. Response: string em `ApiResponse.Message`; se tipado, também `Object` (JSON ou XML conforme parâmetro `deserializer`).
6. Exceções de rede/processamento vão para `ApiResponse.Exception` sem relançar (**fato observado**).

## Dependências internas

- `eNotasClient` → `RestService` + `Models`
- `RestService` → Newtonsoft.Json, HttpClient, XmlSerializer, `ApiResponse`
- Models de emissão → composição (`Nota` → `Cliente`, `Iten`, `Impostos`, `Pedido`, `Transporte`, …); NFS-e V1 → `Nfse` → `Cliente`, `Servico` (**P1-01**; `EmitirNfse` / **P1-02**); consulta NFS-e → `ConsultaNfse` (**P1-03**/**P1-04**; ≠ `Consulta` de NF-e/NFC-e); lista NFS-e → `ListaNfse` (**P1-05**)
- Models XML (`Xml*.cs`) → namespaces próprios aninhados sob `eNotas.Sharp.Models`

## Dependências externas

- API eNotas Gateway (`api.enotasgw.com.br`; manifestação consulta também `api2.enotasgw.com.br`)
- Pacote NuGet `Newtonsoft.Json`

## Pontos de entrada

- **Biblioteca:** `eNotas.Sharp.Clients.eNotasClient`
- **Pacote:** build do `eNotas.Sharp.csproj` gera `.nupkg`
- **Documentação de API de referência:** `docs/API - eNotas - V2 - NF-e - NFC-e.postman_collection.json`

## Fronteiras arquiteturais

- Não alterar a API pública (`eNotasClient` e models públicos) sem considerar breaking change no NuGet.
- `RestService` deve permanecer `internal` (detalhe de implementação).
- Novos endpoints devem seguir o padrão de regiões `#region NFe` / `#region NFCe` / `#region Empresas` no client.
- Esta biblioteca **não** deve incorporar UI, banco ou lógica de negócio do consumidor.

## Decisões observadas

- Target único `netstandard2.0` para ampla compatibilidade.
- Uso de `partial class` nos models (possível origem em geração/código expandido).
- `NullValueHandling.Ignore` na maioria das propriedades JSON.
- Datas de emissão/consulta com `CustomDateTimeConverter` (escrita em formato ISO/`o` UTC).
- Autenticação Basic com a API Key no valor do header (padrão observado no código; confirmar com docs oficiais da eNotas se necessário).

## Inferências arquiteturais

- Classes XML grandes (`Xml.cs`, etc.) parecem mapeamento direto do schema/retorno da SEFAZ via gateway.
- `NotaWebhook` sugere suporte a payload de webhook no lado do consumidor, sem receber webhooks nesta lib.
- Coleção Postman V1 (NFS-e) é referência de contrato para o backlog **P1**; DTO emissão (`Nfse`/`Servico`, **P1-01**), `EmitirNfse` (**P1-02**), `ConsultaNfse` (**P1-03**), `ConsultaNfsePorIdExterno` (**P1-04**), `ListarNfse` (**P1-05**, model `ListaNfse`), `CancelaNfse` (**P1-06**), `CancelaNfsePorIdExterno` (**P1-07**), `ConsultaNfseXML`/`ConsultaNfseXMLPorIdExterno` (**P1-08**, XML em `Message`), `ConsultaNfsePDF`/`ConsultaNfsePDFPorIdExterno` (**P1-09**, bytes em `Object` via `GetBytes`), apoio municipal (**P1-10**, body em `Message`) no client; demais métodos P1-11+.

## Riscos arquiteturais

- Falha de deserialização no `Get` (JSON/XML) preenche `ApiResponse.Exception` mantendo `Message` bruto e `Object` nulo (**fato**).
- `CancellationToken` opcional nos métodos do client e do `RestService`; cancelamento relança `OperationCanceledException` e não preenche `ApiResponse` (**fato**).
- `Delete` usa path relativo ao `BaseAddress` via `SendAsync` + `HttpMethod.Delete`, no mesmo padrão de Post/Get/Put (**fato**).
- `GetBytes` (interno) retorna `ApiResponse<byte[]>` via `ReadAsByteArrayAsync`; sucesso preenche `Object`, falha HTTP preenche `Message` com o body em UTF-8 (**fato**).
- `PostMultipart` (interno) envia `MultipartFormDataContent` via POST; preenche `ApiResponse` como `Post` (Status/IsSuccess/Message); Content-Type multipart com boundary vem do conteúdo (**fato**).
- `Dispose` do client chama `GC.Collect()` — padrão atípico e potencialmente custoso.
- `Version` do pacote e `AssemblyVersion` divergem no `.csproj`.
- Suite mínima cobre serialização e paths; não substitui teste de integração contra a API.

## Recomendações

1. Ao adicionar endpoint: espelhar método no client + model + referência Postman + bump de versão NuGet.
2. Preferir mudanças aditivas (novas propriedades opcionais) a renomeações.
3. Documentar breaking changes no README/CHANGELOG quando houver.
4. Manter/estender testes de serialização e smoke de paths (ver `TESTING.md`).
