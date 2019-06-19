namespace PlataformaRio2C.Infra.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMaxLenghtProjectSummary : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProjectSummary", "Value", c => c.String(maxLength: 4000, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProjectSummary", "Value", c => c.String(maxLength: 3000, unicode: false));
        }
    }
}
