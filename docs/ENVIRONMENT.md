# Ambiente

## Requisitos

| Requisito | Detalhe |
|-----------|---------|
| .NET SDK | Compatível com `netstandard2.0` (library) e `net8.0` (projeto de testes) |
| IDE (opcional) | Visual Studio 2022 / VS Code / Rider |
| Conta eNotas | API Key + Empresa ID para testes reais |
| Rede | Acesso HTTPS a `api.enotasgw.com.br` |

## Variáveis de ambiente

**Não identificado** no repositório: não há `.env`, `appsettings` ou configuração de ambiente da biblioteca.

A API Key é passada no construtor:

```csharp
using (var enotas = new eNotasClient("apiKey"))
{
    // ...
}
```

**Recomendação operacional (não implementada no código):** consumidores devem ler a key de secret store / variável de ambiente da aplicação hospedeira — nunca commitá-la neste repo.

## Configurações locais

| Item | Valor observado |
|------|-----------------|
| Base URL | `https://api.enotasgw.com.br` (hardcoded em `eNotasClient`; `ConsultaManifestacao` usa URL absoluta em `https://api2.enotasgw.com.br`) |
| Auth header | `Authorization: Basic {apiKey}` |
| Content-Type request | `application/json` |

Não há switch de URL sandbox no código. O ambiente fiscal parece controlado pelo campo `ambienteEmissao` no payload (**inferência; validar com documentação eNotas**).

## Serviços externos necessários

- API eNotas Gateway (obrigatório para qualquer chamada real)
- Documentação oficial (consulta humana/agentes): [Central de ajuda NotaGateway](https://atendimento.notagateway.com.br/kb/pt-br)

Usar agent `notagateway-docs-specialist` e skill `notagateway-kb-lookup` para consultar a KB antes de decidir campos/fluxos.

## Banco de dados

**Não identificado** — a biblioteca não persiste dados.

## Cache

**Não identificado.**

## Mensageria

**Não identificado** no client. Existe model `NotaWebhook` para tipar payloads que o **consumidor** pode receber em um endpoint próprio.

## Problemas comuns de ambiente

| Sintoma | Investigação |
|---------|--------------|
| 401/403 | API Key inválida ou header incorreto |
| 404 | `empresaId` ou `notaId` incorretos; path errado |
| Timeout / rede | Firewall, proxy, DNS para `api.enotasgw.com.br` |
| XML null em `Object` | Resposta não-XML ou falha de deserialize (`Exception` + `Message`) |
| Falha de build | SDK ausente; restaurar pacotes (`dotnet restore`) |

## Itens não identificados

- URL alternativa de homologação da API
- Escopo/format exact esperado da API Key no Basic auth
- Rate limits e políticas de retry da eNotas
