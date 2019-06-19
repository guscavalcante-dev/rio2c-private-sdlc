namespace PlataformaRio2C.Infra.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMail : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Mail",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        Subject = c.String(maxLength: 50, unicode: false),
                        Uid = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Uid, unique: true);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Mail", new[] { "Uid" });
            DropTable("dbo.Mail");
        }
    }
}
