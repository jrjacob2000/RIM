namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCreditIdToPaymentDatail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PaymentDetails", "Credit_Id", c => c.Guid());
            CreateIndex("dbo.PaymentDetails", "Credit_Id");
            AddForeignKey("dbo.PaymentDetails", "Credit_Id", "dbo.Credits", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PaymentDetails", "Credit_Id", "dbo.Credits");
            DropIndex("dbo.PaymentDetails", new[] { "Credit_Id" });
            DropColumn("dbo.PaymentDetails", "Credit_Id");
        }
    }
}
