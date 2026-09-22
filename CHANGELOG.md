# Changelog

Notas por versão do pacote NuGet (`Version` em `eNotas.Sharp/eNotas.Sharp.csproj`).  
`AssemblyVersion` pode divergir — ver `docs/ARCHITECTURE.md` / `docs/DEPLOYMENT.md`.

## 1.31.0

`Iten` ganha `nfeReferenciada` (`chaveAcesso` + `numeroItem`), o mesmo contrato da raiz. Campo opcional, omitido quando nulo. O gateway ainda não gera `DFeReferenciado` a partir dele.

## 1.30.0

Estende os modelos de request NFS-e (G7 da onda NFS-e no repositório `exclusiva-monorepo`) para cobrir os campos que faltavam contra a API oficial, igualando o request ao response já tipado em `ConsultaNfse`.

### Superfície pública (resumo)

- **`Nfse`:** adiciona `dataCompetencia`, `naturezaOperacao`, `observacoes`, `dadosAdicionaisEmail`, `deducoes`, `descontos`, `descontoCondicionado`.
- **`Servico`:** adiciona `exportacao`, `regimeEspecialTributacao`, `tipoImunidadeIss`, `paisPrestacaoServico`, `ufPrestacaoServico`, `exigibilidadeSuspensa`, `pisCofinsApuracaoPropria`, `situacaoTributariaPisCofins`, `tipoRetencaoPisCofins`, `valorPis`, `valorCofins`, `valorCsll`, `valorInss`, `valorIr`.
- **Novos modelos auxiliares:** `DadosAdicionaisEmail`, `ExigibilidadeSuspensa`, `PisCofinsApuracaoPropria`.

### Detalhes

| Modelo | Campos | Tipo |
|--------|--------|------|
| `Nfse` | `dataCompetencia` | `DateTimeOffset?` (serialização UTC ISO 8601 via `CustomDateTimeConverter`, mesmo padrão de `ConsultaNfse`) |
| `Nfse` | `naturezaOperacao`, `observacoes` | `string` |
| `Nfse` | `dadosAdicionaisEmail` | `DadosAdicionaisEmail` |
| `Nfse` | `deducoes`, `descontos`, `descontoCondicionado` | `decimal?` |
| `Servico` | `exportacao` | `bool?` |
| `Servico` | `regimeEspecialTributacao`, `tipoImunidadeIss`, `paisPrestacaoServico`, `ufPrestacaoServico`, `situacaoTributariaPisCofins`, `tipoRetencaoPisCofins` | `string` |
| `Servico` | `exigibilidadeSuspensa` | `ExigibilidadeSuspensa` |
| `Servico` | `pisCofinsApuracaoPropria` | `PisCofinsApuracaoPropria` |
| `Servico` | `valorPis`, `valorCofins`, `valorCsll`, `valorInss`, `valorIr` | `decimal?` |

Mudança 100% aditiva; nenhum campo existente foi alterado ou removido.

## 1.29.0

Release que fecha o ciclo NFS-e (P1) + empresas/SAT/consulta de manifestação (P2 Agente-pronto) e correções de contrato pós-review.

### Superfície pública (resumo)

- **NFS-e (API V1):** emitir, consultar (id GW / idExterno), listar, cancelar, XML, PDF, apoio municipal, campos Reforma em `Servico` (`ServicoIbsCbs`).
- **Empresas / SAT:** logo, habilitar/desabilitar, `SetupSat`, `ConsultaSat`.
- **Manifestação:** `ConsultaManifestacao` via host `api2` (envio P2-12 ainda bloqueado).

### Correções nesta linha (FIX-01..05)

| ID | Mudança |
|----|---------|
| FIX-01 | `Nfse`: request serializa `enviarPorEmail` (KB 170286); `EnviadaPorEmail` Obsolete+alias |
| FIX-02 | `Uri.EscapeDataString` em paths NFS-e `porIdExterno` |
| FIX-03 | `numeroRps` / `serieRps` opcionais no request `Nfse` (KB 173801) |
| FIX-04 | Testes/fixtures `ConsultaNfse`/`ListaNfse`; `motivoStatus` permanece `string` (V1) |
| FIX-05 | RoundTrip NFS-e com `JToken.DeepEquals` (shape JSON real) |

### Empacotamento

- Package tag NuGet inclui `nfse`.
- Preferir publicar **apenas** esta versão (não intermediários 1.12–1.28), após `dotnet test` Release e checklist em `docs/DEPLOYMENT.md`.

### Fora desta versão

- P2-12 Enviar manifestação (Bloqueado — path/verbo oficiais).
- Gaps P0 de auditoria de aninhados (Inferência) e itens P3 sem ID Agente-pronto.
