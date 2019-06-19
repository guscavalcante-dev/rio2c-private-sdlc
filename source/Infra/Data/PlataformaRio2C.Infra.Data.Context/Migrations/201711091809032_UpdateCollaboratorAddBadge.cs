namespace PlataformaRio2C.Infra.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCollaboratorAddBadge : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Collaborator", "Badge", c => c.String(maxLength: 50, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Collaborator", "Badge");
        }
    }
}
