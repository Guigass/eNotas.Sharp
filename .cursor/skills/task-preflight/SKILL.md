# Task Preflight

## Objetivo

Evitar ações apressadas, reduzir riscos e garantir que o agente entenda objetivo, escopo, restrições e riscos antes de analisar ou modificar o projeto.

## Quando usar

Use esta skill antes de qualquer tarefa, incluindo:

- Análise de projeto
- Criação ou atualização de documentação, rules, skills ou agents
- Desenvolvimento de feature (endpoint, model, campo)
- Correção de bug
- Refatoração
- Alterações de integração com a API eNotas
- Preparação de publicação NuGet
- Preparação de commit

## Entradas esperadas

- Pedido original do usuário
- Objetivo entendido
- Escopo provável
- Arquivos ou áreas relevantes
- Tipo de ação: leitura, documentação, alteração estrutural ou alteração funcional
- Riscos conhecidos
- Restrições explícitas
- Necessidade ou não de validação humana

## Arquivos e áreas que devem ser analisados

Quando aplicável:

- `README.md`
- `docs/`
- `.cursor/`
- `eNotas.Sharp/eNotas.Sharp.csproj`
- `eNotas.Sharp/Clients/eNotasClient.cs`
- `eNotas.Sharp/Services/RestService.cs`
- Models relacionados à tarefa
- Coleções Postman em `docs/`

## Passo a passo

1. Reescreva o objetivo da tarefa em uma frase.
2. Identifique o escopo provável (client, models, service, docs, cursor).
3. Classifique o tipo de alteração.
4. Identifique riscos iniciais (fiscal, auth, breaking NuGet, secrets).
5. Identifique restrições (não alterar WIP alheio, não inventar endpoints).
6. Verifique governança já relacionada em `.cursor/` e `docs/`.
7. Defina a próxima ação segura.
8. Se risco elevado, marque “Precisa de validação humana”.

## Checklist de segurança

- [ ] Objetivo entendido
- [ ] Escopo provável identificado
- [ ] Áreas sensíveis mapeadas
- [ ] Sem alteração funcional não solicitada
- [ ] Sem comandos destrutivos
- [ ] Sem arquivos desnecessários
- [ ] Riscos registrados
- [ ] Incertezas marcadas

## Resultado esperado

```
# Task Preflight
## Objetivo entendido
## Escopo provável
## Riscos iniciais
## Restrições
## Próxima ação
```

## Como validar

Humano ou agente consegue dizer o que será feito, onde, quais riscos e qual o próximo passo seguro.

## Sinais de alerta

Exigir revisão humana quando envolver:

- Emissão, cancelamento, inutilização, CC-e
- Autenticação / base URL
- Breaking change de API pública NuGet
- Publicação NuGet
- Remoção de arquivos públicos
- Mudanças amplas ou ambíguas

## Agents recomendados

- project-architect
- documentation-maintainer
- qa-reviewer
- production-safety-officer (risco elevado)
