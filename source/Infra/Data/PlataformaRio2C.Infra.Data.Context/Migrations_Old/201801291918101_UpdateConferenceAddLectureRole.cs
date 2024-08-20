namespace PlataformaRio2C.Infra.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateConferenceAddLectureRole : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ConferenceLecturer", "LecturerRole", c => c.String(maxLength: 200, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ConferenceLecturer", "LecturerRole");
        }
    }
}
