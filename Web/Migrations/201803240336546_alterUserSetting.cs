namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alterUserSetting : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserSettings", "AdjustPrefix", c => c.String());
            AddColumn("dbo.UserSettings", "AdjustNumber", c => c.String());
            AddColumn("dbo.UserSettings", "CustomerReturnPrefix", c => c.String());
            AddColumn("dbo.UserSettings", "CustomerReturnNumber", c => c.String());
            AddColumn("dbo.UserSettings", "SupplierReturnPrefix", c => c.String());
            AddColumn("dbo.UserSettings", "SupplierReturnNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserSettings", "SupplierReturnNumber");
            DropColumn("dbo.UserSettings", "SupplierReturnPrefix");
            DropColumn("dbo.UserSettings", "CustomerReturnNumber");
            DropColumn("dbo.UserSettings", "CustomerReturnPrefix");
            DropColumn("dbo.UserSettings", "AdjustNumber");
            DropColumn("dbo.UserSettings", "AdjustPrefix");
        }
    }
}
