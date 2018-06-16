namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userSettingsUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserSettings", "BillPrefix", c => c.String());
            AddColumn("dbo.UserSettings", "BillNumber", c => c.String());
            AddColumn("dbo.UserSettings", "CreditNotePrefix", c => c.String());
            AddColumn("dbo.UserSettings", "CreditNoteNumber", c => c.String());
            DropColumn("dbo.UserSettings", "CustomerReturnPrefix");
            DropColumn("dbo.UserSettings", "CustomerReturnNumber");
            DropColumn("dbo.UserSettings", "SupplierReturnPrefix");
            DropColumn("dbo.UserSettings", "SupplierReturnNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserSettings", "SupplierReturnNumber", c => c.String());
            AddColumn("dbo.UserSettings", "SupplierReturnPrefix", c => c.String());
            AddColumn("dbo.UserSettings", "CustomerReturnNumber", c => c.String());
            AddColumn("dbo.UserSettings", "CustomerReturnPrefix", c => c.String());
            DropColumn("dbo.UserSettings", "CreditNoteNumber");
            DropColumn("dbo.UserSettings", "CreditNotePrefix");
            DropColumn("dbo.UserSettings", "BillNumber");
            DropColumn("dbo.UserSettings", "BillPrefix");
        }
    }
}
