namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCreatedByColumntoAll : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CashInjections", "CreatedBy", c => c.Guid(nullable: false));
            AddColumn("dbo.OrderDetails", "CreatedBy", c => c.Guid(nullable: false));
            AddColumn("dbo.Orders", "CreatedBy", c => c.Guid(nullable: false));
            AddColumn("dbo.Partners", "CreatedBy", c => c.Guid(nullable: false));
            AddColumn("dbo.PaymentDetails", "CreatedBy", c => c.Guid(nullable: false));
            AddColumn("dbo.Payments", "CreatedBy", c => c.Guid(nullable: false));
            AddColumn("dbo.ProductPrices", "CreatedBy", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductPrices", "CreatedBy");
            DropColumn("dbo.Payments", "CreatedBy");
            DropColumn("dbo.PaymentDetails", "CreatedBy");
            DropColumn("dbo.Partners", "CreatedBy");
            DropColumn("dbo.Orders", "CreatedBy");
            DropColumn("dbo.OrderDetails", "CreatedBy");
            DropColumn("dbo.CashInjections", "CreatedBy");
        }
    }
}
