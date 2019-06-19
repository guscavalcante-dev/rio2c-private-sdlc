namespace PlataformaRio2C.Infra.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMailCollaborator : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MailCollaborator",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Uid = c.Guid(nullable: false),
                        IdMailCollaborator = c.Int(nullable: false),
                        IdMail = c.Int(nullable: false),
                        IdCollaborator = c.Int(nullable: false),
                        SendDate = c.DateTime(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        Collaborator_Id = c.Int(),
                        Mail_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Collaborator", t => t.Collaborator_Id)
                .ForeignKey("dbo.Mail", t => t.Mail_Id)
                .Index(t => t.Uid, unique: true)
                .Index(t => t.Collaborator_Id)
                .Index(t => t.Mail_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MailCollaborator", "Mail_Id", "dbo.Mail");
            DropForeignKey("dbo.MailCollaborator", "Collaborator_Id", "dbo.Collaborator");
            DropIndex("dbo.MailCollaborator", new[] { "Mail_Id" });
            DropIndex("dbo.MailCollaborator", new[] { "Collaborator_Id" });
            DropIndex("dbo.MailCollaborator", new[] { "Uid" });
            DropTable("dbo.MailCollaborator");
        }
    }
}
