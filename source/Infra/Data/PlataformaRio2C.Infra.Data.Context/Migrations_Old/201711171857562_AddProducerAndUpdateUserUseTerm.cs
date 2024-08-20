namespace PlataformaRio2C.Infra.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProducerAndUpdateUserUseTerm : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Collaborator", new[] { "PlayerId" });
            CreateTable(
                "dbo.CollaboratorProducer",
                c => new
                    {
                        CollaboratorId = c.Int(nullable: false),
                        ProducerId = c.Int(nullable: false),
                        EventId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CollaboratorId, t.ProducerId, t.EventId })
                .ForeignKey("dbo.Collaborator", t => t.CollaboratorId)
                .ForeignKey("dbo.Event", t => t.EventId)
                .ForeignKey("dbo.Producer", t => t.ProducerId)
                .Index(t => t.CollaboratorId)
                .Index(t => t.ProducerId)
                .Index(t => t.EventId);
            
            CreateTable(
                "dbo.Producer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100, unicode: false),
                        TradeName = c.String(maxLength: 100, unicode: false),
                        CNPJ = c.String(nullable: false, maxLength: 50, unicode: false),
                        Website = c.String(maxLength: 100, unicode: false),
                        SocialMedia = c.String(maxLength: 256, unicode: false),
                        PhoneNumber = c.String(maxLength: 50, unicode: false),
                        ImageId = c.Int(),
                        AddressId = c.Int(),
                        Uid = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Address", t => t.AddressId)
                .ForeignKey("dbo.ImageFile", t => t.ImageId)
                .Index(t => t.CNPJ, unique: true, name: "IX_Cnpj")
                .Index(t => t.ImageId)
                .Index(t => t.AddressId)
                .Index(t => t.Uid, unique: true);
            
            CreateTable(
                "dbo.ProducerDescription",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(maxLength: 8000, unicode: false),
                        LanguageId = c.Int(nullable: false),
                        ProducerId = c.Int(nullable: false),
                        Uid = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Language", t => t.LanguageId)
                .ForeignKey("dbo.Producer", t => t.ProducerId)
                .Index(t => t.LanguageId)
                .Index(t => t.ProducerId)
                .Index(t => t.Uid, unique: true);
            
            CreateTable(
                "dbo.ProducerEvent",
                c => new
                    {
                        ProducerId = c.Int(nullable: false),
                        EventId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProducerId, t.EventId })
                .ForeignKey("dbo.Event", t => t.EventId)
                .ForeignKey("dbo.Producer", t => t.ProducerId)
                .Index(t => t.ProducerId)
                .Index(t => t.EventId);
            
            AddColumn("dbo.Event", "StartDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Event", "EndDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.UserUseTerm", "Role", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AlterColumn("dbo.Collaborator", "PlayerId", c => c.Int());
            CreateIndex("dbo.Collaborator", "PlayerId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProducerEvent", "ProducerId", "dbo.Producer");
            DropForeignKey("dbo.ProducerEvent", "EventId", "dbo.Event");
            DropForeignKey("dbo.CollaboratorProducer", "ProducerId", "dbo.Producer");
            DropForeignKey("dbo.Producer", "ImageId", "dbo.ImageFile");
            DropForeignKey("dbo.ProducerDescription", "ProducerId", "dbo.Producer");
            DropForeignKey("dbo.ProducerDescription", "LanguageId", "dbo.Language");
            DropForeignKey("dbo.Producer", "AddressId", "dbo.Address");
            DropForeignKey("dbo.CollaboratorProducer", "EventId", "dbo.Event");
            DropForeignKey("dbo.CollaboratorProducer", "CollaboratorId", "dbo.Collaborator");
            DropIndex("dbo.ProducerEvent", new[] { "EventId" });
            DropIndex("dbo.ProducerEvent", new[] { "ProducerId" });
            DropIndex("dbo.ProducerDescription", new[] { "Uid" });
            DropIndex("dbo.ProducerDescription", new[] { "ProducerId" });
            DropIndex("dbo.ProducerDescription", new[] { "LanguageId" });
            DropIndex("dbo.Producer", new[] { "Uid" });
            DropIndex("dbo.Producer", new[] { "AddressId" });
            DropIndex("dbo.Producer", new[] { "ImageId" });
            DropIndex("dbo.Producer", "IX_Cnpj");
            DropIndex("dbo.CollaboratorProducer", new[] { "EventId" });
            DropIndex("dbo.CollaboratorProducer", new[] { "ProducerId" });
            DropIndex("dbo.CollaboratorProducer", new[] { "CollaboratorId" });
            DropIndex("dbo.Collaborator", new[] { "PlayerId" });
            AlterColumn("dbo.Collaborator", "PlayerId", c => c.Int(nullable: false));
            DropColumn("dbo.UserUseTerm", "Role");
            DropColumn("dbo.Event", "EndDate");
            DropColumn("dbo.Event", "StartDate");
            DropTable("dbo.ProducerEvent");
            DropTable("dbo.ProducerDescription");
            DropTable("dbo.Producer");
            DropTable("dbo.CollaboratorProducer");
            CreateIndex("dbo.Collaborator", "PlayerId");
        }
    }
}
