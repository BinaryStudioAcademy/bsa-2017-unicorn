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

        public IGenericRepository<Account> AccountRepository => accountRepository ??
                       (accountRepository = factory.CreateRepository<Account>(context));

        public IGenericRepository<Book> BookRepository => bookRepository ??
                  (bookRepository = factory.CreateRepository<Book>(context));

        public IGenericRepository<Calendar> CalendarRepository => calendarRepository ??
                       (calendarRepository = factory.CreateRepository<Calendar>(context));

        public IGenericRepository<Category> CategoryRepository => categoryRepository ??
                  (categoryRepository = factory.CreateRepository<Category>(context));

        public IGenericRepository<Company> CompanyRepository => companyRepository ??
                  (companyRepository = factory.CreateRepository<Company>(context));

        public IGenericRepository<Customer> CustomerRepository => customerRepository ??
                  (customerRepository = factory.CreateRepository<Customer>(context));

        public IGenericRepository<ChatDialog> ChatDialogRepository => chatDialogRepository ??
                  (chatDialogRepository = factory.CreateRepository<ChatDialog>(context));

        public IGenericRepository<ChatFile> ChatFileRepository => chatFileRepository ??
                  (chatFileRepository = factory.CreateRepository<ChatFile>(context));

        public IGenericRepository<ChatMessage> ChatMessageRepository => chatMessageRepository ??
                  (chatMessageRepository = factory.CreateRepository<ChatMessage>(context));

        public IGenericRepository<ExtraDay> ExtraDayRepository => extraDayRepository ??
                       (extraDayRepository = factory.CreateRepository<ExtraDay>(context));

        public IGenericRepository<History> HistoryRepository => historyRepository ??
                  (historyRepository = factory.CreateRepository<History>(context));

        public IGenericRepository<Location> LocationRepository => locationRepository ??
                  (locationRepository = factory.CreateRepository<Location>(context));

        public IGenericRepository<Permission> PermissionRepository => permissionRepository ??
                  (permissionRepository = factory.CreateRepository<Permission>(context));

        public IGenericRepository<Person> PersonRepository => personRepository ??
                  (personRepository = factory.CreateRepository<Person>(context));

        public IGenericRepository<Review> ReviewRepository => reviewRepository ??
                  (reviewRepository = factory.CreateRepository<Review>(context));

        public IGenericRepository<Role> RoleRepository => roleRepository ??
                  (roleRepository = factory.CreateRepository<Role>(context));

        public IGenericRepository<Subcategory> SubcategoryRepository => subcategoryRepository ??
                  (subcategoryRepository = factory.CreateRepository<Subcategory>(context));

        public IGenericRepository<Vendor> VendorRepository => vendorRepository ??
                  (vendorRepository = factory.CreateRepository<Vendor>(context));

        public IGenericRepository<Work> WorkRepository => workRepository ??
                  (workRepository = factory.CreateRepository<Work>(context));

        public IGenericRepository<SocialAccount> SocialAccountRepository => socialAccountRepository ??
                  (socialAccountRepository = factory.CreateRepository<SocialAccount>(context));

        public IGenericRepository<PortfolioItem> PortfolioRepository => portfolioRepository ??
                    (portfolioRepository = factory.CreateRepository<PortfolioItem>(context));

        public IGenericRepository<Contact> ContactRepository => contactRepository ??
                    (contactRepository = factory.CreateRepository<Contact>(context));

        public IGenericRepository<ContactProvider> ContactProviderRepository => contactProviderRepository ??
                    (contactProviderRepository = factory.CreateRepository<ContactProvider>(context));

        public IGenericRepository<Rating> RatingRepository => ratingRepository ??
                    (ratingRepository = factory.CreateRepository<Rating>(context));

        public IGenericRepository<Notification> NotificationRepository => notificationRepository ??
                    (notificationRepository = factory.CreateRepository<Notification>(context));

        public IGenericRepository<Offer> OfferRepository => offerRepository ??
                    (offerRepository = factory.CreateRepository<Offer>(context));

        public IGenericRepository<Report> ReportRepository => reportRepository ??
                    (reportRepository = factory.CreateRepository<Report>(context));

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

        public async Task SaveAsync() => await context.SaveChangesAsync();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
