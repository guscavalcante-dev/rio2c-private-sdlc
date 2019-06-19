namespace PlataformaRio2C.Infra.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateConferenceAddArrivalTimeAndDepartureTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Conference", "ArrivalTime", c => c.Time(nullable: false, precision: 7));
            AddColumn("dbo.Conference", "DepartureTime", c => c.Time(nullable: false, precision: 7));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Conference", "DepartureTime");
            DropColumn("dbo.Conference", "ArrivalTime");
        }
    }
}
