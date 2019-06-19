namespace PlataformaRio2C.Infra.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLogistics : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Logistics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ArrivalTime = c.DateTime(nullable: false),
                        DepartureTime = c.DateTime(nullable: false),
                        CollaboratorId = c.Int(nullable: false),
                        EventId = c.Int(nullable: false),
                        Uid = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Collaborator", t => t.CollaboratorId)
                .ForeignKey("dbo.Event", t => t.EventId)
                .Index(t => new { t.CollaboratorId, t.EventId }, unique: true, name: "IX_Collaborator")
                .Index(t => t.Uid, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Logistics", "EventId", "dbo.Event");
            DropForeignKey("dbo.Logistics", "CollaboratorId", "dbo.Collaborator");
            DropIndex("dbo.Logistics", new[] { "Uid" });
            DropIndex("dbo.Logistics", "IX_Collaborator");
            DropTable("dbo.Logistics");
        }
    }
}
