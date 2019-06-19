namespace PlataformaRio2C.Infra.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Speaker1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Collaborator", "SpeakerId", "dbo.Speaker");
            DropIndex("dbo.Collaborator", new[] { "SpeakerId" });
            DropPrimaryKey("dbo.Speaker");
            AlterColumn("dbo.Speaker", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Speaker", new[] { "CollaboratorId", "Id", "EventId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Speaker");
            AlterColumn("dbo.Speaker", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Speaker", "Id");
            CreateIndex("dbo.Collaborator", "SpeakerId");
            AddForeignKey("dbo.Collaborator", "SpeakerId", "dbo.Speaker", "Id");
        }
    }
}
