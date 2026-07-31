# Visão Geral do Projeto

## Propósito

**Fato observado:** `eNotas.Sharp` é uma biblioteca C# (.NET Standard 2.0) que encapsula o consumo dos endpoints da API eNotas Gateway para NF-e e NFC-e.

Publicada como pacote NuGet (`Install-Package eNotas.Sharp`), versão atual do pacote declarada em `eNotas.Sharp/eNotas.Sharp.csproj`.

## Stack resumida

| Item | Valor |
|------|--------|
| Linguagem | C# |
| Target | `netstandard2.0` |
| Serialização JSON | Newtonsoft.Json 13.0.1 |
| HTTP | `System.Net.Http.HttpClient` |
| XML | `System.Xml.Serialization.XmlSerializer` |
| Empacotamento | NuGet (`GeneratePackageOnBuild=True`) |
| Repositório | https://github.com/Guigass/eNotas.Sharp |

## Principais responsabilidades

1. Expor um client público (`eNotasClient`) para emitir, consultar, cancelar e inutilizar NF-e/NFC-e, e para carta de correção.
2. Mapear payloads da API em modelos POCOs com `[JsonProperty]`.
3. Deserializar respostas JSON e XMLs fiscais (procNFe, cancelamento, inutilização, CC-e).
4. Autenticar chamadas com API Key via header `Authorization: Basic {apiKey}`.

## Principais módulos

- `eNotas.Sharp/Clients/eNotasClient.cs` — fachada pública
- `eNotas.Sharp/Services/RestService.cs` — transporte HTTP (interno)
- `eNotas.Sharp/Models/` — contratos de request/response e XML
- `eNotas.Sharp/Helpers/CustomDateTimeConverter.cs` — serialização de datas
- `eNotas.Sharp.Tests/` — suite xUnit (serialização + smoke HTTP)

## Fluxos mais importantes

- Emissão NF-e / NFC-e
- Consulta de status e XML
- Cancelamento
- Inutilização de numeração
- Carta de correção (NF-e)

Ver `DOMAIN.md` e `ARCHITECTURE.md`.

## Como este projeto se encaixa no negócio ou no contexto técnico

**Inferência baseada no código:** a biblioteca é um SDK cliente usado por sistemas consumidores (ERPs, e-commerces, backends) para integrar fiscalmente com o gateway eNotas, sem implementar a comunicação HTTP/XML manualmente.

## Pontos de atenção

- Operações fiscais podem ser irreversíveis (cancelamento, inutilização, emissão em produção).
- API Key é credencial sensível; não deve ser commitada.
- Contratos JSON devem permanecer alinhados à API oficial e às coleções Postman em `docs/`.
- Há suite mínima xUnit em `eNotas.Sharp.Tests`; integração com API real permanece manual (`TESTING.md`).
- Sample compilável em `Exemplos/EmissaoNfeHomologacao` (credenciais via env; sem API Key no código).
- README lista NFS-e e restante da gestão de empresas (SAT) como futuros; `IncluirAlterarEmpresa` (**P2-02**), `ConsultaEmpresa` (**P2-03**), `ListarEmpresas` (**P2-04**), `VincularCertificadoDigital` (**P2-05**), `VincularLogotipo` (**P2-06**), `DesabilitarEmpresa` (**P2-07**) e `HabilitarEmpresa` (**P2-08**) já estão no client. Postman V1 cobre NFS-e, mas o client C# ainda não implementa esses métodos.
- Lacunas e priorização do backlog estão em `ROADMAP.md` (gaps restantes de models, NFS-e, empresas/manifestação/SAT, qualidade).

## Onde começar

1. Ler este overview e `ARCHITECTURE.md`.
2. Para o que falta na lib: `ROADMAP.md`.
3. Abrir `eNotas.Sharp/Clients/eNotasClient.cs` (ponto de entrada).
4. Estudar `Models/Nota.cs`, `Models/Iten.cs`, `Models/Impostos.cs`.
5. Consultar coleções Postman em `docs/` para contrato da API.
6. Consultar a [KB NotaGateway](https://atendimento.notagateway.com.br/kb/pt-br) (agent `notagateway-docs-specialist`).
7. Para trabalho com agentes: `AGENTIC_WORKFLOW.md` e `.cursor/agents/agent-router.md`.

## Informações não identificadas

- Pipeline de CI/CD
- Processo formal de publicação no NuGet.org
- Ambiente de sandbox/homologação documentado no código (além do campo `ambienteEmissao` nos modelos)
- Testes de integração automatizados contra a API real (só suite offline + checklist manual)
- Documentação oficial da eNotas versionada junto ao código

## Pontos que precisam de validação humana

- Se a publicação NuGet é manual ou automatizada fora do repositório.
- Quais campos tributários novos além do `IbsCbs` tipado (KB 595993) ainda faltam nos demais aninhados.
- Prioridade e escopo de NFS-e / empresas / manifestação — ver lista em `ROADMAP.md`.
- Se `NotaWebhook` é usado apenas como DTO pelo consumidor ou se haverá suporte futuro no client.
