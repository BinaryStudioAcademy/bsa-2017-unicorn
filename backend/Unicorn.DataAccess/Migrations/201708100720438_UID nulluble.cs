namespace Unicorn.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UIDnulluble : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SocialAccounts", "FacebookUID", c => c.Long());
            AlterColumn("dbo.SocialAccounts", "GoogleUID", c => c.Long());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SocialAccounts", "GoogleUID", c => c.Long(nullable: false));
            AlterColumn("dbo.SocialAccounts", "FacebookUID", c => c.Long(nullable: false));
        }
    }
}
