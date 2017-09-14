using System;
using System.Threading.Tasks;
using Unicorn.DataAccess.Entities;

namespace Unicorn.DataAccess.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Account> AccountRepository { get; }
        IGenericRepository<Book> BookRepository { get; }
        IGenericRepository<Calendar> CalendarRepository { get; }
        IGenericRepository<Category> CategoryRepository { get; }
        IGenericRepository<Company> CompanyRepository { get; }
        IGenericRepository<Customer> CustomerRepository { get; }
        IGenericRepository<ChatDialog> ChatDialogRepository { get; }
        IGenericRepository<ChatFile> ChatFileRepository { get; }
        IGenericRepository<ChatMessage> ChatMessageRepository { get; }
        IGenericRepository<ExtraDay> ExtraDayRepository { get; }
        IGenericRepository<History> HistoryRepository { get; }
        IGenericRepository<Location> LocationRepository { get; }
        IGenericRepository<Permission> PermissionRepository { get; }
        IGenericRepository<Person> PersonRepository { get; }
        IGenericRepository<Review> ReviewRepository { get; }
        IGenericRepository<Role> RoleRepository { get; }
        IGenericRepository<Subcategory> SubcategoryRepository { get; }
        IGenericRepository<Vendor> VendorRepository { get; }
        IGenericRepository<Work> WorkRepository { get; }
        IGenericRepository<SocialAccount> SocialAccountRepository { get; }
        IGenericRepository<PortfolioItem> PortfolioRepository { get; }
        IGenericRepository<Contact> ContactRepository { get; }
        IGenericRepository<ContactProvider> ContactProviderRepository { get; }
        IGenericRepository<Rating> RatingRepository { get; }
        IGenericRepository<Notification> NotificationRepository { get; }
        IGenericRepository<Offer> OfferRepository { get; }
        IGenericRepository<Report> ReportRepository { get; }
        Task SaveAsync();
    }
}
