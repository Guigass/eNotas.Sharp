# Troubleshooting

## Problemas conhecidos

Documentados a partir do código e lacunas do repositório (não há issue tracker interno versionado).

| Problema | Evidência |
|----------|-----------|
| Sem integração automatizada com API real | Suite offline em `eNotas.Sharp.Tests`; checklist em `TESTING.md` |
| Pasta `Exemplos/` vazia | Diretório sem arquivos |
| Docs Postman V1 vs client só V2 NF-e/NFC-e | Escopo divergente |
| `Version` ≠ `AssemblyVersion` | `.csproj` |
| Possível inconsistência no `Delete` (URL absoluta vs relativa) | `RestService.Delete` |

## Erros comuns

### `IsSuccess = false` com Message da API
Ler `ApiResponse.Message` (corpo retornado pela eNotas). Conferir empresaId, payload e ambiente.

### `Object` nulo em GET tipado com `IsSuccess = true`
Se `Exception` estiver preenchido, a deserialização JSON/XML falhou: inspecionar `Message` (string bruta) e o tipo esperado. Se `Exception` for nulo, o HTTP OK pode ter retornado payload incompleto ou incompatível com o model.

### Exceção em `ApiResponse.Exception`
Rede/HTTP, ou falha de parse no `Get`. Verificar conectividade, BaseAddress e, em consultas tipadas, o corpo em `Message`.

### Nota rejeitada / status de erro na consulta
Problema geralmente de regra fiscal/payload, não da lib. Validar itens, impostos, CFOP, CST com a documentação eNotas e o Postman V2.

### Pacote NuGet não atualiza no consumidor
Limpar cache NuGet local; conferir número de `Version` publicado.

## Onde olhar logs

A biblioteca **não** implementa logging interno (**fato observado**).

Evidências úteis no consumidor:
- `ApiResponse.Status`, `Message`, `Exception`
- Logs HTTP do app hospedeiro
- Painel/histórico na plataforma eNotas (se disponível)

## Como investigar falhas

1. Capturar request JSON real e response bruta.
2. Classificar: auth · path · payload · parse JSON · parse XML · regra fiscal na API.
3. Comparar com Postman V2.
4. Reproduzir com payload mínimo.
5. Só então alterar código da library.

## Como isolar problemas

| Camada | Como isolar |
|--------|-------------|
| Auth | Chamar endpoint simples de consulta com a mesma key |
| Path | Conferir string em `eNotasClient` vs Postman |
| Payload | Serializar `Nota` e diff com exemplo Postman |
| Parse | Alimentar `Message` conhecido em teste/local de deserialize |
| Fiscal | Validar no painel eNotas / suporte do gateway |

## Como coletar evidências antes de alterar código

- [ ] `empresaId`, `notaId`, ambiente
- [ ] Timestamp da chamada
- [ ] Status HTTP / `ApiResponse.Status`
- [ ] Body (`Message`) — **redigir dados pessoais/fiscais sensíveis** se for compartilhar
- [ ] Versão do pacote `eNotas.Sharp` em uso
- [ ] Trecho do model preenchido

## Pontos que precisam ser completados futuramente

- Catálogo de códigos de erro retornados pela API
- Exemplos oficiais versionados em `Exemplos/`
- Correlação README vs métodos reais (nota sobre ConsultaNfce para XML de cancelamento NF-e)
