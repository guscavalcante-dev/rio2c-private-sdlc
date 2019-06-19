namespace PlataformaRio2C.Infra.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class musicalCommission : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("dbo.Speaker", "EventId", "dbo.Event");
            //DropIndex("dbo.Speaker", new[] { "EventId" });
            //DropPrimaryKey("dbo.Speaker");
            CreateTable(
                "dbo.MusicalCommission",
                c => new
                    {
                        CollaboratorId = c.Int(nullable: false),
                        Id = c.Int(nullable: false, identity: true),
                        Uid = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.CollaboratorId, t.Id })
                .ForeignKey("dbo.Collaborator", t => t.CollaboratorId)
                .Index(t => t.CollaboratorId)
                .Index(t => t.Uid, unique: true);

            AddColumn("dbo.Collaborator", "MusicalCommissionId", c => c.Int());
            //AlterColumn("dbo.Speaker", "Id", c => c.Int(nullable: false, identity: true));
            //AddPrimaryKey("dbo.Speaker", new[] { "CollaboratorId", "Id" });
            //DropColumn("dbo.Speaker", "EventId");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.Speaker", "EventId", c => c.Int(nullable: false));
            //DropForeignKey("dbo.MusicalCommission", "CollaboratorId", "dbo.Collaborator");
            //DropIndex("dbo.MusicalCommission", new[] { "Uid" });
            DropIndex("dbo.MusicalCommission", new[] { "CollaboratorId" });
            DropPrimaryKey("dbo.Speaker");
            AlterColumn("dbo.Speaker", "Id", c => c.Int(nullable: false));
            DropColumn("dbo.Collaborator", "MusicalCommissionId");
            DropTable("dbo.MusicalCommission");
            //AddPrimaryKey("dbo.Speaker", new[] { "CollaboratorId", "Id", "EventId" });
            //CreateIndex("dbo.Speaker", "EventId");
            //AddForeignKey("dbo.Speaker", "EventId", "dbo.Event", "Id");
        }
    }
}
