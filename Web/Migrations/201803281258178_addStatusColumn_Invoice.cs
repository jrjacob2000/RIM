namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addStatusColumn_Invoice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "Status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invoices", "Status");
        }
    }
}
