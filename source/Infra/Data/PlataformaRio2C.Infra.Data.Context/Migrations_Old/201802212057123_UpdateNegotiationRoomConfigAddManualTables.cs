namespace PlataformaRio2C.Infra.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateNegotiationRoomConfigAddManualTables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NegotiationRoomConfig", "CountManualTables", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.NegotiationRoomConfig", "CountManualTables");
        }
    }
}
