namespace PlataformaRio2C.Infra.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAddressMaxLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Address", "AddressValue", c => c.String(maxLength: 1000, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Address", "AddressValue", c => c.String(maxLength: 50, unicode: false));
        }
    }
}
