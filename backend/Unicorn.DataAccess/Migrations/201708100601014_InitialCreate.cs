namespace Unicorn.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        Avatar = c.String(),
                        Rating = c.Int(nullable: false),
                        Role_Id = c.Long(),
                        SocialAccount_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.Role_Id)
                .ForeignKey("dbo.SocialAccounts", t => t.SocialAccount_Id)
                .Index(t => t.Role_Id)
                .Index(t => t.SocialAccount_Id);
            
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SocialAccounts",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false),
                        FacebookUID = c.Long(nullable: false),
                        GoogleUID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Status = c.String(),
                        Description = c.String(),
                        Customer_Id = c.Long(),
                        Location_Id = c.Long(),
                        Vendor_Id = c.Long(),
                        Work_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .ForeignKey("dbo.Locations", t => t.Location_Id)
                .ForeignKey("dbo.Vendors", t => t.Vendor_Id)
                .ForeignKey("dbo.Works", t => t.Work_Id)
                .Index(t => t.Customer_Id)
                .Index(t => t.Location_Id)
                .Index(t => t.Vendor_Id)
                .Index(t => t.Work_Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false),
                        Person_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.Person_Id)
                .Index(t => t.Person_Id);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false),
                        Birthday = c.DateTime(nullable: false),
                        Name = c.String(),
                        SurnameName = c.String(),
                        MiddleName = c.String(),
                        Gender = c.String(),
                        Phone = c.String(),
                        Account_Id = c.Long(),
                        Location_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.Account_Id)
                .ForeignKey("dbo.Locations", t => t.Location_Id)
                .Index(t => t.Account_Id)
                .Index(t => t.Location_Id);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false),
                        City = c.String(),
                        Adress = c.String(),
                        PostIndex = c.String(),
                        CoordinateX = c.Double(nullable: false),
                        CoordinateY = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Vendors",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false),
                        Experience = c.Double(nullable: false),
                        ExWork = c.String(),
                        Position = c.String(),
                        Company_Id = c.Long(),
                        Person_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.Company_Id)
                .ForeignKey("dbo.People", t => t.Person_Id)
                .Index(t => t.Company_Id)
                .Index(t => t.Person_Id);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false),
                        FoundationDate = c.DateTime(nullable: false),
                        Staff = c.Int(nullable: false),
                        Account_Id = c.Long(),
                        Location_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.Account_Id)
                .ForeignKey("dbo.Locations", t => t.Location_Id)
                .Index(t => t.Account_Id)
                .Index(t => t.Location_Id);
            
            CreateTable(
                "dbo.Works",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        Subcategory_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Subcategories", t => t.Subcategory_Id)
                .Index(t => t.Subcategory_Id);
            
            CreateTable(
                "dbo.Subcategories",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        Category_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id)
                .Index(t => t.Category_Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Histories",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false),
                        CustomerId = c.Long(nullable: false),
                        CustomerName = c.String(),
                        VendorId = c.Long(nullable: false),
                        VendorName = c.String(),
                        Date = c.DateTime(nullable: false),
                        BookDescription = c.String(),
                        WorkDescription = c.String(),
                        CategoryName = c.String(),
                        SubcategoryName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false),
                        From = c.String(),
                        To = c.String(),
                        Grade = c.Double(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PermissionAccounts",
                c => new
                    {
                        Permission_Id = c.Long(nullable: false),
                        Account_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Permission_Id, t.Account_Id })
                .ForeignKey("dbo.Permissions", t => t.Permission_Id, cascadeDelete: true)
                .ForeignKey("dbo.Accounts", t => t.Account_Id, cascadeDelete: true)
                .Index(t => t.Permission_Id)
                .Index(t => t.Account_Id);
            
            CreateTable(
                "dbo.WorkVendors",
                c => new
                    {
                        Work_Id = c.Long(nullable: false),
                        Vendor_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Work_Id, t.Vendor_Id })
                .ForeignKey("dbo.Works", t => t.Work_Id, cascadeDelete: true)
                .ForeignKey("dbo.Vendors", t => t.Vendor_Id, cascadeDelete: true)
                .Index(t => t.Work_Id)
                .Index(t => t.Vendor_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "Work_Id", "dbo.Works");
            DropForeignKey("dbo.Books", "Vendor_Id", "dbo.Vendors");
            DropForeignKey("dbo.WorkVendors", "Vendor_Id", "dbo.Vendors");
            DropForeignKey("dbo.WorkVendors", "Work_Id", "dbo.Works");
            DropForeignKey("dbo.Works", "Subcategory_Id", "dbo.Subcategories");
            DropForeignKey("dbo.Subcategories", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.Vendors", "Person_Id", "dbo.People");
            DropForeignKey("dbo.Vendors", "Company_Id", "dbo.Companies");
            DropForeignKey("dbo.Companies", "Location_Id", "dbo.Locations");
            DropForeignKey("dbo.Companies", "Account_Id", "dbo.Accounts");
            DropForeignKey("dbo.Books", "Location_Id", "dbo.Locations");
            DropForeignKey("dbo.Customers", "Person_Id", "dbo.People");
            DropForeignKey("dbo.People", "Location_Id", "dbo.Locations");
            DropForeignKey("dbo.People", "Account_Id", "dbo.Accounts");
            DropForeignKey("dbo.Books", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.Accounts", "SocialAccount_Id", "dbo.SocialAccounts");
            DropForeignKey("dbo.Accounts", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.PermissionAccounts", "Account_Id", "dbo.Accounts");
            DropForeignKey("dbo.PermissionAccounts", "Permission_Id", "dbo.Permissions");
            DropIndex("dbo.WorkVendors", new[] { "Vendor_Id" });
            DropIndex("dbo.WorkVendors", new[] { "Work_Id" });
            DropIndex("dbo.PermissionAccounts", new[] { "Account_Id" });
            DropIndex("dbo.PermissionAccounts", new[] { "Permission_Id" });
            DropIndex("dbo.Subcategories", new[] { "Category_Id" });
            DropIndex("dbo.Works", new[] { "Subcategory_Id" });
            DropIndex("dbo.Companies", new[] { "Location_Id" });
            DropIndex("dbo.Companies", new[] { "Account_Id" });
            DropIndex("dbo.Vendors", new[] { "Person_Id" });
            DropIndex("dbo.Vendors", new[] { "Company_Id" });
            DropIndex("dbo.People", new[] { "Location_Id" });
            DropIndex("dbo.People", new[] { "Account_Id" });
            DropIndex("dbo.Customers", new[] { "Person_Id" });
            DropIndex("dbo.Books", new[] { "Work_Id" });
            DropIndex("dbo.Books", new[] { "Vendor_Id" });
            DropIndex("dbo.Books", new[] { "Location_Id" });
            DropIndex("dbo.Books", new[] { "Customer_Id" });
            DropIndex("dbo.Accounts", new[] { "SocialAccount_Id" });
            DropIndex("dbo.Accounts", new[] { "Role_Id" });
            DropTable("dbo.WorkVendors");
            DropTable("dbo.PermissionAccounts");
            DropTable("dbo.Reviews");
            DropTable("dbo.Histories");
            DropTable("dbo.Categories");
            DropTable("dbo.Subcategories");
            DropTable("dbo.Works");
            DropTable("dbo.Companies");
            DropTable("dbo.Vendors");
            DropTable("dbo.Locations");
            DropTable("dbo.People");
            DropTable("dbo.Customers");
            DropTable("dbo.Books");
            DropTable("dbo.SocialAccounts");
            DropTable("dbo.Roles");
            DropTable("dbo.Permissions");
            DropTable("dbo.Accounts");
        }
    }
}
