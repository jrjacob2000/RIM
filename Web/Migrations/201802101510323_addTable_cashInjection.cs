namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTable_cashInjection : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CashInjections",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Particulars = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CashInjections");
        }
    }
}
