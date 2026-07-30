# Deploy

## Como o deploy parece funcionar

**Fato observado:** o `.csproj` define `GeneratePackageOnBuild=True`, `PackageId` implícito pelo nome do projeto, `Version`, `PackageReadmeFile`, `PackageIcon`, `RepositoryUrl` e tags NuGet.

**Inferência baseada no código:** o “deploy” deste repositório é a geração e publicação do pacote NuGet `eNotas.Sharp`, não o deploy de um serviço.

**Não identificado claramente no repositório:** pipeline CI/CD, script de `dotnet nuget push`, ou credenciais/fonte NuGet automatizada.

## Scripts envolvidos

Não há scripts de publish versionados.

Comandos típicos (CLI padrão, não script do repo):

```powershell
dotnet build eNotas.Sharp/eNotas.Sharp.csproj -c Release
# Artefato: eNotas.Sharp/bin/Release/eNotas.Sharp.{Version}.nupkg
# Publicação (exemplo genérico — validar fonte e política do time):
# dotnet nuget push <pacote.nupkg> --api-key <SECRET> --source https://api.nuget.org/v3/index.json
```

## Ambientes

| Ambiente | No código |
|----------|-----------|
| Build local Debug/Release | Sim (padrão MSBuild) |
| NuGet (consumo público) | Inferido via metadados do pacote |
| API eNotas prod/homolog | Controlado fora da lib (API Key + `ambienteEmissao`) |

## Configurações importantes

Arquivo: `eNotas.Sharp/eNotas.Sharp.csproj`

- `Version` — versão do pacote NuGet (consumidores)
- `AssemblyVersion` — atualmente pode divergir de `Version` (**fato observado**)
- `PackageReadmeFile` → `README.md` da raiz
- `PackageIcon` → `Solution Items/enotas.png`

## Checklist antes do deploy (publicação NuGet)

- [ ] `Version` atualizada conforme semântica acordada
- [ ] `dotnet build -c Release` OK
- [ ] README reflete métodos públicos
- [ ] Diff revisado (sem secrets)
- [ ] Breaking changes comunicados
- [ ] Validação manual dos fluxos afetados (ver `TESTING.md`)
- [ ] Confirmação humana para publish em nuget.org

## Riscos de produção

- Publicar breaking change em versão minor/patch.
- Publicar com serialização incompatível com a API → falhas fiscais nos consumidores.
- Vazamento de API Key em exemplos commitados junto da release.

## Rollback, se identificado

**Não identificado** no repositório. No ecossistema NuGet, rollback típico é unlist da versão e publicação de versão corretiva — **precisa de validação humana** quanto à política do mantenedor.

## Pontos não identificados

- Conta/organização NuGet usada
- Se há feed privado além do NuGet.org
- Automação de release (GitHub Actions, etc.)
