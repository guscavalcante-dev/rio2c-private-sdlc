# PlataformaRio2C

### Setup inicial da aplicação em dev
  ##### Buildar projeto para download de nugets (talvez seja necessário mais de um build)
  ```
  ctrl + shift + b
  ou
  Clicar bom o botão direito na solução PlataformaRio2C > clicar em Build Solution
  ```
  ##### Executar migrations para atualização do banco de dados (Package Manager Console)
```
Update-Database -Project PlataformaRio2C.Infra.Data.Context
```


### Criar uma nova migração

Antes a configuração de classe classe deve estar habilitados

```
Add-Migration [NomeDaMigracao] -Project PlataformaRio2C.Infra.Data.Context -StartupProject PlataformaRio2C.Web.Admin
```
  
### Configurar a classe SqlMigration na migration gerada

Nos arquivos de migração sempre teremos a herança da classe **SqlMigration** para que nossa migração aconteça a partir de um script e não a partir de um modelo gerado pela migration.
```csharp
using Pangea.Infra.CrossCutting.Tools.Helpers;

namespace PlataformaRio2C.Infra.Data.Context.Migrations
{
    public partial class initial_create_table : SqlMigration
    {
    }
}
```

### Possiblidade de configurar a migration para rodar em apenas um ou vários ambientes (environments)

Se você anotar suas classes de migration com [EnvironmentVariable([ambientes]) poderá definir em qual ambiente seu script será executados.
A classe **SqlMigration** irá verificar na variável de ambiente "ENVIRONMENT" no arquivo web.xml

```csharp
using Pangea.Infra.CrossCutting.Tools.Helpers;

 [EnvironmentVariable([EnumEnvironments.Staging, EnumEnvironments.Production])]
namespace PlataformaRio2C.Infra.Data.Context.Migrations
{
    public partial class initial_create_table : SqlMigration
    {
    }
}
```

O exemplo acima irá executar o script em Produção e Staging.

#### Atualizar os arquivos .sql na pasta script e muda a propriedade para copiar arquivos no assembly

Nesse caso, dever-se-á criar um arquivo: “[nome da migration gerada (completa com dados da datahora)].[up|down].sql” que deverão ficar dentro do diretório scripts. O diretório scripts no mesma origem do arquivo de migration gerado.

#### Os scripts deverão estar configurados para realizar cópia deles para dentro do build.**

Essa propriedade pode ser configurada pelo Visual Studio, clicando com o botão direito no arquivo > Propriedades.

```xml
 <Content Include="Migrations\Global\Scripts\20240201013137_initial_create_table_tenants.up.sql">
   <CopyToOutputDirectory>Always</CopyToOutputDirectory>
 </Content>
```
> Os scripts não poderão ter controle de transação (begin transaction e commit). Isso ficará a cargo da classe SqlMigration.


## Downgrade reverte migração informada

```shell

Update-Database -TargetMigration: "nome da miration" ou 0

```

Bibliotecas Inseridas no projeto :
A biblioteca Z.EntityFramework.Extensions é usada para fazer o Bulk Insert do projeto, para garantir o uso dela, ela deve ser atualizada ao menos uma vez por ano, com os comandos :
( sh ) nuget update Z.EntityFramework.Extensions
                  ou
( powershell) Update-Package Z.EntityFramework.Extensions



