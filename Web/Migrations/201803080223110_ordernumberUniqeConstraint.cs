namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ordernumberUniqeConstraint : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "OrderNumber", c => c.String(nullable: false, maxLength: 250));
            CreateIndex("dbo.Orders", "OrderNumber", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Orders", new[] { "OrderNumber" });
            AlterColumn("dbo.Orders", "OrderNumber", c => c.String(nullable: false));
        }
    }
}
