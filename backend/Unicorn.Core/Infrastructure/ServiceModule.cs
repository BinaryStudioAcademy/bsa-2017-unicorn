using Ninject.Modules;
using Unicorn.Core.Interfaces;
using Unicorn.Core.Providers;
using Unicorn.Core.Services;

namespace Unicorn.Shared.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ILocationService>().To<LocationService>();
            Bind<IMembershipProvider>().To<MembershipProvider>();
            Bind<IAuthService>().To<AuthJWTService>();
        }
    }
}
