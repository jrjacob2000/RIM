namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addStatusToPO : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Status");
        }
    }
}
