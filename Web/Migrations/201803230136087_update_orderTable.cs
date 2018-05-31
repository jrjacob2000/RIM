namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_orderTable : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Orders", new[] { "Partner_Id" });
            AlterColumn("dbo.Orders", "Partner_Id", c => c.Guid());
            CreateIndex("dbo.Orders", "Partner_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Orders", new[] { "Partner_Id" });
            AlterColumn("dbo.Orders", "Partner_Id", c => c.Guid(nullable: false));
            CreateIndex("dbo.Orders", "Partner_Id");
        }
    }
}
