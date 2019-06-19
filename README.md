# PlataformaRio2C

### Setup inicial da aplicação em dev
  ##### Buildar projeto para download de nugets (talvez seja necessário mais de um build)
  ```
  ctrl + shift + b
  ou
  Clicar bom o botão direito na solução PlataformaRio2C > clicar em Build Solution
  ```
  ##### Executar migrations para criação do banco de daodos (Package Manager Console)
  ```
  Selecionar o default project: PlataformaRio2C.Infra.CrossCutting.Identity
  > Update-Database
  
  Selecionar o default project: PlataformaRio2C.Infra.Data.Context
  > Update-Database
  
  Selecionar o default project: PlataformaRio2C.Infra.CrossCutting.SystemParameter
  > Update-Database
  ```