namespace PlataformaRio2C.Infra.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserUseTermRemoveRoleString : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.UserUseTerm", "Role");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserUseTerm", "Role", c => c.String(nullable: false, maxLength: 50, unicode: false));
        }
    }
}
