namespace StoreForColman.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRelationshipsToOrderModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderedProducts", "Order_ID", c => c.Int());
            AddColumn("dbo.Orders", "CreatedAt", c => c.DateTime(nullable: false));
            CreateIndex("dbo.OrderedProducts", "Order_ID");
            AddForeignKey("dbo.OrderedProducts", "Order_ID", "dbo.Orders", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderedProducts", "Order_ID", "dbo.Orders");
            DropIndex("dbo.OrderedProducts", new[] { "Order_ID" });
            DropColumn("dbo.Orders", "CreatedAt");
            DropColumn("dbo.OrderedProducts", "Order_ID");
        }
    }
}
