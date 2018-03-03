namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Order_Id = c.Guid(nullable: false),
                        Product_Id = c.Guid(nullable: false),
                        Quantity = c.Int(nullable: false),
                        UnitDiscount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProductPrice_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.Order_Id)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .ForeignKey("dbo.ProductPrices", t => t.ProductPrice_Id)
                .Index(t => t.Order_Id)
                .Index(t => t.Product_Id)
                .Index(t => t.ProductPrice_Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        OrderNumber = c.String(nullable: false),
                        OrderDate = c.DateTime(nullable: false),
                        ExpectedDate = c.DateTime(nullable: false),
                        OrderType = c.String(nullable: false),
                        Partner_Id = c.Guid(nullable: false),
                        OtherCharges = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrderDiscount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TaxRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrderNotes = c.String(),
                        InvoiceNumber = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Partners", t => t.Partner_Id)
                .Index(t => t.Partner_Id);
            
            CreateTable(
                "dbo.Partners",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        ShippingAddress = c.String(),
                        BillingAddress = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                        Contact = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PaymentDetails",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Order_Id = c.Guid(nullable: false),
                        Payment_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.Order_Id)
                .ForeignKey("dbo.Payments", t => t.Payment_Id)
                .Index(t => t.Order_Id)
                .Index(t => t.Payment_Id);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(nullable: false),
                        Partner_Id = c.Guid(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Partners", t => t.Partner_Id)
                .Index(t => t.Partner_Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        StartingInventory = c.Int(nullable: false),
                        ReOrderPoint = c.Int(nullable: false),
                        Unit = c.String(),
                        Taxable = c.Boolean(nullable: false),
                        Category_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id)
                .Index(t => t.Category_Id);
            
            CreateTable(
                "dbo.ProductPrices",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        EffectiveFromDate = c.DateTime(nullable: false),
                        PurchasePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SelesPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Product_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.OrderDetails", "ProductPrice_Id", "dbo.ProductPrices");
            DropForeignKey("dbo.ProductPrices", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.OrderDetails", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.Products", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.PaymentDetails", "Payment_Id", "dbo.Payments");
            DropForeignKey("dbo.Payments", "Partner_Id", "dbo.Partners");
            DropForeignKey("dbo.PaymentDetails", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.Orders", "Partner_Id", "dbo.Partners");
            DropForeignKey("dbo.OrderDetails", "Order_Id", "dbo.Orders");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.ProductPrices", new[] { "Product_Id" });
            DropIndex("dbo.Products", new[] { "Category_Id" });
            DropIndex("dbo.Payments", new[] { "Partner_Id" });
            DropIndex("dbo.PaymentDetails", new[] { "Payment_Id" });
            DropIndex("dbo.PaymentDetails", new[] { "Order_Id" });
            DropIndex("dbo.Orders", new[] { "Partner_Id" });
            DropIndex("dbo.OrderDetails", new[] { "ProductPrice_Id" });
            DropIndex("dbo.OrderDetails", new[] { "Product_Id" });
            DropIndex("dbo.OrderDetails", new[] { "Order_Id" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.ProductPrices");
            DropTable("dbo.Products");
            DropTable("dbo.Payments");
            DropTable("dbo.PaymentDetails");
            DropTable("dbo.Partners");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Categories");
        }
    }
}
