namespace PlataformaRio2C.Infra.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateLogisticsDateTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Logistics", "ArrivalDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Logistics", "DepartureDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Logistics", "DepartureDate");
            DropColumn("dbo.Logistics", "ArrivalDate");
        }
    }
}
