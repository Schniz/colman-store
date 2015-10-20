namespace StoreForColman.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedOrderModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Orders", new[] { "User_Id" });
            DropTable("dbo.Orders");
        }
    }
}
