namespace PlataformaRio2C.Infra.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRoom : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Room",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100, unicode: false),
                        Uid = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true)
                .Index(t => t.Uid, unique: true);
            
            AddColumn("dbo.Conference", "RoomId", c => c.Int());
            CreateIndex("dbo.Conference", "RoomId");
            AddForeignKey("dbo.Conference", "RoomId", "dbo.Room", "Id");
            DropColumn("dbo.Conference", "Local");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Conference", "Local", c => c.String(maxLength: 1000, unicode: false));
            DropForeignKey("dbo.Conference", "RoomId", "dbo.Room");
            DropIndex("dbo.Conference", new[] { "RoomId" });
            DropIndex("dbo.Room", new[] { "Uid" });
            DropIndex("dbo.Room", new[] { "Name" });
            DropColumn("dbo.Conference", "RoomId");
            DropTable("dbo.Room");
        }
    }
}
