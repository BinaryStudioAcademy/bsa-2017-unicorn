﻿using Ninject.Extensions.Factory;
using Ninject.Modules;
using Unicorn.DataAccess.Context;
using Unicorn.DataAccess.Interfaces;
using Unicorn.DataAccess.Repositories.UnitOfWork;

namespace Unicorn.DataAccess.Infrastructure
{
    public class DataAccessModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IRepositoryFactory>().ToFactory();
            Bind<AppContext>().ToSelf();
            Bind(typeof(IGenericRepository<>)).To(typeof(IGenericRepository<>));
            Bind<IUnitOfWork>().To<UnitOfWork>();
        }
    }
}
