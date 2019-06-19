namespace PlataformaRio2C.Infra.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProject : DbMigration
    {
        public override void Up()
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Producer", t => t.ProducerId)
                .ForeignKey("dbo.Project", t => t.ProjectId)
                .Index(t => t.ProducerId)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.Project",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NumberOfEpisodes = c.Int(nullable: false),
                        EachEpisodePlayingTime = c.String(maxLength: 50, unicode: false),
                        ValuePerEpisode = c.String(maxLength: 50, unicode: false),
                        TotalValueOfProject = c.String(maxLength: 50, unicode: false),
                        ValueAlreadyRaised = c.String(maxLength: 50, unicode: false),
                        ValueStillNeeded = c.String(maxLength: 50, unicode: false),
                        Pitching = c.Boolean(nullable: false),
                        Uid = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Uid, unique: true);
            
            CreateTable(
                "dbo.ProjectAdditionalInformation",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(maxLength: 1500, unicode: false),
                        LanguageId = c.Int(nullable: false),
                        ProjectId = c.Int(nullable: false),
                        Uid = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Language", t => t.LanguageId)
                .ForeignKey("dbo.Project", t => t.ProjectId)
                .Index(t => t.LanguageId)
                .Index(t => t.ProjectId)
                .Index(t => t.Uid, unique: true);
            
            CreateTable(
                "dbo.ProjectInterest",
                c => new
                    {
                        ProjectId = c.Int(nullable: false),
                        InterestId = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProjectId, t.InterestId })
                .ForeignKey("dbo.Interest", t => t.InterestId)
                .ForeignKey("dbo.Project", t => t.ProjectId)
                .Index(t => t.ProjectId)
                .Index(t => t.InterestId);
            
            CreateTable(
                "dbo.ProjectLinkImage",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(maxLength: 3000, unicode: false),
                        ProjectId = c.Int(nullable: false),
                        Uid = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Project", t => t.ProjectId)
                .Index(t => t.ProjectId)
                .Index(t => t.Uid, unique: true);
            
            CreateTable(
                "dbo.ProjectLinkTeaser",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(maxLength: 3000, unicode: false),
                        ProjectId = c.Int(nullable: false),
                        Uid = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Project", t => t.ProjectId)
                .Index(t => t.ProjectId)
                .Index(t => t.Uid, unique: true);
            
            CreateTable(
                "dbo.ProjectLogLine",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(maxLength: 256, unicode: false),
                        LanguageId = c.Int(nullable: false),
                        ProjectId = c.Int(nullable: false),
                        Uid = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Language", t => t.LanguageId)
                .ForeignKey("dbo.Project", t => t.ProjectId)
                .Index(t => t.LanguageId)
                .Index(t => t.ProjectId)
                .Index(t => t.Uid, unique: true);
            
            CreateTable(
                "dbo.ProjectProductionPlan",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(maxLength: 3000, unicode: false),
                        LanguageId = c.Int(nullable: false),
                        ProjectId = c.Int(nullable: false),
                        Uid = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Language", t => t.LanguageId)
                .ForeignKey("dbo.Project", t => t.ProjectId)
                .Index(t => t.LanguageId)
                .Index(t => t.ProjectId)
                .Index(t => t.Uid, unique: true);
            
            CreateTable(
                "dbo.ProjectSummary",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(maxLength: 3000, unicode: false),
                        LanguageId = c.Int(nullable: false),
                        ProjectId = c.Int(nullable: false),
                        Uid = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Language", t => t.LanguageId)
                .ForeignKey("dbo.Project", t => t.ProjectId)
                .Index(t => t.LanguageId)
                .Index(t => t.ProjectId)
                .Index(t => t.Uid, unique: true);
            
            CreateTable(
                "dbo.ProjectTitle",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(maxLength: 256, unicode: false),
                        LanguageId = c.Int(nullable: false),
                        ProjectId = c.Int(nullable: false),
                        Uid = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Language", t => t.LanguageId)
                .ForeignKey("dbo.Project", t => t.ProjectId)
                .Index(t => t.LanguageId)
                .Index(t => t.ProjectId)
                .Index(t => t.Uid, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProducerProject", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.ProjectTitle", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.ProjectTitle", "LanguageId", "dbo.Language");
            DropForeignKey("dbo.ProjectSummary", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.ProjectSummary", "LanguageId", "dbo.Language");
            DropForeignKey("dbo.ProjectProductionPlan", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.ProjectProductionPlan", "LanguageId", "dbo.Language");
            DropForeignKey("dbo.ProjectLogLine", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.ProjectLogLine", "LanguageId", "dbo.Language");
            DropForeignKey("dbo.ProjectLinkTeaser", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.ProjectLinkImage", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.ProjectInterest", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.ProjectInterest", "InterestId", "dbo.Interest");
            DropForeignKey("dbo.ProjectAdditionalInformation", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.ProjectAdditionalInformation", "LanguageId", "dbo.Language");
            DropForeignKey("dbo.ProducerProject", "ProducerId", "dbo.Producer");
            DropIndex("dbo.ProjectTitle", new[] { "Uid" });
            DropIndex("dbo.ProjectTitle", new[] { "ProjectId" });
            DropIndex("dbo.ProjectTitle", new[] { "LanguageId" });
            DropIndex("dbo.ProjectSummary", new[] { "Uid" });
            DropIndex("dbo.ProjectSummary", new[] { "ProjectId" });
            DropIndex("dbo.ProjectSummary", new[] { "LanguageId" });
            DropIndex("dbo.ProjectProductionPlan", new[] { "Uid" });
            DropIndex("dbo.ProjectProductionPlan", new[] { "ProjectId" });
            DropIndex("dbo.ProjectProductionPlan", new[] { "LanguageId" });
            DropIndex("dbo.ProjectLogLine", new[] { "Uid" });
            DropIndex("dbo.ProjectLogLine", new[] { "ProjectId" });
            DropIndex("dbo.ProjectLogLine", new[] { "LanguageId" });
            DropIndex("dbo.ProjectLinkTeaser", new[] { "Uid" });
            DropIndex("dbo.ProjectLinkTeaser", new[] { "ProjectId" });
            DropIndex("dbo.ProjectLinkImage", new[] { "Uid" });
            DropIndex("dbo.ProjectLinkImage", new[] { "ProjectId" });
            DropIndex("dbo.ProjectInterest", new[] { "InterestId" });
            DropIndex("dbo.ProjectInterest", new[] { "ProjectId" });
            DropIndex("dbo.ProjectAdditionalInformation", new[] { "Uid" });
            DropIndex("dbo.ProjectAdditionalInformation", new[] { "ProjectId" });
            DropIndex("dbo.ProjectAdditionalInformation", new[] { "LanguageId" });
            DropIndex("dbo.Project", new[] { "Uid" });
            DropIndex("dbo.ProducerProject", new[] { "ProjectId" });
            DropIndex("dbo.ProducerProject", new[] { "ProducerId" });
            DropTable("dbo.ProjectTitle");
            DropTable("dbo.ProjectSummary");
            DropTable("dbo.ProjectProductionPlan");
            DropTable("dbo.ProjectLogLine");
            DropTable("dbo.ProjectLinkTeaser");
            DropTable("dbo.ProjectLinkImage");
            DropTable("dbo.ProjectInterest");
            DropTable("dbo.ProjectAdditionalInformation");
            DropTable("dbo.Project");
            DropTable("dbo.ProducerProject");
        }
    }
}
