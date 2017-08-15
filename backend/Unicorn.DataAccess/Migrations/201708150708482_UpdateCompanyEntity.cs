namespace Unicorn.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCompanyEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Companies", "Name", c => c.String());
            AddColumn("dbo.Companies", "Description", c => c.String());
            AddColumn("dbo.Companies", "Director_Id", c => c.Long());
            AlterColumn("dbo.Accounts", "Rating", c => c.Double(nullable: false));
            CreateIndex("dbo.Companies", "Director_Id");
            AddForeignKey("dbo.Companies", "Director_Id", "dbo.Accounts", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Companies", "Director_Id", "dbo.Accounts");
            DropIndex("dbo.Companies", new[] { "Director_Id" });
            AlterColumn("dbo.Accounts", "Rating", c => c.Int(nullable: false));
            DropColumn("dbo.Companies", "Director_Id");
            DropColumn("dbo.Companies", "Description");
            DropColumn("dbo.Companies", "Name");
        }
    }
}
