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
    ```

Backlog detalhado (prioridades, campos omitidos, NFS-e, qualidade): [docs/ROADMAP.md](docs/ROADMAP.md).

- Métodos em Construção (P2 — ver roadmap):
    ```
    * Manifestação de Destinatário NF-e
    ```

- Métodos Futuros (P1–P2 — ver roadmap):
    ```
    * NFS-e (emitir, consultar, cancelar, XML, PDF) — Postman V1
    * Incluir/Alterar Empresa
    * Vincular Certificado
    * Vincular Logotipo
    * Consultar Empresa
    * Listar Empresas
    * Download EXE customizado do S@T
    ```

P0 (campos opcionais de emissão): parcialmente feito (`tipo`, contingência, `indicadorPresencaConsumidor`, campos de `Iten`, `enviarPorEmail`, model `IbsCbs`); gaps restantes em [docs/ROADMAP.md](docs/ROADMAP.md).
