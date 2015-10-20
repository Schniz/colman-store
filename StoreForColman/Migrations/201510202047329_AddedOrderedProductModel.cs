namespace StoreForColman.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedOrderedProductModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderedProducts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        CurrentPriceInNIS = c.Int(nullable: false),
                        Product_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Products", t => t.Product_ID)
                .Index(t => t.Product_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderedProducts", "Product_ID", "dbo.Products");
            DropIndex("dbo.OrderedProducts", new[] { "Product_ID" });
            DropTable("dbo.OrderedProducts");
        }
    }
}
