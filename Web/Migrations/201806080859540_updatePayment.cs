namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatePayment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PaymentDetails", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.PaymentDetails", "Reference", c => c.String());
            AddColumn("dbo.PaymentDetails", "Notes", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PaymentDetails", "Notes");
            DropColumn("dbo.PaymentDetails", "Reference");
            DropColumn("dbo.PaymentDetails", "Date");
        }
    }
}
