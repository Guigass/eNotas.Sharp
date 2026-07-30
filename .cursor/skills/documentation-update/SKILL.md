# Documentation Update

## Objetivo

Manter `docs/`, README e governança Cursor alinhados ao código real.

## Quando usar

- Novo método ou comportamento público
- Mudança de arquitetura/fronteiras
- Nova rule/skill/agent
- Descoberta de lacuna ou risco
- Após feature ou bugfix relevante

## Entradas esperadas

- O que mudou no código (ou confirmação de que só docs)
- Arquivos de doc candidatos

## Arquivos e áreas que devem ser analisados

- `README.md`
- `docs/*.md`
- `.cursor/rules|skills|agents`
- Código fonte da mudança para não documentar inventado

## Passo a passo

1. Task Preflight.
2. Ler docs existentes relacionados — não sobrescrever util sem avaliar.
3. Atualizar apenas seções impactadas.
4. Separar fato / inferência / não identificado / validação humana.
5. Atualizar Agent Router se agents mudarem.
6. Não criar docs para stacks inexistentes.
7. Revisar links internos.

## Checklist de segurança

- [ ] Texto fiel ao código
- [ ] Sem secrets/exemplos com key real
- [ ] Sem duplicar arquivos com mesmo propósito
- [ ] Estrutura permanece enxuta

## Resultado esperado

Docs atualizados, proporção mantida, divergências antigas corrigidas ou marcadas.

## Como validar

Outro agente consegue operar só com docs + código citado.

## Sinais de alerta

- Documentar endpoint não implementado como se existisse
- Apagar Postman ou docs úteis sem motivo
- Gerar pasta enorme de docs genéricos

## Agents recomendados

- documentation-maintainer
- project-architect
