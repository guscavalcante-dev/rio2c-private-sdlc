namespace PlataformaRio2C.Infra.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveConference : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ConferenceLecturer", "CollaboratorId", "dbo.Collaborator");
            DropForeignKey("dbo.ConferenceLecturer", "ConferenceId", "dbo.Conference");
            DropForeignKey("dbo.ConferenceLecturer", "ImageId", "dbo.ImageFile");
            DropForeignKey("dbo.Conference", "RoomId", "dbo.Room");
            DropForeignKey("dbo.ConferenceSynopsis", "ConferenceId", "dbo.Conference");
            DropForeignKey("dbo.ConferenceSynopsis", "LanguageId", "dbo.Language");
            DropForeignKey("dbo.ConferenceTitle", "ConferenceId", "dbo.Conference");
            DropForeignKey("dbo.ConferenceTitle", "LanguageId", "dbo.Language");
            DropIndex("dbo.Room", new[] { "Name" });
            DropIndex("dbo.Room", new[] { "Uid" });
            DropIndex("dbo.Conference", new[] { "RoomId" });
            DropIndex("dbo.Conference", new[] { "Uid" });
            DropIndex("dbo.ConferenceLecturer", new[] { "ConferenceId" });
            DropIndex("dbo.ConferenceLecturer", new[] { "CollaboratorId" });
            DropIndex("dbo.ConferenceLecturer", new[] { "ImageId" });
            DropIndex("dbo.ConferenceLecturer", new[] { "Uid" });
            DropIndex("dbo.ConferenceSynopsis", new[] { "LanguageId" });
            DropIndex("dbo.ConferenceSynopsis", new[] { "ConferenceId" });
            DropIndex("dbo.ConferenceSynopsis", new[] { "Uid" });
            DropIndex("dbo.ConferenceTitle", new[] { "LanguageId" });
            DropIndex("dbo.ConferenceTitle", new[] { "ConferenceId" });
            DropIndex("dbo.ConferenceTitle", new[] { "Uid" });
            DropTable("dbo.Room");
            DropTable("dbo.Conference");
            DropTable("dbo.ConferenceLecturer");
            DropTable("dbo.ConferenceSynopsis");
            DropTable("dbo.ConferenceTitle");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.Id);
            
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
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ConferenceLecturer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ConferenceId = c.Int(nullable: false),
                        CollaboratorId = c.Int(),
                        IsPreRegistered = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 1000, unicode: false),
                        ImageId = c.Int(),
                        LecturerRole = c.String(maxLength: 200, unicode: false),
                        Uid = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Conference",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(),
                        StartTime = c.Time(nullable: false, precision: 7),
                        EndTime = c.Time(nullable: false, precision: 7),
                        Info = c.String(maxLength: 3000, unicode: false),
                        RoomId = c.Int(),
                        Uid = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Room",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100, unicode: false),
                        Uid = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.ConferenceTitle", "Uid", unique: true);
            CreateIndex("dbo.ConferenceTitle", "ConferenceId");
            CreateIndex("dbo.ConferenceTitle", "LanguageId");
            CreateIndex("dbo.ConferenceSynopsis", "Uid", unique: true);
            CreateIndex("dbo.ConferenceSynopsis", "ConferenceId");
            CreateIndex("dbo.ConferenceSynopsis", "LanguageId");
            CreateIndex("dbo.ConferenceLecturer", "Uid", unique: true);
            CreateIndex("dbo.ConferenceLecturer", "ImageId");
            CreateIndex("dbo.ConferenceLecturer", "CollaboratorId");
            CreateIndex("dbo.ConferenceLecturer", "ConferenceId");
            CreateIndex("dbo.Conference", "Uid", unique: true);
            CreateIndex("dbo.Conference", "RoomId");
            CreateIndex("dbo.Room", "Uid", unique: true);
            CreateIndex("dbo.Room", "Name", unique: true);
            AddForeignKey("dbo.ConferenceTitle", "LanguageId", "dbo.Language", "Id");
            AddForeignKey("dbo.ConferenceTitle", "ConferenceId", "dbo.Conference", "Id");
            AddForeignKey("dbo.ConferenceSynopsis", "LanguageId", "dbo.Language", "Id");
            AddForeignKey("dbo.ConferenceSynopsis", "ConferenceId", "dbo.Conference", "Id");
            AddForeignKey("dbo.Conference", "RoomId", "dbo.Room", "Id");
            AddForeignKey("dbo.ConferenceLecturer", "ImageId", "dbo.ImageFile", "Id");
            AddForeignKey("dbo.ConferenceLecturer", "ConferenceId", "dbo.Conference", "Id");
            AddForeignKey("dbo.ConferenceLecturer", "CollaboratorId", "dbo.Collaborator", "Id");
        }
    }
}
