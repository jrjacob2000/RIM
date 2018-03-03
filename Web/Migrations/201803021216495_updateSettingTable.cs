namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateSettingTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "InvoiceNumber", c => c.String());
            AlterColumn("dbo.UserSettings", "InvoiceNumber", c => c.String());
            AlterColumn("dbo.UserSettings", "PurchaseNumber", c => c.String());
            AlterColumn("dbo.UserSettings", "SalesNumber", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserSettings", "SalesNumber", c => c.Int());
            AlterColumn("dbo.UserSettings", "PurchaseNumber", c => c.Int());
            AlterColumn("dbo.UserSettings", "InvoiceNumber", c => c.Int());
            AlterColumn("dbo.Orders", "InvoiceNumber", c => c.Int());
        }
    }
}
