namespace PlataformaRio2C.Infra.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModelSprint2MiniBioAndSocialMediaAndTradeNameAndUserUseTerm : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserUseTerm",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        EventId = c.Int(nullable: false),
                        Uid = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Event", t => t.EventId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.EventId)
                .Index(t => t.Uid, unique: true);            
           
            
            AddColumn("dbo.Collaborator", "PhoneNumber", c => c.String(maxLength: 50, unicode: false));
            AddColumn("dbo.Collaborator", "CellPhone", c => c.String(maxLength: 50, unicode: false));
            AddColumn("dbo.Player", "TradeName", c => c.String(maxLength: 100, unicode: false));
            AddColumn("dbo.Player", "SocialMedia", c => c.String(maxLength: 256, unicode: false));
            AlterColumn("dbo.CollaboratorJobTitle", "Value", c => c.String(maxLength: 256, unicode: false));
            AlterColumn("dbo.CollaboratorMiniBio", "Value", c => c.String(maxLength: 8000, unicode: false));
            AlterColumn("dbo.Player", "Website", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.PlayerDescription", "Value", c => c.String(maxLength: 8000, unicode: false));
            AlterColumn("dbo.HoldingDescription", "Value", c => c.String(maxLength: 8000, unicode: false));            
        }
        
        public override void Down()
        {            
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.UserUseTerm", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserUseTerm", "EventId", "dbo.Event");
            DropForeignKey("dbo.AspNetRoles", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.UserUseTerm", new[] { "Uid" });
            DropIndex("dbo.UserUseTerm", new[] { "EventId" });
            DropIndex("dbo.UserUseTerm", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", new[] { "User_Id" });
            AlterColumn("dbo.HoldingDescription", "Value", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("dbo.PlayerDescription", "Value", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("dbo.Player", "Website", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("dbo.CollaboratorMiniBio", "Value", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("dbo.CollaboratorJobTitle", "Value", c => c.String(maxLength: 50, unicode: false));
            DropColumn("dbo.Player", "SocialMedia");
            DropColumn("dbo.Player", "TradeName");
            DropColumn("dbo.Collaborator", "CellPhone");
            DropColumn("dbo.Collaborator", "PhoneNumber");            
            DropTable("dbo.UserUseTerm");           
        }
    }
}
