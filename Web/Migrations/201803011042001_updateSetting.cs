namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateSetting : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserSettings", "InvoicePrefix", c => c.Int());
            AddColumn("dbo.UserSettings", "PurchasePrefix", c => c.String());
            AddColumn("dbo.UserSettings", "SalesPrefix", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserSettings", "SalesPrefix");
            DropColumn("dbo.UserSettings", "PurchasePrefix");
            DropColumn("dbo.UserSettings", "InvoicePrefix");
        }
    }
}
