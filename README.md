# eNotas.Sharp
Biblioteca em C# (.Net Standard) para uso dos Endpoints da eNotas.

**Versão NuGet:** `1.29.0` — notas em [CHANGELOG.md](CHANGELOG.md).

## Documentação

| Documento | Conteúdo |
|-----------|----------|
| [Visão geral](docs/PROJECT_OVERVIEW.md) | Propósito, stack, pontos de atenção |
| [Roadmap](docs/ROADMAP.md) | Lacunas e backlog priorizado (P0–P3) |
| [Changelog](CHANGELOG.md) | Notas por versão NuGet |
| [Arquitetura](docs/ARCHITECTURE.md) | Camadas Client → Service → Models |
| [Módulos](docs/MODULES.md) | Responsabilidades por área |
| [Domínio](docs/DOMAIN.md) | Fluxos NF-e/NFC-e e áreas sensíveis |
| [Desenvolvimento](docs/DEVELOPMENT_GUIDE.md) | Build, features, commits |
| [Ambiente](docs/ENVIRONMENT.md) | API Key, URL, requisitos |
| [Testes](docs/TESTING.md) | Suite xUnit + checklist manual |
| [Deploy](docs/DEPLOYMENT.md) | Empacotamento NuGet |
| [Troubleshooting](docs/TROUBLESHOOTING.md) | Falhas comuns |
| [Workflow agentico](docs/AGENTIC_WORKFLOW.md) | Rules, skills e agents Cursor |

Referência de API (Postman):

| Coleção | Escopo | Paths típicos |
|---------|--------|---------------|
| `docs/API - eNotas - V2 - NF-e - NFC-e.postman_collection.json` | NF-e, NFC-e, empresas, SAT | `/v2/empresas/.../nf-e`, `/nfc-e`, `/v2/empresas` |
| `docs/API - eNotas - V1 - NFS-e.postman_collection.json` | NFS-e e apoio municipal | `/v1/empresas/.../nfes`, `/v1/estados/...`, `/v1/servicos/...` |

**Não misturar:** V2 usa `nf-e`/`nfc-e` (mercadorias); V1 usa `nfes` (serviço). Mesmo host base (`https://api.enotasgw.com.br`), versões e models distintos. Docs oficiais: [API V2](https://docs.notagateway.com.br/v2/docs/sobre-a-api) · [API V1](https://docs.notagateway.com.br/docs).

Documentação oficial NotaGateway: [Central de ajuda](https://atendimento.notagateway.com.br/kb/pt-br) — consulta via agent `notagateway-docs-specialist` / skill `notagateway-kb-lookup`.

Governança Cursor: `.cursor/rules`, `.cursor/skills`, `.cursor/agents` (comece por `.cursor/agents/agent-router.md`).

- Exemplo de Uso (NF-e)
    ```
    // API Key e empresaId via configuração do consumidor — nunca commitados.
    using (var enotas = new eNotasClient(apiKey))
    {
        var nota = new Nota
        {
            //......
        };
        var response = await enotas.EmitirNfe(nota, empresaId);
        // Ou com cancelamento:
        // var resp = await enotas.EmitirNfe(nota, empresaId, cancellationToken);
    }
    ```

Sample compilável (homologação NF-e, sem API Key no código): [`Exemplos/EmissaoNfeHomologacao`](Exemplos/EmissaoNfeHomologacao) — define `ENOTAS_API_KEY` e `ENOTAS_EMPRESA_ID` antes de `dotnet run`.

Métodos públicos aceitam `CancellationToken cancellationToken = default` (opcional; source-compatible).
--------------------------------------------------------------------------------------------------
- Instalação no Nuget PM
    ```
    Install-Package eNotas.Sharp
    # ou pin: Install-Package eNotas.Sharp -Version 1.29.0
    ```
--------------------------------------------------------------------------------------------------

## NFS-e (API V1) ≠ NF-e / NFC-e (API V2)

NFS-e usa a **API V1** (`/v1/.../nfes`), models próprios (`Nfse`, `Servico`, `ConsultaNfse`, `ListaNfse`) e a região `#region NFSe` em `eNotasClient`. Não reutilize `Nota` / `Consulta` / paths V2 `nf-e`/`nfc-e` para serviço.

Contrato: Postman V1 + [KB 173803](https://atendimento.notagateway.com.br/kb/pt-br/article/173803/baixar-o-pdf-ou-xml-de-uma-nota-fiscal) (PDF/XML). XML em `ApiResponse.Message`; PDF em `ApiResponse<byte[]>.Object`.

- Exemplo de uso (NFS-e) — sem API Key no código:
    ```
    using (var enotas = new eNotasClient(apiKey))
    {
        var nfse = new Nfse
        {
            // tipo, idExterno, ambienteEmissao, cliente, servico, valorTotal, ...
        };
        var emitida = await enotas.EmitirNfse(nfse, empresaId);
        var consulta = await enotas.ConsultaNfse(nfeId, empresaId);
        // var porExt = await enotas.ConsultaNfsePorIdExterno(idExterno, empresaId);
        // var xml = await enotas.ConsultaNfseXML(nfeId, empresaId); // Message
        // var pdf = await enotas.ConsultaNfsePDF(nfeId, empresaId); // Object = byte[]
    }
    ```

Testes offline (sem API Key): `NfseSerializationTests` + fixtures `nfse-emissao.json` / `nfse-reforma.json`; smoke de paths em `eNotasClientPathTests`.

### Métodos NFS-e no client

| Método | Path |
|--------|------|
| `EmitirNfse` | `POST /v1/empresas/{empresaId}/nfes` |
| `ConsultaNfse` | `GET /v1/empresas/{empresaId}/nfes/{nfeId}` |
| `ConsultaNfsePorIdExterno` | `GET /v1/empresas/{empresaId}/nfes/porIdExterno/{idExterno}` |
| `ListarNfse` | `GET /v1/empresas/{empresaId}/nfes?pageNumber&pageSize&sortBy&sortDirection&filter` |
| `CancelaNfse` | `DELETE /v1/empresas/{empresaId}/nfes/{nfeId}` |
| `CancelaNfsePorIdExterno` | `DELETE /v1/empresas/{empresaId}/nfes/porIdExterno/{idExterno}` |
| `ConsultaNfseXML` | `GET .../nfes/{nfeId}/xml` (XML em `Message`) |
| `ConsultaNfseXMLPorIdExterno` | `GET .../porIdExterno/{idExterno}/xml` |
| `ConsultaNfsePDF` | `GET .../nfes/{nfeId}/pdf` (bytes em `Object`) |
| `ConsultaNfsePDFPorIdExterno` | `GET .../porIdExterno/{idExterno}/pdf` |
| `ConsultaServicosMunicipais` | `GET /v1/estados/{uf}/cidades/{nome}/servicos?...` (body em `Message`) |
| `ConsultaServicosMunicipaisUnificados` | `GET /v1/servicos/cidades?...` |
| `ConsultaProvedorCidade` | `GET /v1/estados/cidades/{codigoIBGECidade}/provedor` |
| `CriticarDadosObrigatorios` | `GET /v1/empresas/{empresaId}/criticardadosobrigatorios` |

Models: `Nfse`/`Servico` (emissão: `enviarPorEmail`, opcionais `numeroRps`/`serieRps`; Reforma: `codigoNBS`, `codigoTributacaoNacional`, `ServicoIbsCbs` — distinto de `IbsCbs` NF-e/NFC-e), `ConsultaNfse`, `ListaNfse`. Paths `porIdExterno` escapam `idExterno` na URI.

--------------------------------------------------------------------------------------------------

- Métodos Disponíveis (NF-e / NFC-e / empresas / SAT / manifestação):
    ```
    * Emitir NF-e
    * Consultar NF-e
    * Consultar XML NF-e
    * Cancelar NF-e
    * Consultar XML de Cancelamento NF-e (ConsultaNfeXMLCancelamento)
    * Emitir NFC-e
    * Consultar NFC-e
    * Consultar XML NFC-e
    * Cancelar NFC-e
    * Consultar XML de Cancelamento NFC-e
    * Inutilizar Numeração NF-e
    * Consultar Inutilização de Número da Nota Fiscal NF-e
    * Consultar XML de Inutilização NF-e
    * Inutilizar Numeração NFC-e
    * Consultar Inutilização de Número da Nota Fiscal NFC-e
    * Consultar XML de Inutilização NFC-e
    * Emitir Carta de Correção pela Chave da NF-e
    * Consultar Carta de Correção NF-e
    * Consultar XML da Carta de Correção NF-e
    * Incluir/Alterar Empresa (`IncluirAlterarEmpresa`)
    * Consultar Empresa (`ConsultaEmpresa`)
    * Listar Empresas (`ListarEmpresas`)
    * Vincular Certificado Digital (`VincularCertificadoDigital`)
    * Vincular Logotipo (`VincularLogotipo`) — JPG/PNG/GIF
    * Desabilitar Empresa (`DesabilitarEmpresa`)
    * Habilitar Empresa (`HabilitarEmpresa`)
    * Setup SAT (`SetupSat`) — body bruto em `ApiResponse.Message` (schema Postman incerto)
    * Consultar SAT (`ConsultaSat`) — parâmetro `satId` no path `GET /v2/sat/{satId}/all`; body bruto em `ApiResponse.Message` (schema Postman incerto)
    * Consultar Manifestação de Destinatário NF-e (`ConsultaManifestacao`) — `GET` absoluto em `https://api2.enotasgw.com.br/v3/empresas/{empresaId}/nf-e/manifestacao/{chaveAcesso}` (host **api2**, distinto da base padrão `api.enotasgw.com.br`); body bruto em `ApiResponse.Message` (schema Postman vazio)
    ```

NFS-e: ver seção **NFS-e (API V1)** acima (métodos reais do `#region NFSe`).

Backlog detalhado: [docs/ROADMAP.md](docs/ROADMAP.md).

Empresas / SAT / consulta de manifestação: models `Empresa`, `ConfiguracoesNfse`, `ListaEmpresas`; certificado e logo usam multipart; `SetupSat`, `ConsultaSat` e `ConsultaManifestacao` devolvem body bruto em `ApiResponse.Message` quando o schema Postman é incerto.

- Ainda não no client (ver [roadmap](docs/ROADMAP.md)):
    ```
    * Enviar Manifestação de Destinatário NF-e (P2-12 — bloqueado até path/verbo oficiais)
    ```

P0 (campos opcionais de emissão NF-e/NFC-e): parcialmente feito (`tipo`, contingência, `indicadorPresencaConsumidor`, campos de `Iten`, `enviarPorEmail`, model `IbsCbs`); gaps restantes em [docs/ROADMAP.md](docs/ROADMAP.md).
