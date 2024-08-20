namespace PlataformaRio2C.Infra.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMaxValueFieldsProjects : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProjectAdditionalInformation", "Value", c => c.String(maxLength: 8000, unicode: false));
            AlterColumn("dbo.ProjectLogLine", "Value", c => c.String(maxLength: 8000, unicode: false));
            AlterColumn("dbo.ProjectProductionPlan", "Value", c => c.String(maxLength: 8000, unicode: false));
            AlterColumn("dbo.ProjectSummary", "Value", c => c.String(maxLength: 8000, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProjectSummary", "Value", c => c.String(maxLength: 6000, unicode: false));
            AlterColumn("dbo.ProjectProductionPlan", "Value", c => c.String(maxLength: 3000, unicode: false));
            AlterColumn("dbo.ProjectLogLine", "Value", c => c.String(maxLength: 256, unicode: false));
            AlterColumn("dbo.ProjectAdditionalInformation", "Value", c => c.String(maxLength: 1500, unicode: false));
        }
    }
}
