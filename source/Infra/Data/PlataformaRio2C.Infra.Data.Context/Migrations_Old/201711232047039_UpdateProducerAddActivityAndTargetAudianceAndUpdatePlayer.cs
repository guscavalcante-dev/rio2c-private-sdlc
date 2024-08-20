namespace PlataformaRio2C.Infra.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateProducerAddActivityAndTargetAudianceAndUpdatePlayer : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PlayerAdditionalInformation", "EventId", "dbo.Event");
            DropForeignKey("dbo.PlayerAdditionalInformation", "LanguageId", "dbo.Language");
            DropForeignKey("dbo.PlayerAdditionalInformation", "PlayerId", "dbo.Player");
            DropIndex("dbo.PlayerAdditionalInformation", new[] { "LanguageId" });
            DropIndex("dbo.PlayerAdditionalInformation", new[] { "PlayerId" });
            DropIndex("dbo.PlayerAdditionalInformation", new[] { "EventId" });
            DropIndex("dbo.PlayerAdditionalInformation", new[] { "Uid" });
            CreateTable(
                "dbo.ProducerActivity",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProducerId = c.Int(nullable: false),
                        ActivityId = c.Int(nullable: false),
                        Uid = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Activity", t => t.ActivityId)
                .ForeignKey("dbo.Producer", t => t.ProducerId)
                .Index(t => t.ProducerId)
                .Index(t => t.ActivityId)
                .Index(t => t.Uid, unique: true);
            
            CreateTable(
                "dbo.ProducerTargetAudience",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProducerId = c.Int(nullable: false),
                        TargetAudienceId = c.Int(nullable: false),
                        Uid = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Producer", t => t.ProducerId)
                .ForeignKey("dbo.TargetAudience", t => t.TargetAudienceId)
                .Index(t => t.ProducerId)
                .Index(t => t.TargetAudienceId)
                .Index(t => t.Uid, unique: true);
            
            DropTable("dbo.PlayerAdditionalInformation");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.ProducerTargetAudience", "TargetAudienceId", "dbo.TargetAudience");
            DropForeignKey("dbo.ProducerTargetAudience", "ProducerId", "dbo.Producer");
            DropForeignKey("dbo.ProducerActivity", "ProducerId", "dbo.Producer");
            DropForeignKey("dbo.ProducerActivity", "ActivityId", "dbo.Activity");
            DropIndex("dbo.ProducerTargetAudience", new[] { "Uid" });
            DropIndex("dbo.ProducerTargetAudience", new[] { "TargetAudienceId" });
            DropIndex("dbo.ProducerTargetAudience", new[] { "ProducerId" });
            DropIndex("dbo.ProducerActivity", new[] { "Uid" });
            DropIndex("dbo.ProducerActivity", new[] { "ActivityId" });
            DropIndex("dbo.ProducerActivity", new[] { "ProducerId" });
            DropTable("dbo.ProducerTargetAudience");
            DropTable("dbo.ProducerActivity");
            CreateIndex("dbo.PlayerAdditionalInformation", "Uid", unique: true);
            CreateIndex("dbo.PlayerAdditionalInformation", "EventId");
            CreateIndex("dbo.PlayerAdditionalInformation", "PlayerId");
            CreateIndex("dbo.PlayerAdditionalInformation", "LanguageId");
            AddForeignKey("dbo.PlayerAdditionalInformation", "PlayerId", "dbo.Player", "Id");
            AddForeignKey("dbo.PlayerAdditionalInformation", "LanguageId", "dbo.Language", "Id");
            AddForeignKey("dbo.PlayerAdditionalInformation", "EventId", "dbo.Event", "Id");
        }
    }
}
