namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newCreditTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CreditInvoices",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Credit_Id = c.Guid(nullable: false),
                        Invoice_Id = c.Guid(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(nullable: false),
                        CreatedBy = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Credits", t => t.Credit_Id)
                .ForeignKey("dbo.Invoices", t => t.Invoice_Id)
                .Index(t => t.Credit_Id)
                .Index(t => t.Invoice_Id);
            
            CreateTable(
                "dbo.Credits",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Partner_Id = c.Guid(nullable: false),
                        CreditNumber = c.String(),
                        Order_Id = c.Guid(nullable: false),
                        CreditDate = c.DateTime(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                        CreatedBy = c.Guid(nullable: false),
                        Status = c.String(),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.Order_Id)
                .ForeignKey("dbo.Partners", t => t.Partner_Id)
                .Index(t => t.Partner_Id)
                .Index(t => t.Order_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CreditInvoices", "Invoice_Id", "dbo.Invoices");
            DropForeignKey("dbo.CreditInvoices", "Credit_Id", "dbo.Credits");
            DropForeignKey("dbo.Credits", "Partner_Id", "dbo.Partners");
            DropForeignKey("dbo.Credits", "Order_Id", "dbo.Orders");
            DropIndex("dbo.Credits", new[] { "Order_Id" });
            DropIndex("dbo.Credits", new[] { "Partner_Id" });
            DropIndex("dbo.CreditInvoices", new[] { "Invoice_Id" });
            DropIndex("dbo.CreditInvoices", new[] { "Credit_Id" });
            DropTable("dbo.Credits");
            DropTable("dbo.CreditInvoices");
        }
    }
}
