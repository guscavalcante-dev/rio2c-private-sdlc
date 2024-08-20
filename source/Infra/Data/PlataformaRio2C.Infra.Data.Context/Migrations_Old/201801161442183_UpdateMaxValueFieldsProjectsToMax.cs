namespace PlataformaRio2C.Infra.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMaxValueFieldsProjectsToMax : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProjectAdditionalInformation", "Value", c => c.String());
            AlterColumn("dbo.ProjectProductionPlan", "Value", c => c.String());
            AlterColumn("dbo.ProjectSummary", "Value", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProjectSummary", "Value", c => c.String(maxLength: 8000, unicode: false));
            AlterColumn("dbo.ProjectProductionPlan", "Value", c => c.String(maxLength: 8000, unicode: false));
            AlterColumn("dbo.ProjectAdditionalInformation", "Value", c => c.String(maxLength: 8000, unicode: false));
        }
    }
}
