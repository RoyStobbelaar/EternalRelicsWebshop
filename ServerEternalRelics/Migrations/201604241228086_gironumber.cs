namespace ServerEternalRelics.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gironumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "GiroNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order", "GiroNumber");
        }
    }
}
