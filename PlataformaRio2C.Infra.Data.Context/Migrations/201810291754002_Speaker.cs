namespace PlataformaRio2C.Infra.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Speaker : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Speaker",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EventId = c.Int(nullable: false),
                        CollaboratorId = c.Int(nullable: false),
                        Uid = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Collaborator", t => t.CollaboratorId)
                .ForeignKey("dbo.Event", t => t.EventId)
                .Index(t => t.EventId)
                .Index(t => t.CollaboratorId)
                .Index(t => t.Uid, unique: true);
            
            AddColumn("dbo.Collaborator", "SpeakerId", c => c.Int());
            CreateIndex("dbo.Collaborator", "SpeakerId");
            AddForeignKey("dbo.Collaborator", "SpeakerId", "dbo.Speaker", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Collaborator", "SpeakerId", "dbo.Speaker");
            DropForeignKey("dbo.Speaker", "EventId", "dbo.Event");
            DropForeignKey("dbo.Speaker", "CollaboratorId", "dbo.Collaborator");
            DropIndex("dbo.Speaker", new[] { "Uid" });
            DropIndex("dbo.Speaker", new[] { "CollaboratorId" });
            DropIndex("dbo.Speaker", new[] { "EventId" });
            DropIndex("dbo.Collaborator", new[] { "SpeakerId" });
            DropColumn("dbo.Collaborator", "SpeakerId");
            DropTable("dbo.Speaker");
        }
    }
}
