namespace PlataformaRio2C.Infra.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateProducerRemoveRequiredCnpj : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Producer", "IX_Cnpj");
            AlterColumn("dbo.Producer", "CNPJ", c => c.String(maxLength: 50, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Producer", "CNPJ", c => c.String(nullable: false, maxLength: 50, unicode: false));
            CreateIndex("dbo.Producer", "CNPJ", unique: true, name: "IX_Cnpj");
        }
    }
}
