using Ninject.Modules;
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
            Bind<ICustomerService>().To<CustomerService>();
            Bind<IVendorService>().To<VendorService>();
            Bind<IRoleService>().To<RoleService>();
            Bind<IWorkService>().To<WorkService>();
            Bind<IPersonService>().To<PersonService>();
        }
    }
}
