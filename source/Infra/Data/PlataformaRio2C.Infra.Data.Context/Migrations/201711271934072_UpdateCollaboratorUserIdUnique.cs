namespace PlataformaRio2C.Infra.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCollaboratorUserIdUnique : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Collaborator", new[] { "UserId" });
            CreateIndex("dbo.Collaborator", "UserId", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Collaborator", new[] { "UserId" });
            CreateIndex("dbo.Collaborator", "UserId");
        }
    }
}
