namespace PlataformaRio2C.Infra.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNegotiation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Negotiation",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProjectId = c.Int(nullable: false),
                        PlayerId = c.Int(nullable: false),
                        RoomId = c.Int(nullable: false),
                        EvaluationId = c.Int(),
                        Date = c.DateTime(nullable: false),
                        StarTime = c.Time(nullable: false, precision: 7),
                        EndTime = c.Time(nullable: false, precision: 7),
                        TableNumber = c.Int(nullable: false),
                        RoundNumber = c.Int(nullable: false),
                        Uid = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProjectPlayerEvaluation", t => t.EvaluationId)
                .ForeignKey("dbo.Player", t => t.PlayerId)
                .ForeignKey("dbo.Project", t => t.ProjectId)
                .ForeignKey("dbo.Room", t => t.RoomId)
                .Index(t => t.ProjectId)
                .Index(t => t.PlayerId)
                .Index(t => t.RoomId)
                .Index(t => t.EvaluationId)
                .Index(t => t.Uid, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Negotiation", "RoomId", "dbo.Room");
            DropForeignKey("dbo.Negotiation", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.Negotiation", "PlayerId", "dbo.Player");
            DropForeignKey("dbo.Negotiation", "EvaluationId", "dbo.ProjectPlayerEvaluation");
            DropIndex("dbo.Negotiation", new[] { "Uid" });
            DropIndex("dbo.Negotiation", new[] { "EvaluationId" });
            DropIndex("dbo.Negotiation", new[] { "RoomId" });
            DropIndex("dbo.Negotiation", new[] { "PlayerId" });
            DropIndex("dbo.Negotiation", new[] { "ProjectId" });
            DropTable("dbo.Negotiation");
        }
    }
}
