namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createInvoice : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Partner_Id = c.Guid(nullable: false),
                        InvoiceNumber = c.String(),
                        Order_Id = c.Guid(nullable: false),
                        InvoiceDate = c.DateTime(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                        OtherCharges = c.Decimal(precision: 18, scale: 2),
                        OrderDiscount = c.Decimal(precision: 18, scale: 2),
                        TaxRate = c.Decimal(precision: 18, scale: 2),
                        CreatedBy = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.Order_Id)
                .ForeignKey("dbo.Partners", t => t.Partner_Id)
                .Index(t => t.Partner_Id)
                .Index(t => t.Order_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Invoices", "Partner_Id", "dbo.Partners");
            DropForeignKey("dbo.Invoices", "Order_Id", "dbo.Orders");
            DropIndex("dbo.Invoices", new[] { "Order_Id" });
            DropIndex("dbo.Invoices", new[] { "Partner_Id" });
            DropTable("dbo.Invoices");
        }
    }
}
