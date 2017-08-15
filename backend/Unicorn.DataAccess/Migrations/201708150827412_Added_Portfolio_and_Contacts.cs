namespace Unicorn.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_Portfolio_and_Contacts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Type = c.String(),
                        Value = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PortfolioItems",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Image = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        HistoryEntry_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Histories", t => t.HistoryEntry_Id)
                .Index(t => t.HistoryEntry_Id);
            
            AddColumn("dbo.Vendors", "WorkLetter", c => c.String());
            AddColumn("dbo.People", "Surname", c => c.String());
            DropColumn("dbo.People", "SurnameName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.People", "SurnameName", c => c.String());
            DropForeignKey("dbo.PortfolioItems", "HistoryEntry_Id", "dbo.Histories");
            DropIndex("dbo.PortfolioItems", new[] { "HistoryEntry_Id" });
            DropColumn("dbo.People", "Surname");
            DropColumn("dbo.Vendors", "WorkLetter");
            DropTable("dbo.PortfolioItems");
            DropTable("dbo.Contacts");
        }
    }
}
