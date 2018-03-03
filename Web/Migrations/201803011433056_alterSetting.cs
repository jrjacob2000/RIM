namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alterSetting : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserSettings", "InvoicePrefix", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserSettings", "InvoicePrefix", c => c.Int());
        }
    }
}
