namespace PlataformaRio2C.Infra.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePlayerProjectRemoveId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProjectPlayer", "StatusId", "dbo.ProjectStatus");
            DropIndex("dbo.ProjectPlayer", new[] { "StatusId" });
            DropColumn("dbo.ProjectPlayer", "StatusId");
            DropColumn("dbo.ProjectPlayer", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProjectPlayer", "Id", c => c.Int(nullable: false));
            AddColumn("dbo.ProjectPlayer", "StatusId", c => c.Int());
            CreateIndex("dbo.ProjectPlayer", "StatusId");
            AddForeignKey("dbo.ProjectPlayer", "StatusId", "dbo.ProjectStatus", "Id");
        }
    }
}
