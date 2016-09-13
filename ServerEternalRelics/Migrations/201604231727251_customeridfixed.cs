namespace ServerEternalRelics.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class customeridfixed : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Order", "Customer_CustomerID", "dbo.Customer");
            DropIndex("dbo.Order", new[] { "Customer_CustomerID" });
            RenameColumn(table: "dbo.Order", name: "Customer_CustomerID", newName: "CustomerID");
            AlterColumn("dbo.Order", "CustomerID", c => c.Int(nullable: false));
            CreateIndex("dbo.Order", "CustomerID");
            AddForeignKey("dbo.Order", "CustomerID", "dbo.Customer", "CustomerID", cascadeDelete: true);
            DropColumn("dbo.Order", "KlantID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Order", "KlantID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Order", "CustomerID", "dbo.Customer");
            DropIndex("dbo.Order", new[] { "CustomerID" });
            AlterColumn("dbo.Order", "CustomerID", c => c.Int());
            RenameColumn(table: "dbo.Order", name: "CustomerID", newName: "Customer_CustomerID");
            CreateIndex("dbo.Order", "Customer_CustomerID");
            AddForeignKey("dbo.Order", "Customer_CustomerID", "dbo.Customer", "CustomerID");
        }
    }
}
