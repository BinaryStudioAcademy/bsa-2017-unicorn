namespace Unicorn.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCompanyintoBook : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "Company_Id", c => c.Long());
            CreateIndex("dbo.Books", "Company_Id");
            AddForeignKey("dbo.Books", "Company_Id", "dbo.Companies", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "Company_Id", "dbo.Companies");
            DropIndex("dbo.Books", new[] { "Company_Id" });
            DropColumn("dbo.Books", "Company_Id");
        }
    }
}
