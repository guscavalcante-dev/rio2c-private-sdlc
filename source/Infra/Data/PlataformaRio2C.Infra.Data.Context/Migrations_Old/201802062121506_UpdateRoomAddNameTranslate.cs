namespace PlataformaRio2C.Infra.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRoomAddNameTranslate : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Room", new[] { "Name" });
            CreateTable(
                "dbo.RoomName",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(maxLength: 256, unicode: false),
                        LanguageId = c.Int(nullable: false),
                        RoomId = c.Int(nullable: false),
                        Uid = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Language", t => t.LanguageId)
                .ForeignKey("dbo.Room", t => t.RoomId)
                .Index(t => t.LanguageId)
                .Index(t => t.RoomId)
                .Index(t => t.Uid, unique: true);
            
            DropColumn("dbo.Room", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Room", "Name", c => c.String(nullable: false, maxLength: 100, unicode: false));
            DropForeignKey("dbo.RoomName", "RoomId", "dbo.Room");
            DropForeignKey("dbo.RoomName", "LanguageId", "dbo.Language");
            DropIndex("dbo.RoomName", new[] { "Uid" });
            DropIndex("dbo.RoomName", new[] { "RoomId" });
            DropIndex("dbo.RoomName", new[] { "LanguageId" });
            DropTable("dbo.RoomName");
            CreateIndex("dbo.Room", "Name", unique: true);
        }
    }
}
