namespace PlataformaRio2C.Infra.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddConfigNegotiation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NegotiationConfig",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        StartTime = c.Time(nullable: false, precision: 7),
                        EndTime = c.Time(nullable: false, precision: 7),
                        RoudsFirstTurn = c.Int(nullable: false),
                        RoundsSecondTurn = c.Int(nullable: false),
                        TimeIntervalBetweenTurn = c.Time(nullable: false, precision: 7),
                        TimeOfEachRound = c.Time(nullable: false, precision: 7),
                        TimeIntervalBetweenRound = c.Time(nullable: false, precision: 7),
                        Uid = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Uid, unique: true);
            
            CreateTable(
                "dbo.NegotiationRoomConfig",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoomId = c.Int(nullable: false),
                        CountAutomaticTables = c.Int(nullable: false),
                        NegotiationConfigId = c.Int(nullable: false),
                        Uid = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NegotiationConfig", t => t.NegotiationConfigId)
                .ForeignKey("dbo.Room", t => t.RoomId)
                .Index(t => t.RoomId)
                .Index(t => t.NegotiationConfigId)
                .Index(t => t.Uid, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NegotiationRoomConfig", "RoomId", "dbo.Room");
            DropForeignKey("dbo.NegotiationRoomConfig", "NegotiationConfigId", "dbo.NegotiationConfig");
            DropIndex("dbo.NegotiationRoomConfig", new[] { "Uid" });
            DropIndex("dbo.NegotiationRoomConfig", new[] { "NegotiationConfigId" });
            DropIndex("dbo.NegotiationRoomConfig", new[] { "RoomId" });
            DropIndex("dbo.NegotiationConfig", new[] { "Uid" });
            DropTable("dbo.NegotiationRoomConfig");
            DropTable("dbo.NegotiationConfig");
        }
    }
}
