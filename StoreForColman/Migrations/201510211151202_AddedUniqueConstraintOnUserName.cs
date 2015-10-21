namespace StoreForColman.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUniqueConstraintOnUserName : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.AspNetUsers", "UserName", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.AspNetUsers", new[] { "UserName" });
        }
    }
}
