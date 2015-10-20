namespace StoreForColman.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangePriceToDoubleInOrderedProduct : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.OrderedProducts", "CurrentPriceInNIS", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OrderedProducts", "CurrentPriceInNIS", c => c.Int(nullable: false));
        }
    }
}
