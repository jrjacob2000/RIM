namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class productAddColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "CreatedBy", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "CreatedBy");
        }
    }
}
