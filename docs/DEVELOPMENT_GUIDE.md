# Guia de Desenvolvimento

## Como rodar o projeto

**Fato observado:** é uma class library, não uma aplicação executável.

```powershell
cd D:\Projetos\eNotas.Sharp
dotnet restore eNotas.Sharp.sln
dotnet build eNotas.Sharp.sln -c Release
```

O build Release gera `.nupkg` em `eNotas.Sharp/bin/Release/` quando `GeneratePackageOnBuild` está ativo.

## Como configurar o ambiente

- SDK .NET compatível com `netstandard2.0` (SDK moderno do .NET Core/.NET 5+ funciona).
- Para `dotnet test`: SDK com target `net8.0` (projeto `eNotas.Sharp.Tests`).
- Visual Studio 2022 ou `dotnet` CLI.
- Para testar contra a API real: API Key e `empresaId` fornecidos pela eNotas (não versionar no Git).

Ver `ENVIRONMENT.md`.

## Como criar nova feature

Cenários típicos neste repo:

### Novo endpoint no client
1. Task Preflight (`.cursor/skills/task-preflight`).
2. Conferir path e payload no Postman correto: V2 (NF-e/NFC-e/empresas) ou V1 (NFS-e/apoio municipal) em `docs/`.
3. Criar/estender models em `Models/` com `[JsonProperty]` e `NullValueHandling.Ignore`.
4. Adicionar método async em `eNotasClient` na região `#region NFe`, `#region NFCe`, `#region NFSe` ou `#region Empresas`.
5. Atualizar lista de métodos no `README.md` e `CHANGELOG.md` se for release.
6. Avaliar bump de `Version` no `.csproj`.
7. `dotnet test eNotas.Sharp.sln -c Release`; validação manual contra homologação se possível.

### Novo campo em model existente
1. Confirmar nome JSON na API.
2. Adicionar propriedade opcional (nullable) — mudança aditiva.
3. Não remover/renomear propriedades públicas sem major version.
4. Documentar no PR/commit o motivo (ex.: novo campo tributário).

## Como corrigir bug

1. Reproduzir com payload mínimo (JSON) e resposta da API.
2. Isolar: model (serialização) vs `RestService` (HTTP) vs path do client.
3. Evitar “corrigir” engolindo mais exceções.
4. Verificar impacto em NF-e **e** NFC-e se o model for compartilhado.
5. Usar skill `bugfix-safe-workflow`.

## Como alterar código existente

- Preservar nomenclatura pública existente (`Iten`, `Adicoe`, `eNotasClient`, etc.).
- Manter `RestService` interno.
- Não hardcodar API Keys de produção.
- Separar mudança de modelo de mudança de transporte em commits distintos quando possível.

## Como validar alterações

```powershell
dotnet test eNotas.Sharp.sln -c Release
```

Há suite xUnit em `eNotas.Sharp.Tests` (serialização + smoke HTTP). Validação adicional / integração real: `TESTING.md`.

Checklist rápido:
- [ ] `dotnet test` Release OK
- [ ] Assinaturas públicas intencionais
- [ ] JsonProperty conferido
- [ ] README atualizado se método novo/removido
- [ ] Versão do pacote avaliada
- [ ] Sem secrets no diff

## Como revisar alterações antes de commit

Usar skill `.cursor/skills/git-commit` e rule `95-git-and-change-management`.

```powershell
git status
git diff
```

Não incluir alterações não relacionadas (ex.: governança + campos fiscais WIP no mesmo commit sem intenção).

## Padrão de commits

**Fato observado:** histórico em português, informal (“Adicionado…”, “Alterado…”, mensagens “;”).

**Recomendação (Conventional Commits em português ou inglês, consistente):**

```
feat(client): add consulta XML inutilizacao NFCe
fix(models): correct decimal type on tax field
docs: add architecture and agent workflow
chore(nuget): bump package version to 1.4.5
```

## Padrões de nomenclatura

| Elemento | Padrão observado |
|----------|------------------|
| Namespaces | `eNotas.Sharp.Clients`, `.Services`, `.Models` |
| Client | `eNotasClient` (e minúsculo — preservar) |
| Models | PascalCase; alguns nomes irregulares (`Iten`) |
| JSON | camelCase via `JsonProperty("...")` |
| Métodos API | Verbo + documento: `EmitirNfe`, `CancelaNfce`, `ConsultaInutilizacaoNfe` |

## Padrões de organização

- Um arquivo por conceito principal de model (exceto XMLs agregados).
- Regiões `#region NFe` / `#region NFCe` / `#region NFSe` / `#region Empresas` no client.
- `partial class` nos models — manter o padrão ao editar.

## Cuidados antes de abrir PR ou finalizar tarefa

1. Diff sem secrets.
2. `dotnet test` Release.
3. Impacto em pacote NuGet comunicado.
4. Docs/README alinhados.
5. Se operação fiscal sensível: marcar para revisão humana.

## Erros comuns

- Renomear `JsonProperty` achando que é só C#.
- Alterar só NF-e e esquecer NFC-e (ou o contrário) quando o model é compartilhado.
- Commitar API Key em exemplos.
- Subir versão do pacote sem mudança real (ou o contrário).
- Usar Postman V1 (NFS-e / `nfes`) como referência de paths NF-e/NFC-e V2 (`nf-e`/`nfc-e`), ou o inverso.
