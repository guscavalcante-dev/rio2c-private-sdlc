namespace PlataformaRio2C.Infra.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMessage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Message",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        IsRead = c.Boolean(nullable: false),
                        SenderId = c.Int(nullable: false),
                        RecipientId = c.Int(nullable: false),
                        Uid = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.RecipientId)
                .ForeignKey("dbo.AspNetUsers", t => t.SenderId)
                .Index(t => t.SenderId)
                .Index(t => t.RecipientId)
                .Index(t => t.Uid, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Message", "SenderId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Message", "RecipientId", "dbo.AspNetUsers");
            DropIndex("dbo.Message", new[] { "Uid" });
            DropIndex("dbo.Message", new[] { "RecipientId" });
            DropIndex("dbo.Message", new[] { "SenderId" });
            DropTable("dbo.Message");
        }
    }
}
