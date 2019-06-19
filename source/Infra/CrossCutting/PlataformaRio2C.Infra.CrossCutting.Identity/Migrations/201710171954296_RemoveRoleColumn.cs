namespace PlataformaRio2C.Infra.CrossCutting.Identity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveRoleColumn : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "Role");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Role", c => c.Int(nullable: false));
        }
    }
}
