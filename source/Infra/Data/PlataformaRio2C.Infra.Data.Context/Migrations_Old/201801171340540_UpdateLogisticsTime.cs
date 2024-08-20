namespace PlataformaRio2C.Infra.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateLogisticsTime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Logistics", "ArrivalTime", c => c.Time(nullable: false, precision: 7));
            AlterColumn("dbo.Logistics", "DepartureTime", c => c.Time(nullable: false, precision: 7));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Logistics", "DepartureTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Logistics", "ArrivalTime", c => c.DateTime(nullable: false));
        }
    }
}
