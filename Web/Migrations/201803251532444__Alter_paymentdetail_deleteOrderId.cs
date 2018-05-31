namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _Alter_paymentdetail_deleteOrderId : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.PaymentDetails", new[] { "Order_Id" });
            AlterColumn("dbo.PaymentDetails", "Order_Id", c => c.Guid());
            CreateIndex("dbo.PaymentDetails", "Order_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.PaymentDetails", new[] { "Order_Id" });
            AlterColumn("dbo.PaymentDetails", "Order_Id", c => c.Guid(nullable: false));
            CreateIndex("dbo.PaymentDetails", "Order_Id");
        }
    }
}
