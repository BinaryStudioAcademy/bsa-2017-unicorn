using Microsoft.AspNet.SignalR;
using Ninject.Modules;
using Unicorn.Core.Infrastructure.SignalR;
using Unicorn.Core.Interfaces;
using Unicorn.Core.Providers;
using Unicorn.Core.Services;

namespace Unicorn.Core.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ILocationService>().To<LocationService>();
            Bind<IMembershipProvider>().To<MembershipProvider>();
            Bind<IAuthService>().To<AuthJWTService>();
            Bind<IAccountService>().To<AccountService>();
            Bind<IBookService>().To<BookService>();
            Bind<IDBService>().To<DBService>();
            Bind<IMailService>().To<MailService>();
            Bind<IHistoryService>().To<HistoryService>();
            Bind<ICalendarService>().To<CalendarService>();
            Bind<ICustomerService>().To<CustomerService>();
            Bind<IChatService>().To<ChatService>();
            Bind<IVendorService>().To<VendorService>();
            Bind<IRoleService>().To<RoleService>();
            Bind<IWorkService>().To<WorkService>();
            Bind<IPersonService>().To<PersonService>();
            Bind<IPermissionService>().To<PermissionService>();
            Bind<ICompanyService>().To<CompanyService>();
            Bind<IAvatarService>().To<AvatarService>();
            Bind<IPortfolioService>().To<PortfolioService>();
            Bind<IReviewService>().To<ReviewService>();
            Bind<ICategoryService>().To<CategoryService>();
            Bind<ISubcategoryService>().To<SubcategoryService>();
            Bind<ICompanyPageService>().To<CompanyPageService>();
            Bind<IContactService>().To<ContactService>();
            Bind<IPopularService>().To<PopularService>();
            Bind<IRatingService>().To<RatingService>();
            Bind<INotificationProxy>().To<NotificationProxy>()
                .WithConstructorArgument("context", GlobalHost.ConnectionManager.GetHubContext<NotificationHub>());
            Bind<INotificationService>().To<NotificationService>();
            Bind<ISearchService>().To<SearchService>();
            Bind<IOfferService>().To<OfferService>();
            Bind<IAdminService>().To<AdminService>();
            Bind<IAnalyticsService>().To<AnalyticsService>();
            Bind<IReportService>().To<ReportService>();
        }
    }
}
