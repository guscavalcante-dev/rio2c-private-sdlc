namespace PlataformaRio2C.Infra.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRelationshipCollaboratorPlayer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CollaboratorPlayer",
                c => new
                    {
                        CollaboratorId = c.Int(nullable: false),
                        PlayerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CollaboratorId, t.PlayerId })
                .ForeignKey("dbo.Collaborator", t => t.CollaboratorId)
                .ForeignKey("dbo.Player", t => t.PlayerId)
                .Index(t => t.CollaboratorId)
                .Index(t => t.PlayerId);          
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CollaboratorPlayer", "PlayerId", "dbo.Player");
            DropForeignKey("dbo.CollaboratorPlayer", "CollaboratorId", "dbo.Collaborator");
            DropIndex("dbo.CollaboratorPlayer", new[] { "PlayerId" });
            DropIndex("dbo.CollaboratorPlayer", new[] { "CollaboratorId" });
            DropTable("dbo.CollaboratorPlayer");          
        }
    }
}
