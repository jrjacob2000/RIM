namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Credits", "Invoice_Id", c => c.Guid());
            CreateIndex("dbo.Credits", "Invoice_Id");
            AddForeignKey("dbo.Credits", "Invoice_Id", "dbo.Invoices", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Credits", "Invoice_Id", "dbo.Invoices");
            DropIndex("dbo.Credits", new[] { "Invoice_Id" });
            DropColumn("dbo.Credits", "Invoice_Id");
        }
    }
}
