namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adjustmentReason : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "AdjustmentReason", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "AdjustmentReason");
        }
    }
}
