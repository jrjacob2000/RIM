namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alter_paymentdetail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PaymentDetails", "Invoice_Id", c => c.Guid());
            CreateIndex("dbo.PaymentDetails", "Invoice_Id");
            AddForeignKey("dbo.PaymentDetails", "Invoice_Id", "dbo.Invoices", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PaymentDetails", "Invoice_Id", "dbo.Invoices");
            DropIndex("dbo.PaymentDetails", new[] { "Invoice_Id" });
            DropColumn("dbo.PaymentDetails", "Invoice_Id");
        }
    }
}
