namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDeleteColumn_Payment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Payments", "Deleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Payments", "Deleted");
        }
    }
}
