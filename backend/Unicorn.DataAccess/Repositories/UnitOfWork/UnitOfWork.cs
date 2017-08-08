using System;
using System.Threading.Tasks;
using Unicorn.DataAccess.Context;
using Unicorn.DataAccess.Entities;
using Unicorn.DataAccess.Interfaces;

namespace Unicorn.DataAccess.Repositories.UnitOfWork
{
    class UnitOfWork : IDisposable, IUnitOfWork
    {
        private AppContext context;
        private IRepositoryFactory factory;
        private IGenericRepository<Account> accountRepository;
        private IGenericRepository<Book> bookRepository;
        private IGenericRepository<Category> categoryRepository;
        private IGenericRepository<Company> companyRepository;
        private IGenericRepository<Customer> customerRepository;
        private IGenericRepository<History> historyRepository;
        private IGenericRepository<Location> locationRepository;
        private IGenericRepository<Permission> permissionRepository;
        private IGenericRepository<Person> personRepository;
        private IGenericRepository<Review> reviewRepository;
        private IGenericRepository<Role> roleRepository;
        private IGenericRepository<Subcategory> subcategoryRepository;
        private IGenericRepository<Vendor> vendorRepository;
        private IGenericRepository<Work> workRepository;

        public UnitOfWork(IRepositoryFactory factory, AppContext context)
        {
            this.context = context;
            this.factory = factory;
        }

        public IGenericRepository<Account> AccountRepository
        {
            get { return accountRepository ??
                    (accountRepository = factory.CreateRepository<Account>(context)); }
        }

        public IGenericRepository<Book> BookRepository
        {
            get
            {
                return bookRepository ??
                  (bookRepository = factory.CreateRepository<Book>(context));
            }
        }

        public IGenericRepository<Category> CategoryRepository
        {
            get
            {
                return categoryRepository ??
                  (categoryRepository = factory.CreateRepository<Category>(context));
            }
        }

        public IGenericRepository<Company> CompanyRepository
        {
            get
            {
                return companyRepository ??
                  (companyRepository = factory.CreateRepository<Company>(context));
            }
        }

        public IGenericRepository<Customer> CustomerRepository
        {
            get
            {
                return customerRepository ??
                  (customerRepository = factory.CreateRepository<Customer>(context));
            }
        }

        public IGenericRepository<History> HistoryRepository
        {
            get
            {
                return historyRepository ??
                  (historyRepository = factory.CreateRepository<History>(context));
            }
        }

        public IGenericRepository<Location> LocationRepository
        {
            get
            {
                return locationRepository ??
                  (locationRepository = factory.CreateRepository<Location>(context));
            }
        }

        public IGenericRepository<Permission> PermissionRepository
        {
            get
            {
                return permissionRepository ??
                  (permissionRepository = factory.CreateRepository<Permission>(context));
            }
        }

        public IGenericRepository<Person> PersonRepository
        {
            get
            {
                return personRepository ??
                  (personRepository = factory.CreateRepository<Person>(context));
            }
        }

        public IGenericRepository<Review> ReviewRepository
        {
            get
            {
                return reviewRepository ??
                  (reviewRepository = factory.CreateRepository<Review>(context));
            }
        }

        public IGenericRepository<Role> RoleRepository
        {
            get
            {
                return roleRepository ??
                  (roleRepository = factory.CreateRepository<Role>(context));
            }
        }

        public IGenericRepository<Subcategory> SubcategoryRepository
        {
            get
            {
                return subcategoryRepository ??
                  (subcategoryRepository = factory.CreateRepository<Subcategory>(context));
            }
        }

        public IGenericRepository<Vendor> VendorRepository
        {
            get
            {
                return vendorRepository ??
                  (vendorRepository = factory.CreateRepository<Vendor>(context));
            }
        }

        public IGenericRepository<Work> WorkRepository
        {
            get
            {
                return workRepository ??
                  (workRepository = factory.CreateRepository<Work>(context));
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
