namespace PlataformaRio2C.Infra.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateProjectPlayerAddStatus : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProjectStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 150, unicode: false),
                        Name = c.String(maxLength: 50, unicode: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Code, unique: true);
            
            AddColumn("dbo.ProjectPlayer", "StatusId", c => c.Int());
            CreateIndex("dbo.ProjectPlayer", "StatusId");
            AddForeignKey("dbo.ProjectPlayer", "StatusId", "dbo.ProjectStatus", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectPlayer", "StatusId", "dbo.ProjectStatus");
            DropIndex("dbo.ProjectStatus", new[] { "Code" });
            DropIndex("dbo.ProjectPlayer", new[] { "StatusId" });
            DropColumn("dbo.ProjectPlayer", "StatusId");
            DropTable("dbo.ProjectStatus");
        }
    }
}
