namespace PlataformaRio2C.Infra.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Collaborator",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50, unicode: false),
                        AddressId = c.Int(),
                        PlayerId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        ImageId = c.Int(),
                        Uid = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Address", t => t.AddressId)
                .ForeignKey("dbo.ImageFile", t => t.ImageId)
                .ForeignKey("dbo.Player", t => t.PlayerId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.AddressId)
                .Index(t => t.PlayerId)
                .Index(t => t.UserId)
                .Index(t => t.ImageId)
                .Index(t => t.Uid, unique: true);
            
            CreateTable(
                "dbo.Address",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ZipCode = c.String(maxLength: 50, unicode: false),
                        Country = c.String(maxLength: 50, unicode: false),
                        State = c.String(maxLength: 50, unicode: false),
                        City = c.String(maxLength: 50, unicode: false),
                        AddressValue = c.String(maxLength: 50, unicode: false),
                        Uid = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Uid, unique: true);
            
            CreateTable(
                "dbo.ImageFile",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileName = c.String(nullable: false, maxLength: 500, unicode: false),
                        File = c.Binary(nullable: false),
                        ContentType = c.String(nullable: false, maxLength: 200, unicode: false),
                        ContentLength = c.Int(nullable: false),
                        Uid = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Uid, unique: true);
            
            CreateTable(
                "dbo.CollaboratorJobTitle",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(maxLength: 50, unicode: false),
                        LanguageId = c.Int(nullable: false),
                        CollaboratoId = c.Int(nullable: false),
                        Uid = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Collaborator", t => t.CollaboratoId)
                .ForeignKey("dbo.Language", t => t.LanguageId)
                .Index(t => t.LanguageId)
                .Index(t => t.CollaboratoId)
                .Index(t => t.Uid, unique: true);
            
            CreateTable(
                "dbo.Language",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Code = c.String(nullable: false, maxLength: 50, unicode: false),
                        Uid = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Uid, unique: true);
            
            CreateTable(
                "dbo.CollaboratorMiniBio",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(maxLength: 50, unicode: false),
                        LanguageId = c.Int(nullable: false),
                        CollaboratoId = c.Int(nullable: false),
                        Uid = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Collaborator", t => t.CollaboratoId)
                .ForeignKey("dbo.Language", t => t.LanguageId)
                .Index(t => t.LanguageId)
                .Index(t => t.CollaboratoId)
                .Index(t => t.Uid, unique: true);
            
            CreateTable(
                "dbo.Player",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100, unicode: false),
                        CNPJ = c.String(maxLength: 50, unicode: false),
                        Website = c.String(maxLength: 50, unicode: false),
                        PhoneNumber = c.String(maxLength: 50, unicode: false),
                        ImageId = c.Int(),
                        AddressId = c.Int(),
                        HoldingId = c.Int(nullable: false),
                        Uid = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Address", t => t.AddressId)
                .ForeignKey("dbo.Holding", t => t.HoldingId)
                .ForeignKey("dbo.ImageFile", t => t.ImageId)
                .Index(t => t.Name, unique: true)
                .Index(t => t.ImageId)
                .Index(t => t.AddressId)
                .Index(t => t.HoldingId)
                .Index(t => t.Uid, unique: true);
            
            CreateTable(
                "dbo.PlayerDescription",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(maxLength: 50, unicode: false),
                        LanguageId = c.Int(nullable: false),
                        PlayerId = c.Int(nullable: false),
                        Uid = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Language", t => t.LanguageId)
                .ForeignKey("dbo.Player", t => t.PlayerId)
                .Index(t => t.LanguageId)
                .Index(t => t.PlayerId)
                .Index(t => t.Uid, unique: true);
            
            CreateTable(
                "dbo.Holding",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100, unicode: false),
                        ImageId = c.Int(),
                        Uid = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ImageFile", t => t.ImageId)
                .Index(t => t.Name, unique: true)
                .Index(t => t.ImageId)
                .Index(t => t.Uid, unique: true);
            
            CreateTable(
                "dbo.HoldingDescription",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(maxLength: 50, unicode: false),
                        LanguageId = c.Int(nullable: false),
                        HoldingId = c.Int(nullable: false),
                        Uid = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Holding", t => t.HoldingId)
                .ForeignKey("dbo.Language", t => t.LanguageId)
                .Index(t => t.LanguageId)
                .Index(t => t.HoldingId)
                .Index(t => t.Uid, unique: true);
            
            CreateTable(
                "dbo.Event",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50, unicode: false),
                        Uid = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Uid, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Collaborator", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Collaborator", "PlayerId", "dbo.Player");
            DropForeignKey("dbo.Player", "ImageId", "dbo.ImageFile");
            DropForeignKey("dbo.Player", "HoldingId", "dbo.Holding");
            DropForeignKey("dbo.Holding", "ImageId", "dbo.ImageFile");
            DropForeignKey("dbo.HoldingDescription", "LanguageId", "dbo.Language");
            DropForeignKey("dbo.HoldingDescription", "HoldingId", "dbo.Holding");
            DropForeignKey("dbo.PlayerDescription", "PlayerId", "dbo.Player");
            DropForeignKey("dbo.PlayerDescription", "LanguageId", "dbo.Language");
            DropForeignKey("dbo.Player", "AddressId", "dbo.Address");
            DropForeignKey("dbo.CollaboratorMiniBio", "LanguageId", "dbo.Language");
            DropForeignKey("dbo.CollaboratorMiniBio", "CollaboratoId", "dbo.Collaborator");
            DropForeignKey("dbo.CollaboratorJobTitle", "LanguageId", "dbo.Language");
            DropForeignKey("dbo.CollaboratorJobTitle", "CollaboratoId", "dbo.Collaborator");
            DropForeignKey("dbo.Collaborator", "ImageId", "dbo.ImageFile");
            DropForeignKey("dbo.Collaborator", "AddressId", "dbo.Address");
            DropIndex("dbo.Event", new[] { "Uid" });
            DropIndex("dbo.AspNetUsers", new[] { "Uid" });
            DropIndex("dbo.HoldingDescription", new[] { "Uid" });
            DropIndex("dbo.HoldingDescription", new[] { "HoldingId" });
            DropIndex("dbo.HoldingDescription", new[] { "LanguageId" });
            DropIndex("dbo.Holding", new[] { "Uid" });
            DropIndex("dbo.Holding", new[] { "ImageId" });
            DropIndex("dbo.Holding", new[] { "Name" });
            DropIndex("dbo.PlayerDescription", new[] { "Uid" });
            DropIndex("dbo.PlayerDescription", new[] { "PlayerId" });
            DropIndex("dbo.PlayerDescription", new[] { "LanguageId" });
            DropIndex("dbo.Player", new[] { "Uid" });
            DropIndex("dbo.Player", new[] { "HoldingId" });
            DropIndex("dbo.Player", new[] { "AddressId" });
            DropIndex("dbo.Player", new[] { "ImageId" });
            DropIndex("dbo.Player", new[] { "Name" });
            DropIndex("dbo.CollaboratorMiniBio", new[] { "Uid" });
            DropIndex("dbo.CollaboratorMiniBio", new[] { "CollaboratoId" });
            DropIndex("dbo.CollaboratorMiniBio", new[] { "LanguageId" });
            DropIndex("dbo.Language", new[] { "Uid" });
            DropIndex("dbo.CollaboratorJobTitle", new[] { "Uid" });
            DropIndex("dbo.CollaboratorJobTitle", new[] { "CollaboratoId" });
            DropIndex("dbo.CollaboratorJobTitle", new[] { "LanguageId" });
            DropIndex("dbo.ImageFile", new[] { "Uid" });
            DropIndex("dbo.Address", new[] { "Uid" });
            DropIndex("dbo.Collaborator", new[] { "Uid" });
            DropIndex("dbo.Collaborator", new[] { "ImageId" });
            DropIndex("dbo.Collaborator", new[] { "UserId" });
            DropIndex("dbo.Collaborator", new[] { "PlayerId" });
            DropIndex("dbo.Collaborator", new[] { "AddressId" });
            DropTable("dbo.Event");
            DropTable("dbo.HoldingDescription");
            DropTable("dbo.Holding");
            DropTable("dbo.PlayerDescription");
            DropTable("dbo.Player");
            DropTable("dbo.CollaboratorMiniBio");
            DropTable("dbo.Language");
            DropTable("dbo.CollaboratorJobTitle");
            DropTable("dbo.ImageFile");
            DropTable("dbo.Address");
            DropTable("dbo.Collaborator");
        }
    }
}
