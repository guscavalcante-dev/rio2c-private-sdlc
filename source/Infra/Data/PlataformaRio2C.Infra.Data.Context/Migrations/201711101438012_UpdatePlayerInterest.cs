namespace PlataformaRio2C.Infra.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePlayerInterest : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Activity",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50, unicode: false),
                        Uid = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Uid, unique: true);
            
            CreateTable(
                "dbo.PlayerInterest",
                c => new
                    {
                        PlayerId = c.Int(nullable: false),
                        InterestId = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.PlayerId, t.InterestId })
                .ForeignKey("dbo.Interest", t => t.InterestId)
                .ForeignKey("dbo.Player", t => t.PlayerId)
                .Index(t => t.PlayerId)
                .Index(t => t.InterestId);
            
            CreateTable(
                "dbo.PlayerActivity",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlayerId = c.Int(nullable: false),
                        ActivityId = c.Int(nullable: false),
                        Uid = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Activity", t => t.ActivityId)
                .ForeignKey("dbo.Player", t => t.PlayerId)
                .Index(t => t.PlayerId)
                .Index(t => t.ActivityId)
                .Index(t => t.Uid, unique: true);
            
            CreateTable(
                "dbo.PlayerTargetAudience",
                c => new
                    {
                        PlayerId = c.Int(nullable: false),
                        TargetAudienceId = c.Int(nullable: false),
                        Id = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.PlayerId, t.TargetAudienceId })
                .ForeignKey("dbo.Player", t => t.PlayerId)
                .ForeignKey("dbo.TargetAudience", t => t.TargetAudienceId)
                .Index(t => t.PlayerId)
                .Index(t => t.TargetAudienceId);
            
            CreateTable(
                "dbo.TargetAudience",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50, unicode: false),
                        Uid = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Uid, unique: true);
            
            CreateTable(
                "dbo.PlayerRestrictionsSpecifics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(maxLength: 8000, unicode: false),
                        LanguageId = c.Int(nullable: false),
                        PlayerId = c.Int(nullable: false),
                        Uid = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Language", t => t.LanguageId)
                .ForeignKey("dbo.Player", t => t.PlayerId)
                .Index(t => t.LanguageId)
                .Index(t => t.PlayerId)
                .Index(t => t.Uid, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlayerRestrictionsSpecifics", "PlayerId", "dbo.Player");
            DropForeignKey("dbo.PlayerRestrictionsSpecifics", "LanguageId", "dbo.Language");
            DropForeignKey("dbo.PlayerTargetAudience", "TargetAudienceId", "dbo.TargetAudience");
            DropForeignKey("dbo.PlayerTargetAudience", "PlayerId", "dbo.Player");
            DropForeignKey("dbo.PlayerActivity", "PlayerId", "dbo.Player");
            DropForeignKey("dbo.PlayerActivity", "ActivityId", "dbo.Activity");
            DropForeignKey("dbo.PlayerInterest", "PlayerId", "dbo.Player");
            DropForeignKey("dbo.PlayerInterest", "InterestId", "dbo.Interest");
            DropIndex("dbo.PlayerRestrictionsSpecifics", new[] { "Uid" });
            DropIndex("dbo.PlayerRestrictionsSpecifics", new[] { "PlayerId" });
            DropIndex("dbo.PlayerRestrictionsSpecifics", new[] { "LanguageId" });
            DropIndex("dbo.TargetAudience", new[] { "Uid" });
            DropIndex("dbo.PlayerTargetAudience", new[] { "TargetAudienceId" });
            DropIndex("dbo.PlayerTargetAudience", new[] { "PlayerId" });
            DropIndex("dbo.PlayerActivity", new[] { "Uid" });
            DropIndex("dbo.PlayerActivity", new[] { "ActivityId" });
            DropIndex("dbo.PlayerActivity", new[] { "PlayerId" });
            DropIndex("dbo.PlayerInterest", new[] { "InterestId" });
            DropIndex("dbo.PlayerInterest", new[] { "PlayerId" });
            DropIndex("dbo.Activity", new[] { "Uid" });
            DropTable("dbo.PlayerRestrictionsSpecifics");
            DropTable("dbo.TargetAudience");
            DropTable("dbo.PlayerTargetAudience");
            DropTable("dbo.PlayerActivity");
            DropTable("dbo.PlayerInterest");
            DropTable("dbo.Activity");
        }
    }
}
