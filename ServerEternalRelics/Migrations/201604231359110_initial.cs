namespace ServerEternalRelics.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        Image = c.String(),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        Image = c.String(),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(nullable: false, maxLength: 500),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ProductID);
            
            CreateTable(
                "dbo.Offer",
                c => new
                    {
                        OfferID = c.Int(nullable: false, identity: true),
                        OfferPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DateFrom = c.DateTime(nullable: false),
                        DateTill = c.DateTime(nullable: false),
                        OfferText = c.String(nullable: false, maxLength: 500),
                        ProductID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OfferID)
                .ForeignKey("dbo.Product", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        CustomerID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Address = c.String(nullable: false),
                        Zipcode = c.String(nullable: false),
                        Residence = c.String(nullable: false, maxLength: 50),
                        EmailAdres = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CustomerID);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        KlantID = c.Int(nullable: false),
                        Customer_CustomerID = c.Int(),
                    })
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("dbo.Customer", t => t.Customer_CustomerID)
                .Index(t => t.Customer_CustomerID);
            
            CreateTable(
                "dbo.OrderLine",
                c => new
                    {
                        OrderLineID = c.Int(nullable: false, identity: true),
                        Amount = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        OrderID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderLineID)
                .ForeignKey("dbo.Order", t => t.OrderID, cascadeDelete: true)
                .ForeignKey("dbo.Product", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ProductID)
                .Index(t => t.OrderID);
            
            CreateTable(
                "dbo.ProductCategory",
                c => new
                    {
                        CategoryID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CategoryID, t.ProductID })
                .ForeignKey("dbo.Category", t => t.CategoryID, cascadeDelete: true)
                .ForeignKey("dbo.Product", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.CategoryID)
                .Index(t => t.ProductID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderLine", "ProductID", "dbo.Product");
            DropForeignKey("dbo.OrderLine", "OrderID", "dbo.Order");
            DropForeignKey("dbo.Order", "Customer_CustomerID", "dbo.Customer");
            DropForeignKey("dbo.ProductCategory", "ProductID", "dbo.Product");
            DropForeignKey("dbo.ProductCategory", "CategoryID", "dbo.Category");
            DropForeignKey("dbo.Offer", "ProductID", "dbo.Product");
            DropIndex("dbo.ProductCategory", new[] { "ProductID" });
            DropIndex("dbo.ProductCategory", new[] { "CategoryID" });
            DropIndex("dbo.OrderLine", new[] { "OrderID" });
            DropIndex("dbo.OrderLine", new[] { "ProductID" });
            DropIndex("dbo.Order", new[] { "Customer_CustomerID" });
            DropIndex("dbo.Offer", new[] { "ProductID" });
            DropTable("dbo.ProductCategory");
            DropTable("dbo.OrderLine");
            DropTable("dbo.Order");
            DropTable("dbo.Customer");
            DropTable("dbo.Offer");
            DropTable("dbo.Product");
            DropTable("dbo.Category");
        }
    }
}
