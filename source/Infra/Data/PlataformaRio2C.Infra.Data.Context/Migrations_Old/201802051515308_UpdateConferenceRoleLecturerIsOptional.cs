namespace PlataformaRio2C.Infra.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateConferenceRoleLecturerIsOptional : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ConferenceLecturer", new[] { "RoleLecturerId" });
            AlterColumn("dbo.ConferenceLecturer", "RoleLecturerId", c => c.Int());
            CreateIndex("dbo.ConferenceLecturer", "RoleLecturerId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ConferenceLecturer", new[] { "RoleLecturerId" });
            AlterColumn("dbo.ConferenceLecturer", "RoleLecturerId", c => c.Int(nullable: false));
            CreateIndex("dbo.ConferenceLecturer", "RoleLecturerId");
        }
    }
}
