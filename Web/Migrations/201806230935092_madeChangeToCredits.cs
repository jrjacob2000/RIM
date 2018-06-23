namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class madeChangeToCredits : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Credits", "Partner_Id", "dbo.Partners");
            DropIndex("dbo.Credits", new[] { "Partner_Id" });
            AddColumn("dbo.Credits", "Partner_Id1", c => c.Guid());
            CreateIndex("dbo.Credits", "Partner_Id1");
            AddForeignKey("dbo.Credits", "Partner_Id1", "dbo.Partners", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Credits", "Partner_Id1", "dbo.Partners");
            DropIndex("dbo.Credits", new[] { "Partner_Id1" });
            DropColumn("dbo.Credits", "Partner_Id1");
            CreateIndex("dbo.Credits", "Partner_Id");
            AddForeignKey("dbo.Credits", "Partner_Id", "dbo.Partners", "Id");
        }
    }
}
