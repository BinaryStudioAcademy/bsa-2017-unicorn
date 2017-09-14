using System;
using System.Threading.Tasks;
using Unicorn.DataAccess.Context;
using Unicorn.DataAccess.Entities;
using Unicorn.DataAccess.Interfaces;

namespace Unicorn.DataAccess.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppContext context;
        private IRepositoryFactory factory;

        private IGenericRepository<Account> accountRepository;
        private IGenericRepository<Book> bookRepository;
        private IGenericRepository<Calendar> calendarRepository;
        private IGenericRepository<Category> categoryRepository;
        private IGenericRepository<Company> companyRepository;
        private IGenericRepository<Customer> customerRepository;
        private IGenericRepository<ChatDialog> chatDialogRepository;
        private IGenericRepository<ChatFile> chatFileRepository;
        private IGenericRepository<ChatMessage> chatMessageRepository;
        private IGenericRepository<ExtraDay> extraDayRepository;
        private IGenericRepository<History> historyRepository;
        private IGenericRepository<Location> locationRepository;
        private IGenericRepository<Permission> permissionRepository;
        private IGenericRepository<Person> personRepository;
        private IGenericRepository<Review> reviewRepository;
        private IGenericRepository<Role> roleRepository;
        private IGenericRepository<Subcategory> subcategoryRepository;
        private IGenericRepository<Vendor> vendorRepository;
        private IGenericRepository<Work> workRepository;
        private IGenericRepository<SocialAccount> socialAccountRepository;
        private IGenericRepository<PortfolioItem> portfolioRepository;
        private IGenericRepository<Contact> contactRepository;
        private IGenericRepository<ContactProvider> contactProviderRepository;
        private IGenericRepository<Rating> ratingRepository;
        private IGenericRepository<Notification> notificationRepository;
        private IGenericRepository<Offer> offerRepository;
        private IGenericRepository<Report> reportRepository;

        public UnitOfWork(AppContext context, IRepositoryFactory factory)
        {
            this.context = context;
            this.factory = factory;
        }

        public IGenericRepository<Account> AccountRepository
        {
            get
            {
                return accountRepository ??
                       (accountRepository = factory.CreateRepository<Account>(context));
            }
        }


        public IGenericRepository<Book> BookRepository
        {
            get
            {
                return bookRepository ??
                  (bookRepository = factory.CreateRepository<Book>(context));
            }
        }

        public IGenericRepository<Calendar> CalendarRepository
        {
            get
            {
                return calendarRepository ??
                       (calendarRepository = factory.CreateRepository<Calendar>(context));
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

        public IGenericRepository<ChatDialog> ChatDialogRepository
        {
            get
            {
                return chatDialogRepository ??
                  (chatDialogRepository = factory.CreateRepository<ChatDialog>(context));
            }
        }

        public IGenericRepository<ChatFile> ChatFileRepository
        {
            get
            {
                return chatFileRepository ??
                  (chatFileRepository = factory.CreateRepository<ChatFile>(context));
            }
        }

        public IGenericRepository<ChatMessage> ChatMessageRepository
        {
            get
            {
                return chatMessageRepository ??
                  (chatMessageRepository = factory.CreateRepository<ChatMessage>(context));
            }
        }

        public IGenericRepository<ExtraDay> ExtraDayRepository
        {
            get
            {
                return extraDayRepository ??
                       (extraDayRepository = factory.CreateRepository<ExtraDay>(context));
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

        public IGenericRepository<SocialAccount> SocialAccountRepository
        {
            get
            {
                return socialAccountRepository ??
                  (socialAccountRepository = factory.CreateRepository<SocialAccount>(context));
            }
        }

        public IGenericRepository<PortfolioItem> PortfolioRepository
        {
            get
            {
                return portfolioRepository ??
                    (portfolioRepository = factory.CreateRepository<PortfolioItem>(context));
            }
        }

        public IGenericRepository<Contact> ContactRepository
        {
            get
            {
                return contactRepository ??
                    (contactRepository = factory.CreateRepository<Contact>(context));
            }
        }

        public IGenericRepository<ContactProvider> ContactProviderRepository
        {
            get
            {
                return contactProviderRepository ??
                    (contactProviderRepository = factory.CreateRepository<ContactProvider>(context));
            }
        }

        public IGenericRepository<Rating> RatingRepository
        {
            get
            {
                return ratingRepository ??
                    (ratingRepository = factory.CreateRepository<Rating>(context));
            }
        }

        public IGenericRepository<Notification> NotificationRepository
        {
            get
            {
                return notificationRepository ??
                    (notificationRepository = factory.CreateRepository<Notification>(context));
            }
        }

        public IGenericRepository<Offer> OfferRepository
        {
            get
            {
                return offerRepository ??
                    (offerRepository = factory.CreateRepository<Offer>(context));
            }
        }

        public IGenericRepository<Report> ReportRepository
        {
            get
            {
                return reportRepository ??
                    (reportRepository = factory.CreateRepository<Report>(context));
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
