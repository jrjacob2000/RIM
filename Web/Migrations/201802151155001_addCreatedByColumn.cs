namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCreatedByColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "CreatedBy", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "CreatedBy");
        }
    }
}
