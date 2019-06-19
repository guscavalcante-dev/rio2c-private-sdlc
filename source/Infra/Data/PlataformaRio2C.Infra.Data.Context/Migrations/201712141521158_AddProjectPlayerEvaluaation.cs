namespace PlataformaRio2C.Infra.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProjectPlayerEvaluaation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProjectPlayerEvaluation",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        ProjectPlayerId = c.Int(nullable: false),
                        StatusId = c.Int(nullable: false),
                        EvaluationUserId = c.Int(nullable: false),
                        Reason = c.String(maxLength: 1500, unicode: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.EvaluationUserId)
                .ForeignKey("dbo.ProjectStatus", t => t.StatusId)
                .ForeignKey("dbo.ProjectPlayer", t => t.Id)
                .Index(t => t.Id)
                .Index(t => new { t.ProjectPlayerId, t.EvaluationUserId }, unique: true, name: "IX_ProjectPlayerEvaluationdUser")
                .Index(t => t.StatusId);
            
            AddColumn("dbo.ProjectPlayer", "EvaluationId", c => c.Int());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectPlayerEvaluation", "Id", "dbo.ProjectPlayer");
            DropForeignKey("dbo.ProjectPlayerEvaluation", "StatusId", "dbo.ProjectStatus");
            DropForeignKey("dbo.ProjectPlayerEvaluation", "EvaluationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.ProjectPlayerEvaluation", new[] { "StatusId" });
            DropIndex("dbo.ProjectPlayerEvaluation", "IX_ProjectPlayerEvaluationdUser");
            DropIndex("dbo.ProjectPlayerEvaluation", new[] { "Id" });
            DropColumn("dbo.ProjectPlayer", "EvaluationId");
            DropTable("dbo.ProjectPlayerEvaluation");
        }
    }
}
