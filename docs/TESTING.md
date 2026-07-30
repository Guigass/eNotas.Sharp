# Testes e Validação

## Estratégia atual de testes

Há um projeto `eNotas.Sharp.Tests` (xUnit, `net8.0`) com suite mínima:

1. Serialização/deserialização JSON dos models (`Nota`, `Consulta`) e `CustomDateTimeConverter`
2. Deserialização XML mínima (`NfeProc`)
3. Smoke de paths/auth/`ApiResponse` do `eNotasClient` com `HttpMessageHandler` fake (sem chamar a API)

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
| Serialização JSON | `NotaSerializationTests`, `ConsultaSerializationTests`, `CustomDateTimeConverterTests` | Fixtures em `eNotas.Sharp.Tests/Fixtures/` |
| XML | `NfeProcXmlTests` | Fixture mínima, não documento fiscal completo |
| HTTP smoke | `eNotasClientPathTests` | Paths NF-e/NFC-e, auth Basic, sucesso e 4xx |

## Lacunas de teste

1. Cobertura de todos os métodos do client (inutilização, CC-e, XMLs via HTTP).
2. Regressão tributária ampla ao adicionar campos.
3. Testes de integração com API de homologação (secrets fora do Git).

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
