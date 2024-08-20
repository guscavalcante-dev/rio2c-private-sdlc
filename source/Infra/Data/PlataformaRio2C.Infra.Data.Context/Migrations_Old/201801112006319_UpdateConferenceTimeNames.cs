namespace PlataformaRio2C.Infra.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateConferenceTimeNames : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Conference", "StartTime", c => c.Time(nullable: false, precision: 7));
            AddColumn("dbo.Conference", "EndTime", c => c.Time(nullable: false, precision: 7));
            DropColumn("dbo.Conference", "ArrivalTime");
            DropColumn("dbo.Conference", "DepartureTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Conference", "DepartureTime", c => c.Time(nullable: false, precision: 7));
            AddColumn("dbo.Conference", "ArrivalTime", c => c.Time(nullable: false, precision: 7));
            DropColumn("dbo.Conference", "EndTime");
            DropColumn("dbo.Conference", "StartTime");
        }
    }
}
