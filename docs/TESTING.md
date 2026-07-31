# Testes e Validação

## Estratégia atual de testes

Há um projeto `eNotas.Sharp.Tests` (xUnit, `net8.0`) cobrindo:

1. Serialização/deserialização JSON (`Nota`, `Consulta`, `Inutilizacao`, `CartaCorrecao`, `Impostos`/`IbsCbs` completo e simplificado, `Empresa`/`ConfiguracoesNfse`/`ListaEmpresas`, `Nfse`/`Servico`) e `CustomDateTimeConverter`
2. Deserialização XML (`NfeProc`, cancelamento, inutilização, CC-e)
3. Smoke de paths/auth/`ApiResponse` de **todos** os métodos públicos do `eNotasClient` com `HttpMessageHandler` fake (inclui cancelamento e host absoluto `api2` em `ConsultaManifestacao`)
4. Edge cases do `RestService` (JSON inválido, exceção de rede, `Put`, `Delete` path relativo, `GetBytes` binário, `PostMultipart`, `CancellationToken`)

Validação complementar: build da library + checklist manual / integração em sistemas consumidores.

## Como rodar testes

```powershell
dotnet test eNotas.Sharp.sln -c Release
```

Validação de build:

```powershell
dotnet build eNotas.Sharp.sln -c Release
```

## Tipos de teste encontrados

| Tipo | Local | Observação |
|------|-------|------------|
| Serialização JSON | `NotaSerializationTests`, `ConsultaSerializationTests`, `InutilizacaoSerializationTests`, `CartaCorrecaoSerializationTests`, `ImpostosSerializationTests`, `EmpresaSerializationTests`, `NfseSerializationTests`, `CustomDateTimeConverterTests` | Fixtures em `eNotas.Sharp.Tests/Fixtures/` (incl. `empresa-incluir-alterar.json`, `nfse-emissao.json`) |
| XML | `NfeProcXmlTests`, `XmlDocumentTests` | Fixtures mínimas, não documento fiscal completo |
| HTTP smoke | `eNotasClientPathTests` | Métodos NF-e/NFC-e + empresas (`IncluirAlterarEmpresa`, `ConsultaEmpresa`, `ListarEmpresas`, multipart cert/logo, desabilitar/habilitar, `SetupSat`, `ConsultaSat`, `ConsultaManifestacao` host `api2`/`v3`); auth Basic; sucesso e 4xx; `CancellationToken` cancelado |
| RestService | `RestServiceTests` | Parse JSON/XML inválido deixa `Object` null e preenche `Exception`; exceção de rede não relança; `Put`; `Get` não tipado (body em `Message`); `Delete` path relativo; `GetBytes` (sucesso → bytes em `Object`, 4xx → `Message`); `PostMultipart` (sucesso → multipart/form-data, 4xx → `Message`); cancelamento relança `OperationCanceledException` |

## Lacunas de teste

1. Regressão tributária ampla ao adicionar campos novos além de `ibsCbs`.
2. Testes de integração com API de homologação (secrets fora do Git).

## Checklist de validação manual

- [ ] `dotnet build` / `dotnet test` Release sem erros
- [ ] Instanciar `eNotasClient` com key de homologação
- [ ] Emitir nota em ambiente de teste (`ambienteEmissao` adequado)
- [ ] Consultar a mesma nota e verificar `Consulta`
- [ ] Baixar XML e confirmar parse (`Object` não nulo quando sucesso)
- [ ] Conferir JSON enviado (proxy/Fiddler ou log do consumidor) vs Postman V2

## Checklist para fluxos críticos

Antes de publicar pacote que altere emissão/cancelamento/inutilização/CC-e:

- [ ] Comparar payload com `docs/API - eNotas - V2 - NF-e - NFC-e.postman_collection.json`
- [ ] Testar NF-e e NFC-e se o model compartilhado mudou
- [ ] Confirmar que propriedades antigas ainda serializam
- [ ] Revisar header de autenticação inalterado
- [ ] Revisão humana do diff

## Fixtures

Fixtures em `eNotas.Sharp.Tests/Fixtures/` usam dados fictícios de homologação. Não incluir API Keys, cookies ou dados fiscais reais.
