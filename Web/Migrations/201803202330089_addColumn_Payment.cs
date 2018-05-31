namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addColumn_Payment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Payments", "Type", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Payments", "Type");
        }
    }
}
