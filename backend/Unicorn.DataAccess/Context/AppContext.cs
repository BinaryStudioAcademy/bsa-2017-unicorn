using System.Data.Entity;

using Unicorn.DataAccess.Entities;

namespace Unicorn.DataAccess.Context
{
    public class AppContext: DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Work> Works { get; set; }
        public DbSet<SocialAccount> SocialAccounts { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactProvider> ContactProviders { get; set; }
        public DbSet<PortfolioItem> PortfolioItems { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public AppContext() : base("DefaultConnection")
        {
            Database.SetInitializer(new UnicornDbInitializer());
            Database.Initialize(true);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Role>()
                .HasMany<Permission>(r => r.Permissions)
                .WithMany(p => p.Roles)
                .Map(cs =>
                {
                    cs.MapLeftKey("RoleId");
                    cs.MapRightKey("PermissionId");
                    cs.ToTable("RoleAndPermission");
                });
            modelBuilder.Entity<Person>()
               .Property(f => f.Birthday)
               .HasColumnType("datetime2");

        }
    }
}
