# Bugfix Safe Workflow

## Objetivo

Corrigir bugs sem quebrar contratos públicos nem fluxos fiscais.

## Quando usar

- Serialização incorreta
- Path/HTTP errado
- Parse XML/JSON falhando
- Comportamento divergente NFe vs NFCe
- Problemas relatados por consumidores do NuGet

## Entradas esperadas

- Sintoma observado
- Evidências (`ApiResponse`, body, versão do pacote)
- Ambiente (homolog/prod) — sem exigir secrets no chat

## Arquivos e áreas que devem ser analisados

- Método do client envolvido
- Models do payload/resposta
- `RestService` (se HTTP/parse)
- `CustomDateTimeConverter` (se data)
- Postman V2 para contrato esperado

## Passo a passo

1. Task Preflight.
2. Reproduzir/classificar a camada (auth, path, payload, parse, fiscal API).
3. Coletar evidências (ver `docs/TROUBLESHOOTING.md`) sem vazar secrets.
4. Isolar a menor correção possível.
5. impact-analysis rápido.
6. Aplicar correção.
7. Build Release.
8. Listar regressões possíveis (par NFe/NFCe, outros GETs).
9. Preparar commit via skill git-commit.

## Checklist de segurança

- [ ] Causa raiz identificada ou hipótese explícita
- [ ] Correção mínima
- [ ] Sem mascarar erro engolindo exceção a mais
- [ ] Build OK
- [ ] Risco fiscal comunicado

## Resultado esperado

Fix + explicação da causa + validações + riscos residuais.

## Como validar

Build OK; hipótese de correção testável; diff limitado ao problema.

## Sinais de alerta

- “Corrigir” mudando auth global
- Alterar muitos models sem evidência
- Bug só em produção sem evidência redigida

## Agents recomendados

- library-specialist
- integration-specialist
- qa-reviewer
- production-safety-officer (se produção/fiscal)
