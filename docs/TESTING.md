# Testes e Validação

## Estratégia atual de testes

**Fato observado:** não há projetos de teste, arquivos `*Test*.cs`, nem configuração de framework de testes (xUnit, NUnit, MSTest) no repositório.

Validação atual aparente: build da library + uso manual / integração em sistemas consumidores.

## Como rodar testes

Não há comando de teste no repositório.

Validação mínima disponível:

```powershell
dotnet build eNotas.Sharp.sln -c Release
```

## Tipos de teste encontrados

**Não identificado.**

## Lacunas de teste

1. Serialização/deserialização JSON dos models (`Nota`, `Impostos`, `Consulta`).
2. Deserialização XML (`NfeProc`, cancelamento, inutilização, CC-e).
3. Montagem de paths e métodos HTTP (mocks de `HttpMessageHandler`).
4. Contrato de `ApiResponse` em sucesso e erro.
5. Regressão ao adicionar campos tributários.

## Checklist de validação manual

- [ ] `dotnet build` Release sem erros
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

## Recomendações iniciais

Prioridade sugerida (não implementada nesta entrega de governança):

1. Projeto `eNotas.Sharp.Tests` com testes de serialização a partir de fixtures JSON/XML (sem chamar a API).
2. Testes de contrato do `RestService` com `HttpMessageHandler` fake.
3. Opcional: smoke test manual documentado com dados de homologação (secrets fora do Git).
