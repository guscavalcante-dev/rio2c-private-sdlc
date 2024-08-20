namespace PlataformaRio2C.Infra.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddConferenceAndLecturer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RoleLecturer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Uid = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Uid, unique: true);
            
            CreateTable(
                "dbo.RoleLecturerTitle",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(maxLength: 256, unicode: false),
                        LanguageId = c.Int(nullable: false),
                        RoleLecturerId = c.Int(nullable: false),
                        Uid = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Language", t => t.LanguageId)
                .ForeignKey("dbo.RoleLecturer", t => t.RoleLecturerId)
                .Index(t => t.LanguageId)
                .Index(t => t.RoleLecturerId)
                .Index(t => t.Uid, unique: true);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Room", t => t.RoomId)
                .Index(t => t.RoomId)
                .Index(t => t.Uid, unique: true);
            
            CreateTable(
                "dbo.ConferenceLecturer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsPreRegistered = c.Boolean(nullable: false),
                        ConferenceId = c.Int(nullable: false),
                        CollaboratorId = c.Int(),
                        LecturerId = c.Int(),
                        RoleLecturerId = c.Int(nullable: false),
                        Uid = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Collaborator", t => t.CollaboratorId)
                .ForeignKey("dbo.Conference", t => t.ConferenceId)
                .ForeignKey("dbo.Lecturer", t => t.LecturerId)
                .ForeignKey("dbo.RoleLecturer", t => t.RoleLecturerId)
                .Index(t => t.ConferenceId)
                .Index(t => t.CollaboratorId)
                .Index(t => t.LecturerId)
                .Index(t => t.RoleLecturerId)
                .Index(t => t.Uid, unique: true);
            
            CreateTable(
                "dbo.Lecturer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 1000, unicode: false),
                        ImageId = c.Int(),
                        Email = c.String(maxLength: 50, unicode: false),
                        CompanyName = c.String(maxLength: 50, unicode: false),
                        Uid = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ImageFile", t => t.ImageId)
                .Index(t => t.ImageId)
                .Index(t => t.Uid, unique: true);
            
            CreateTable(
                "dbo.LecturerJobTitle",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(maxLength: 50, unicode: false),
                        LanguageId = c.Int(nullable: false),
                        LanguageCode = c.String(maxLength: 50, unicode: false),
                        LecturerId = c.Int(nullable: false),
                        Uid = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Language", t => t.LanguageId)
                .ForeignKey("dbo.Lecturer", t => t.LecturerId)
                .Index(t => t.LanguageId)
                .Index(t => t.LecturerId)
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
            DropForeignKey("dbo.Conference", "RoomId", "dbo.Room");
            DropForeignKey("dbo.ConferenceLecturer", "RoleLecturerId", "dbo.RoleLecturer");
            DropForeignKey("dbo.ConferenceLecturer", "LecturerId", "dbo.Lecturer");
            DropForeignKey("dbo.LecturerJobTitle", "LecturerId", "dbo.Lecturer");
            DropForeignKey("dbo.LecturerJobTitle", "LanguageId", "dbo.Language");
            DropForeignKey("dbo.Lecturer", "ImageId", "dbo.ImageFile");
            DropForeignKey("dbo.ConferenceLecturer", "ConferenceId", "dbo.Conference");
            DropForeignKey("dbo.ConferenceLecturer", "CollaboratorId", "dbo.Collaborator");
            DropForeignKey("dbo.RoleLecturerTitle", "RoleLecturerId", "dbo.RoleLecturer");
            DropForeignKey("dbo.RoleLecturerTitle", "LanguageId", "dbo.Language");
            DropIndex("dbo.ConferenceTitle", new[] { "Uid" });
            DropIndex("dbo.ConferenceTitle", new[] { "ConferenceId" });
            DropIndex("dbo.ConferenceTitle", new[] { "LanguageId" });
            DropIndex("dbo.ConferenceSynopsis", new[] { "Uid" });
            DropIndex("dbo.ConferenceSynopsis", new[] { "ConferenceId" });
            DropIndex("dbo.ConferenceSynopsis", new[] { "LanguageId" });
            DropIndex("dbo.LecturerJobTitle", new[] { "Uid" });
            DropIndex("dbo.LecturerJobTitle", new[] { "LecturerId" });
            DropIndex("dbo.LecturerJobTitle", new[] { "LanguageId" });
            DropIndex("dbo.Lecturer", new[] { "Uid" });
            DropIndex("dbo.Lecturer", new[] { "ImageId" });
            DropIndex("dbo.ConferenceLecturer", new[] { "Uid" });
            DropIndex("dbo.ConferenceLecturer", new[] { "RoleLecturerId" });
            DropIndex("dbo.ConferenceLecturer", new[] { "LecturerId" });
            DropIndex("dbo.ConferenceLecturer", new[] { "CollaboratorId" });
            DropIndex("dbo.ConferenceLecturer", new[] { "ConferenceId" });
            DropIndex("dbo.Conference", new[] { "Uid" });
            DropIndex("dbo.Conference", new[] { "RoomId" });
            DropIndex("dbo.Room", new[] { "Uid" });
            DropIndex("dbo.Room", new[] { "Name" });
            DropIndex("dbo.RoleLecturerTitle", new[] { "Uid" });
            DropIndex("dbo.RoleLecturerTitle", new[] { "RoleLecturerId" });
            DropIndex("dbo.RoleLecturerTitle", new[] { "LanguageId" });
            DropIndex("dbo.RoleLecturer", new[] { "Uid" });
            DropTable("dbo.ConferenceTitle");
            DropTable("dbo.ConferenceSynopsis");
            DropTable("dbo.LecturerJobTitle");
            DropTable("dbo.Lecturer");
            DropTable("dbo.ConferenceLecturer");
            DropTable("dbo.Conference");
            DropTable("dbo.Room");
            DropTable("dbo.RoleLecturerTitle");
            DropTable("dbo.RoleLecturer");
        }
    }
}
