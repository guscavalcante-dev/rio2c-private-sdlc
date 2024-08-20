namespace PlataformaRio2C.Infra.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddInterests : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PlayerAdditionalInformation",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(maxLength: 8000, unicode: false),
                        LanguageId = c.Int(nullable: false),
                        PlayerId = c.Int(nullable: false),
                        PlayerUid = c.Guid(nullable: false),
                        EventId = c.Int(nullable: false),
                        EventUid = c.Guid(nullable: false),
                        Uid = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Event", t => t.EventId)
                .ForeignKey("dbo.Language", t => t.LanguageId)
                .ForeignKey("dbo.Player", t => t.PlayerId)
                .Index(t => t.LanguageId)
                .Index(t => t.PlayerId)
                .Index(t => t.EventId)
                .Index(t => t.Uid, unique: true);
            
            CreateTable(
                "dbo.PlayerInterestEvent",
                c => new
                    {
                        PlayerId = c.Int(nullable: false),
                        InterestId = c.Int(nullable: false),
                        EventId = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.PlayerId, t.InterestId, t.EventId })
                .ForeignKey("dbo.Event", t => t.EventId)
                .ForeignKey("dbo.Interest", t => t.InterestId)
                .ForeignKey("dbo.Player", t => t.PlayerId)
                .Index(t => t.PlayerId)
                .Index(t => t.InterestId)
                .Index(t => t.EventId);
            
            CreateTable(
                "dbo.Interest",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InterestGroupId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 150, unicode: false),
                        Uid = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.InterestGroup", t => t.InterestGroupId)
                .Index(t => t.InterestGroupId)
                .Index(t => t.Name, unique: true)
                .Index(t => t.Uid, unique: true);
            
            CreateTable(
                "dbo.InterestGroup",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150, unicode: false),
                        Type = c.String(nullable: false, maxLength: 100, unicode: false),
                        Uid = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true)
                .Index(t => t.Uid, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlayerInterestEvent", "PlayerId", "dbo.Player");
            DropForeignKey("dbo.PlayerInterestEvent", "InterestId", "dbo.Interest");
            DropForeignKey("dbo.Interest", "InterestGroupId", "dbo.InterestGroup");
            DropForeignKey("dbo.PlayerInterestEvent", "EventId", "dbo.Event");
            DropForeignKey("dbo.PlayerAdditionalInformation", "PlayerId", "dbo.Player");
            DropForeignKey("dbo.PlayerAdditionalInformation", "LanguageId", "dbo.Language");
            DropForeignKey("dbo.PlayerAdditionalInformation", "EventId", "dbo.Event");
            DropIndex("dbo.InterestGroup", new[] { "Uid" });
            DropIndex("dbo.InterestGroup", new[] { "Name" });
            DropIndex("dbo.Interest", new[] { "Uid" });
            DropIndex("dbo.Interest", new[] { "Name" });
            DropIndex("dbo.Interest", new[] { "InterestGroupId" });
            DropIndex("dbo.PlayerInterestEvent", new[] { "EventId" });
            DropIndex("dbo.PlayerInterestEvent", new[] { "InterestId" });
            DropIndex("dbo.PlayerInterestEvent", new[] { "PlayerId" });
            DropIndex("dbo.PlayerAdditionalInformation", new[] { "Uid" });
            DropIndex("dbo.PlayerAdditionalInformation", new[] { "EventId" });
            DropIndex("dbo.PlayerAdditionalInformation", new[] { "PlayerId" });
            DropIndex("dbo.PlayerAdditionalInformation", new[] { "LanguageId" });
            DropTable("dbo.InterestGroup");
            DropTable("dbo.Interest");
            DropTable("dbo.PlayerInterestEvent");
            DropTable("dbo.PlayerAdditionalInformation");
        }
    }
}
