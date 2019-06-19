namespace PlataformaRio2C.Infra.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateConferenceRemoveImage : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Conference", "ImageId", "dbo.ImageFile");
            DropIndex("dbo.Conference", new[] { "ImageId" });
            AddColumn("dbo.ConferenceLecturer", "ImageId", c => c.Int());
            CreateIndex("dbo.ConferenceLecturer", "ImageId");
            AddForeignKey("dbo.ConferenceLecturer", "ImageId", "dbo.ImageFile", "Id");
            DropColumn("dbo.Conference", "ImageId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Conference", "ImageId", c => c.Int());
            DropForeignKey("dbo.ConferenceLecturer", "ImageId", "dbo.ImageFile");
            DropIndex("dbo.ConferenceLecturer", new[] { "ImageId" });
            DropColumn("dbo.ConferenceLecturer", "ImageId");
            CreateIndex("dbo.Conference", "ImageId");
            AddForeignKey("dbo.Conference", "ImageId", "dbo.ImageFile", "Id");
        }
    }
}
