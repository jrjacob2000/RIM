namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTypecolumn_invoice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "Type", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invoices", "Type");
        }
    }
}
