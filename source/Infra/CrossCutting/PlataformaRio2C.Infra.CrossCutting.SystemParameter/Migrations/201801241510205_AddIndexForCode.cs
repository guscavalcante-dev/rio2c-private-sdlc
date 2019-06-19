namespace PlataformaRio2C.Infra.CrossCutting.SystemParameter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIndexForCode : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.SystemParameter", "Code");
        }
        
        public override void Down()
        {
            DropIndex("dbo.SystemParameter", new[] { "Code" });
        }
    }
}
