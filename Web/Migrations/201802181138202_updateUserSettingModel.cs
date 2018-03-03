namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateUserSettingModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserSettings", "InvoiceNumber", c => c.Int());
            AlterColumn("dbo.UserSettings", "PurchaseNumber", c => c.Int());
            AlterColumn("dbo.UserSettings", "SalesNumber", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserSettings", "SalesNumber", c => c.Int(nullable: false));
            AlterColumn("dbo.UserSettings", "PurchaseNumber", c => c.Int(nullable: false));
            AlterColumn("dbo.UserSettings", "InvoiceNumber", c => c.Int(nullable: false));
        }
    }
}
