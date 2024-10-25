## Link do Jira:
(colar o link do item do Jira)

## Essa PR tem depedência de alguma outra PR, dados ou qualquer outra coisa?
- `Que outras PRs são necessárias para o deploy dessa PR?`
- `Informe se há algum script de dados (seed) que precisa ser executado manualmente.`
- `Informe se há qualquer outra dependência para o deploy dessa PR (ex: DevOps).`

## CHECKLIST DO AUTOR
<!-- Marque as caixas com um "x" para garantir que as atividades foram cumpridas. Caso não tenha cumprido, coloque o motivo ao lado do item com sua explicação "(motivo: )".  -->
- [ ] Atualizei a branch da PR com a branch principal e resolvi todos os conflitos.
- [ ] Revisei TODOS os requisitos do item e estão sendo consideradas no código.
- [ ] Implementei testes automatizados para TODOS os cenários de teste do item.
- [ ] Executei os testes automatizados localmente e TODOS estão passando com sucesso.
- [ ] Não reduzi a cobertura (%) de testes automatizados.
- [ ] Testei manualmente e2e TODOS os cenários de teste do item.
- [ ] Revisei que a minha entrega está seguindo os padrões/guideline do projeto.
- [ ] Chequei que não há warnings ou erros no console durante a execução da aplicação.
- [ ] Não introduzi problemas de performance ou segurança na aplicação.
- [ ] Linkei as outras PRs necessárias para o deploy da minha PR na descrição dessa PR.
- [ ] Revisei o texto e as alterações da minha PR (arquivo a arquivo) antes de solicitar a revisão.
Frontend:
- [ ] Revisei o layout (espaçamentos, fontes, cores etc.) de TODAS as telas entregues e estão pixel perfect em relação ao Figma.
- [ ] Revisei a responsividade (responsive no navegador e tamanho de fonte no celular) de TODAS as telas entregues.
Backend:
- [ ] Documentei TODAS as alterações de API no Swagger ou similar, caso existam.
- [ ] Incluí os scripts de schema e seed necessários para cada ambiente, caso existam.

## CHECKLIST DO REVISOR
<!-- Marque as caixas com um "x" para garantir que as atividades foram cumpridas. Caso não tenha cumprido, coloque o motivo ao lado do item com sua explicação "(motivo: )".  -->
- [ ] Revisei o texto da PR e o checklist do Autor para garantir que tudo foi marcado ou tem motivo adequado.
- [ ] Li os comentários do revisor de PR de AI (ex: Coderabbit).
- [ ] Tenho conhecimento amplo do projeto para fazer a revisão da PR.
- [ ] Revisei o código e está claro e fácil de entender.
- [ ] Revisei o código e está seguindo os padrões/guideline do projeto.
- [ ] Revisei o código e não está introduzindo problemas de segurança na aplicação.
- [ ] Fiz uma call com o Autor para ele me explicar a PR.