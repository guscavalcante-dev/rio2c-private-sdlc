namespace PlataformaRio2C.Infra.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePlayerProjectAddIdPrimaryKey : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ProjectPlayer", new[] { "PlayerId" });
            DropIndex("dbo.ProjectPlayer", new[] { "ProjectId" });
            DropPrimaryKey("dbo.ProjectPlayer");
            AddColumn("dbo.ProjectPlayer", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.ProjectPlayer", "Id");
            CreateIndex("dbo.ProjectPlayer", new[] { "PlayerId", "ProjectId" }, unique: true, name: "IX_PlayerProject");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ProjectPlayer", "IX_PlayerProject");
            DropPrimaryKey("dbo.ProjectPlayer");
            DropColumn("dbo.ProjectPlayer", "Id");
            AddPrimaryKey("dbo.ProjectPlayer", new[] { "PlayerId", "ProjectId" });
            CreateIndex("dbo.ProjectPlayer", "ProjectId");
            CreateIndex("dbo.ProjectPlayer", "PlayerId");
        }
    }
}
