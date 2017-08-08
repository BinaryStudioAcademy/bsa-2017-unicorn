using System;
using System.Threading.Tasks;
using Unicorn.DataAccess.Context;
using Unicorn.DataAccess.Entities;

namespace Unicorn.DataAccess.Repositories.UnitOfWork
{
    class UnitOfWork : IDisposable
    {
        private AppContext context;
        private GenericRepository<Account> accountRepository;
        private GenericRepository<Book> bookRepository;
        private GenericRepository<Category> categoryRepository;
        private GenericRepository<Company> companyRepository;
        private GenericRepository<Customer> customerRepository;
        private GenericRepository<History> historyRepository;
        private GenericRepository<Location> locationRepository;
        private GenericRepository<Permission> permissionRepository;
        private GenericRepository<Person> personRepository;
        private GenericRepository<Review> reviewRepository;
        private GenericRepository<Role> roleRepository;
        private GenericRepository<Subcategory> subcategoryRepository;
        private GenericRepository<Vendor> vendorRepository;
        private GenericRepository<Work> workRepository;

        public UnitOfWork()
        {
            context = new AppContext("DefaultConnection");
        }
        public UnitOfWork(string connectionString)
        {
            context = new AppContext(connectionString);
        }

        public GenericRepository<Account> AccountRepository
        {
            get
            {
                if (this.accountRepository == null)
                {
                    this.accountRepository = new GenericRepository<Account>(context);
                }
                return accountRepository;
            }
        }

        public GenericRepository<Book> BookRepository
        {
            get
            {
                if (this.bookRepository == null)
                {
                    this.bookRepository = new GenericRepository<Book>(context);
                }
                return bookRepository;
            }
        }

        public GenericRepository<Category> CategoryRepository
        {
            get
            {
                if (this.categoryRepository == null)
                {
                    this.categoryRepository = new GenericRepository<Category>(context);
                }
                return categoryRepository;
            }
        }

        public GenericRepository<Company> CompanyRepository
        {
            get
            {
                if (this.companyRepository == null)
                {
                    this.companyRepository = new GenericRepository<Company>(context);
                }
                return companyRepository;
            }
        }

        public GenericRepository<Customer> CustomerRepository
        {
            get
            {
                if (this.customerRepository == null)
                {
                    this.customerRepository = new GenericRepository<Customer>(context);
                }
                return customerRepository;
            }
        }

        public GenericRepository<History> HistoryRepository
        {
            get
            {
                if (this.historyRepository == null)
                {
                    this.historyRepository = new GenericRepository<History>(context);
                }
                return historyRepository;
            }
        }

        public GenericRepository<Location> LocationRepository
        {
            get
            {
                if (this.locationRepository == null)
                {
                    this.locationRepository = new GenericRepository<Location>(context);
                }
                return locationRepository;
            }
        }

        public GenericRepository<Permission> PermissionRepository
        {
            get
            {
                if (this.permissionRepository == null)
                {
                    this.permissionRepository = new GenericRepository<Permission>(context);
                }
                return permissionRepository;
            }
        }

        public GenericRepository<Person> PersonRepository
        {
            get
            {
                if (this.personRepository == null)
                {
                    this.personRepository = new GenericRepository<Person>(context);
                }
                return personRepository;
            }
        }

        public GenericRepository<Review> ReviewRepository
        {
            get
            {
                if (this.reviewRepository == null)
                {
                    this.reviewRepository = new GenericRepository<Review>(context);
                }
                return reviewRepository;
            }
        }

        public GenericRepository<Role> RoleRepository
        {
            get
            {
                if (this.roleRepository == null)
                {
                    this.roleRepository = new GenericRepository<Role>(context);
                }
                return roleRepository;
            }
        }

        public GenericRepository<Subcategory> SubcategoryRepository
        {
            get
            {
                if (this.subcategoryRepository == null)
                {
                    this.subcategoryRepository = new GenericRepository<Subcategory>(context);
                }
                return subcategoryRepository;
            }
        }

        public GenericRepository<Vendor> VendorRepository
        {
            get
            {
                if (this.vendorRepository == null)
                {
                    this.vendorRepository = new GenericRepository<Vendor>(context);
                }
                return vendorRepository;
            }
        }

        public GenericRepository<Work> WorkRepository
        {
            get
            {
                if (this.workRepository == null)
                {
                    this.workRepository = new GenericRepository<Work>(context);
                }
                return workRepository;
            }
        }

        private bool disposed;

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
                disposed = true;
            }
        }
        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
