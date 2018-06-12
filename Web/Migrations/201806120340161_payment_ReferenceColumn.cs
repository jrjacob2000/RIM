namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class payment_ReferenceColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Payments", "Reference", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Payments", "Reference");
        }
    }
}
