namespace PlataformaRio2C.Infra.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserUseTermAddRoleId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserUseTerm", "RoleId", c => c.Int(nullable: false, defaultValueSql: "2"));
            CreateIndex("dbo.UserUseTerm", "RoleId");
            AddForeignKey("dbo.UserUseTerm", "RoleId", "dbo.AspNetRoles", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserUseTerm", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.UserUseTerm", new[] { "RoleId" });
            DropColumn("dbo.UserUseTerm", "RoleId");
        }
    }
}
