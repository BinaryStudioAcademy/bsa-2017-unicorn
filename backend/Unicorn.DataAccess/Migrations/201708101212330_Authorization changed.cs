namespace Unicorn.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Authorizationchanged : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Accounts", "SocialAccount_Id", "dbo.SocialAccounts");
            DropIndex("dbo.Accounts", new[] { "SocialAccount_Id" });
            AddColumn("dbo.SocialAccounts", "Provider", c => c.String());
            AddColumn("dbo.SocialAccounts", "Uid", c => c.Long(nullable: false));
            AddColumn("dbo.SocialAccounts", "Account_Id", c => c.Long());
            CreateIndex("dbo.SocialAccounts", "Account_Id");
            AddForeignKey("dbo.SocialAccounts", "Account_Id", "dbo.Accounts", "Id");
            DropColumn("dbo.Accounts", "SocialAccount_Id");
            DropColumn("dbo.SocialAccounts", "FacebookUID");
            DropColumn("dbo.SocialAccounts", "GoogleUID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SocialAccounts", "GoogleUID", c => c.Long());
            AddColumn("dbo.SocialAccounts", "FacebookUID", c => c.Long());
            AddColumn("dbo.Accounts", "SocialAccount_Id", c => c.Long());
            DropForeignKey("dbo.SocialAccounts", "Account_Id", "dbo.Accounts");
            DropIndex("dbo.SocialAccounts", new[] { "Account_Id" });
            DropColumn("dbo.SocialAccounts", "Account_Id");
            DropColumn("dbo.SocialAccounts", "Uid");
            DropColumn("dbo.SocialAccounts", "Provider");
            CreateIndex("dbo.Accounts", "SocialAccount_Id");
            AddForeignKey("dbo.Accounts", "SocialAccount_Id", "dbo.SocialAccounts", "Id");
        }
    }
}
