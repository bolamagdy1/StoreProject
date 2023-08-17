namespace StoreProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "Product_Name", c => c.String(nullable: false));
            AlterColumn("dbo.Spaces", "Space_Name", c => c.String(nullable: false));
            AlterColumn("dbo.Stores", "Store_Name", c => c.String(nullable: false));
            AlterColumn("dbo.Stores", "Address", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Stores", "Address", c => c.String());
            AlterColumn("dbo.Stores", "Store_Name", c => c.String());
            AlterColumn("dbo.Spaces", "Space_Name", c => c.String());
            AlterColumn("dbo.Products", "Product_Name", c => c.String());
        }
    }
}
