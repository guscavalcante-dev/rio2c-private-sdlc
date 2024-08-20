namespace PlataformaRio2C.Infra.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePlayerAddCompanyName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Player", "CompanyName", c => c.String(maxLength: 100, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Player", "CompanyName");
        }
    }
}
