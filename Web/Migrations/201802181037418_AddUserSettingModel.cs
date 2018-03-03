namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserSettingModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserSettings",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        BusinessName = c.String(),
                        InvoiceNumber = c.Int(nullable: false),
                        PurchaseNumber = c.Int(nullable: false),
                        SalesNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserSettings");
        }
    }
}
