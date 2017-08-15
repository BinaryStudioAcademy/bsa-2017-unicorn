namespace Unicorn.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class configure_many_to_many_roles_to_permissions : DbMigration
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
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.Role_Id)
                .Index(t => t.Role_Id);
            
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
                "dbo.Permissions",
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
                        Provider = c.String(),
                        Uid = c.String(),
                        Account_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.Account_Id)
                .Index(t => t.Account_Id);
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Status = c.String(),
                        Description = c.String(),
                        Company_Id = c.Long(),
                        Customer_Id = c.Long(),
                        Location_Id = c.Long(),
                        Vendor_Id = c.Long(),
                        Work_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.Company_Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .ForeignKey("dbo.Locations", t => t.Location_Id)
                .ForeignKey("dbo.Vendors", t => t.Vendor_Id)
                .ForeignKey("dbo.Works", t => t.Work_Id)
                .Index(t => t.Company_Id)
                .Index(t => t.Customer_Id)
                .Index(t => t.Location_Id)
                .Index(t => t.Vendor_Id)
                .Index(t => t.Work_Id);
            
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
                "dbo.RoleAndPermission",
                c => new
                    {
                        RoleId = c.Long(nullable: false),
                        PermissionId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.RoleId, t.PermissionId })
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Permissions", t => t.PermissionId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.PermissionId);
            
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
            DropForeignKey("dbo.Books", "Location_Id", "dbo.Locations");
            DropForeignKey("dbo.Customers", "Person_Id", "dbo.People");
            DropForeignKey("dbo.Books", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.Books", "Company_Id", "dbo.Companies");
            DropForeignKey("dbo.WorkVendors", "Vendor_Id", "dbo.Vendors");
            DropForeignKey("dbo.WorkVendors", "Work_Id", "dbo.Works");
            DropForeignKey("dbo.Works", "Subcategory_Id", "dbo.Subcategories");
            DropForeignKey("dbo.Subcategories", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.Vendors", "Person_Id", "dbo.People");
            DropForeignKey("dbo.People", "Location_Id", "dbo.Locations");
            DropForeignKey("dbo.People", "Account_Id", "dbo.Accounts");
            DropForeignKey("dbo.Vendors", "Company_Id", "dbo.Companies");
            DropForeignKey("dbo.Companies", "Location_Id", "dbo.Locations");
            DropForeignKey("dbo.Companies", "Account_Id", "dbo.Accounts");
            DropForeignKey("dbo.SocialAccounts", "Account_Id", "dbo.Accounts");
            DropForeignKey("dbo.Accounts", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.RoleAndPermission", "PermissionId", "dbo.Permissions");
            DropForeignKey("dbo.RoleAndPermission", "RoleId", "dbo.Roles");
            DropIndex("dbo.WorkVendors", new[] { "Vendor_Id" });
            DropIndex("dbo.WorkVendors", new[] { "Work_Id" });
            DropIndex("dbo.RoleAndPermission", new[] { "PermissionId" });
            DropIndex("dbo.RoleAndPermission", new[] { "RoleId" });
            DropIndex("dbo.Customers", new[] { "Person_Id" });
            DropIndex("dbo.Subcategories", new[] { "Category_Id" });
            DropIndex("dbo.Works", new[] { "Subcategory_Id" });
            DropIndex("dbo.People", new[] { "Location_Id" });
            DropIndex("dbo.People", new[] { "Account_Id" });
            DropIndex("dbo.Vendors", new[] { "Person_Id" });
            DropIndex("dbo.Vendors", new[] { "Company_Id" });
            DropIndex("dbo.Companies", new[] { "Location_Id" });
            DropIndex("dbo.Companies", new[] { "Account_Id" });
            DropIndex("dbo.Books", new[] { "Work_Id" });
            DropIndex("dbo.Books", new[] { "Vendor_Id" });
            DropIndex("dbo.Books", new[] { "Location_Id" });
            DropIndex("dbo.Books", new[] { "Customer_Id" });
            DropIndex("dbo.Books", new[] { "Company_Id" });
            DropIndex("dbo.SocialAccounts", new[] { "Account_Id" });
            DropIndex("dbo.Accounts", new[] { "Role_Id" });
            DropTable("dbo.WorkVendors");
            DropTable("dbo.RoleAndPermission");
            DropTable("dbo.Reviews");
            DropTable("dbo.Histories");
            DropTable("dbo.Customers");
            DropTable("dbo.Categories");
            DropTable("dbo.Subcategories");
            DropTable("dbo.Works");
            DropTable("dbo.People");
            DropTable("dbo.Vendors");
            DropTable("dbo.Locations");
            DropTable("dbo.Companies");
            DropTable("dbo.Books");
            DropTable("dbo.SocialAccounts");
            DropTable("dbo.Permissions");
            DropTable("dbo.Roles");
            DropTable("dbo.Accounts");
        }
    }
}
