# eNotas.Sharp
Biblioteca em C# (.Net Standard) para uso dos Endpoints da eNotas.

## Documentação

| Documento | Conteúdo |
|-----------|----------|
| [Visão geral](docs/PROJECT_OVERVIEW.md) | Propósito, stack, pontos de atenção |
| [Roadmap](docs/ROADMAP.md) | Lacunas e backlog priorizado (P0–P3) |
| [Arquitetura](docs/ARCHITECTURE.md) | Camadas Client → Service → Models |
| [Módulos](docs/MODULES.md) | Responsabilidades por área |
| [Domínio](docs/DOMAIN.md) | Fluxos NF-e/NFC-e e áreas sensíveis |
| [Desenvolvimento](docs/DEVELOPMENT_GUIDE.md) | Build, features, commits |
| [Ambiente](docs/ENVIRONMENT.md) | API Key, URL, requisitos |
| [Testes](docs/TESTING.md) | Suite xUnit + checklist manual |
| [Deploy](docs/DEPLOYMENT.md) | Empacotamento NuGet |
| [Troubleshooting](docs/TROUBLESHOOTING.md) | Falhas comuns |
| [Workflow agentico](docs/AGENTIC_WORKFLOW.md) | Rules, skills e agents Cursor |

Referência de API (Postman): `docs/API - eNotas - V2 - NF-e - NFC-e.postman_collection.json` (client atual). A coleção V1 NFS-e é referência futura/não implementada no client C#.

Documentação oficial NotaGateway: [Central de ajuda](https://atendimento.notagateway.com.br/kb/pt-br) — consulta via agent `notagateway-docs-specialist` / skill `notagateway-kb-lookup`.

Governança Cursor: `.cursor/rules`, `.cursor/skills`, `.cursor/agents` (comece por `.cursor/agents/agent-router.md`).

- Exemplo de Uso
    ```
    //Uso da biblioteca é simples basta dar o using passando sua APIKEY, 
    //chamar o método passando o modelo nescessário e o id da empresa 
    //(APIKEY e empresaID fornecidos pela eNotas).
    using (var enotas = new eNotasClient("apiKey"))
    {
        var nota = new Nota
        {
            //......
        };
        var response = enotas.EmitirNfe(nota, "empresaID").Result;
        // Ou
        // var resp = await enotas.EmitirNfe(nota, "empresaID");
        // Ou com cancelamento:
        // var resp = await enotas.EmitirNfe(nota, "empresaID", cancellationToken);
        // Para Metodos ASYNC
    }
    ```

Sample compilável (homologação, sem API Key no código): [`Exemplos/EmissaoNfeHomologacao`](Exemplos/EmissaoNfeHomologacao) — define `ENOTAS_API_KEY` e `ENOTAS_EMPRESA_ID` antes de `dotnet run`.

Métodos públicos aceitam `CancellationToken cancellationToken = default` (opcional; source-compatible).
--------------------------------------------------------------------------------------------------
- Instalação no Nuget PM
    ```
    Install-Package eNotas.Sharp
    ```
--------------------------------------------------------------------------------------------------

- Métodos Disponíveis:
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

Backlog detalhado (prioridades, campos omitidos, NFS-e, qualidade): [docs/ROADMAP.md](docs/ROADMAP.md).

Empresas / SAT / consulta de manifestação estão na lista acima (models `Empresa`, `ConfiguracoesNfse`, `ListaEmpresas`; `Endereco` com `codigoIbgeUf`/`codigoIbgeCidade`). Certificado e logo usam multipart; `SetupSat`, `ConsultaSat` e `ConsultaManifestacao` devolvem o body bruto em `ApiResponse.Message` quando o schema Postman é incerto.

- Ainda não no client (ver [roadmap](docs/ROADMAP.md)):
    ```
    * Enviar Manifestação de Destinatário NF-e (P2-12 — bloqueado até path/verbo oficiais)
    * NFS-e (emitir, consultar, cancelar, XML, PDF) — Postman V1 / itens P1
    ```

P0 (campos opcionais de emissão): parcialmente feito (`tipo`, contingência, `indicadorPresencaConsumidor`, campos de `Iten`, `enviarPorEmail`, model `IbsCbs`); gaps restantes em [docs/ROADMAP.md](docs/ROADMAP.md).
