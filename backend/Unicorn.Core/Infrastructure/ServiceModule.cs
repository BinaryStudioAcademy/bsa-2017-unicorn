using Ninject.Modules;
using Unicorn.Core.Interfaces;
using Unicorn.Core.Services;

namespace Unicorn.Shared.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ILocationService>().To<LocationService>();
            Bind<IAccountService>().To<AccountService>();
            Bind<IBookService>().To<BookService>();
            Bind<ICustomerService>().To<CustomerService>();
        }
    }
}
