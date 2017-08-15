namespace Unicorn.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateReviewEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reviews", "Avatar", c => c.String());
            AddColumn("dbo.Reviews", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reviews", "Date");
            DropColumn("dbo.Reviews", "Avatar");
        }
    }
}
