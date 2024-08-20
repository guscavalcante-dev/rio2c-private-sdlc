namespace PlataformaRio2C.Infra.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropPlayerInterestEvent : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PlayerInterestEvent", "EventId", "dbo.Event");
            DropForeignKey("dbo.PlayerInterestEvent", "InterestId", "dbo.Interest");
            DropForeignKey("dbo.PlayerInterestEvent", "PlayerId", "dbo.Player");
            DropIndex("dbo.PlayerInterestEvent", new[] { "PlayerId" });
            DropIndex("dbo.PlayerInterestEvent", new[] { "InterestId" });
            DropIndex("dbo.PlayerInterestEvent", new[] { "EventId" });
            DropTable("dbo.PlayerInterestEvent");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PlayerInterestEvent",
                c => new
                    {
                        PlayerId = c.Int(nullable: false),
                        InterestId = c.Int(nullable: false),
                        EventId = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.PlayerId, t.InterestId, t.EventId });
            
            CreateIndex("dbo.PlayerInterestEvent", "EventId");
            CreateIndex("dbo.PlayerInterestEvent", "InterestId");
            CreateIndex("dbo.PlayerInterestEvent", "PlayerId");
            AddForeignKey("dbo.PlayerInterestEvent", "PlayerId", "dbo.Player", "Id");
            AddForeignKey("dbo.PlayerInterestEvent", "InterestId", "dbo.Interest", "Id");
            AddForeignKey("dbo.PlayerInterestEvent", "EventId", "dbo.Event", "Id");
        }
    }
}
