namespace PlataformaRio2C.Infra.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddConference : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Conference",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(),
                        Local = c.String(maxLength: 1000, unicode: false),
                        Info = c.String(maxLength: 3000, unicode: false),
                        ImageId = c.Int(),
                        Uid = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ImageFile", t => t.ImageId)
                .Index(t => t.ImageId)
                .Index(t => t.Uid, unique: true);
            
            CreateTable(
                "dbo.ConferenceLecturer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ConferenceId = c.Int(nullable: false),
                        CollaboratorId = c.Int(),
                        IsPreRegistered = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 1000, unicode: false),
                        Uid = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Collaborator", t => t.CollaboratorId)
                .ForeignKey("dbo.Conference", t => t.ConferenceId)
                .Index(t => t.ConferenceId)
                .Index(t => t.CollaboratorId)
                .Index(t => t.Uid, unique: true);
            
            CreateTable(
                "dbo.ConferenceSynopsis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(maxLength: 8000, unicode: false),
                        LanguageId = c.Int(nullable: false),
                        ConferenceId = c.Int(nullable: false),
                        Uid = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Conference", t => t.ConferenceId)
                .ForeignKey("dbo.Language", t => t.LanguageId)
                .Index(t => t.LanguageId)
                .Index(t => t.ConferenceId)
                .Index(t => t.Uid, unique: true);
            
            CreateTable(
                "dbo.ConferenceTitle",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(maxLength: 8000, unicode: false),
                        LanguageId = c.Int(nullable: false),
                        ConferenceId = c.Int(nullable: false),
                        Uid = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Conference", t => t.ConferenceId)
                .ForeignKey("dbo.Language", t => t.LanguageId)
                .Index(t => t.LanguageId)
                .Index(t => t.ConferenceId)
                .Index(t => t.Uid, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ConferenceTitle", "LanguageId", "dbo.Language");
            DropForeignKey("dbo.ConferenceTitle", "ConferenceId", "dbo.Conference");
            DropForeignKey("dbo.ConferenceSynopsis", "LanguageId", "dbo.Language");
            DropForeignKey("dbo.ConferenceSynopsis", "ConferenceId", "dbo.Conference");
            DropForeignKey("dbo.ConferenceLecturer", "ConferenceId", "dbo.Conference");
            DropForeignKey("dbo.ConferenceLecturer", "CollaboratorId", "dbo.Collaborator");
            DropForeignKey("dbo.Conference", "ImageId", "dbo.ImageFile");
            DropIndex("dbo.ConferenceTitle", new[] { "Uid" });
            DropIndex("dbo.ConferenceTitle", new[] { "ConferenceId" });
            DropIndex("dbo.ConferenceTitle", new[] { "LanguageId" });
            DropIndex("dbo.ConferenceSynopsis", new[] { "Uid" });
            DropIndex("dbo.ConferenceSynopsis", new[] { "ConferenceId" });
            DropIndex("dbo.ConferenceSynopsis", new[] { "LanguageId" });
            DropIndex("dbo.ConferenceLecturer", new[] { "Uid" });
            DropIndex("dbo.ConferenceLecturer", new[] { "CollaboratorId" });
            DropIndex("dbo.ConferenceLecturer", new[] { "ConferenceId" });
            DropIndex("dbo.Conference", new[] { "Uid" });
            DropIndex("dbo.Conference", new[] { "ImageId" });
            DropTable("dbo.ConferenceTitle");
            DropTable("dbo.ConferenceSynopsis");
            DropTable("dbo.ConferenceLecturer");
            DropTable("dbo.Conference");
        }
    }
}
