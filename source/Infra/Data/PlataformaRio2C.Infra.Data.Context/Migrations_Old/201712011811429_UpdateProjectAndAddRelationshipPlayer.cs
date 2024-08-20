namespace PlataformaRio2C.Infra.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateProjectAndAddRelationshipPlayer : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProducerProject", "ProducerId", "dbo.Producer");
            DropForeignKey("dbo.ProducerProject", "ProjectId", "dbo.Project");
            DropIndex("dbo.ProducerProject", new[] { "ProducerId" });
            DropIndex("dbo.ProducerProject", new[] { "ProjectId" });
            CreateTable(
                "dbo.ProjectPlayer",
                c => new
                    {
                        PlayerId = c.Int(nullable: false),
                        ProjectId = c.Int(nullable: false),
                        Sent = c.Boolean(nullable: false),
                        SavedUserId = c.Int(),
                        SendingUserId = c.Int(),
                        DateSaved = c.DateTime(),
                        DateSending = c.DateTime(),
                        Id = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.PlayerId, t.ProjectId })
                .ForeignKey("dbo.Player", t => t.PlayerId)
                .ForeignKey("dbo.Project", t => t.ProjectId)
                .ForeignKey("dbo.AspNetUsers", t => t.SavedUserId)
                .ForeignKey("dbo.AspNetUsers", t => t.SendingUserId)
                .Index(t => t.PlayerId)
                .Index(t => t.ProjectId)
                .Index(t => t.SavedUserId)
                .Index(t => t.SendingUserId);
            
            AddColumn("dbo.Project", "ProducerId", c => c.Int(nullable: false));
            CreateIndex("dbo.Project", "ProducerId");
            AddForeignKey("dbo.Project", "ProducerId", "dbo.Producer", "Id");
            DropTable("dbo.ProducerProject");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProducerProject",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProducerId = c.Int(nullable: false),
                        ProjectId = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Project", "ProducerId", "dbo.Producer");
            DropForeignKey("dbo.ProjectPlayer", "SendingUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ProjectPlayer", "SavedUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ProjectPlayer", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.ProjectPlayer", "PlayerId", "dbo.Player");
            DropIndex("dbo.ProjectPlayer", new[] { "SendingUserId" });
            DropIndex("dbo.ProjectPlayer", new[] { "SavedUserId" });
            DropIndex("dbo.ProjectPlayer", new[] { "ProjectId" });
            DropIndex("dbo.ProjectPlayer", new[] { "PlayerId" });
            DropIndex("dbo.Project", new[] { "ProducerId" });
            DropColumn("dbo.Project", "ProducerId");
            DropTable("dbo.ProjectPlayer");
            CreateIndex("dbo.ProducerProject", "ProjectId");
            CreateIndex("dbo.ProducerProject", "ProducerId");
            AddForeignKey("dbo.ProducerProject", "ProjectId", "dbo.Project", "Id");
            AddForeignKey("dbo.ProducerProject", "ProducerId", "dbo.Producer", "Id");
        }
    }
}
