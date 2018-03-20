namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFieldsSetting : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserSettings", "BillingAddress", c => c.String());
            AddColumn("dbo.UserSettings", "ShippingAddress", c => c.String());
            AddColumn("dbo.UserSettings", "BusinessContactNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserSettings", "BusinessContactNumber");
            DropColumn("dbo.UserSettings", "ShippingAddress");
            DropColumn("dbo.UserSettings", "BillingAddress");
        }
    }
}
