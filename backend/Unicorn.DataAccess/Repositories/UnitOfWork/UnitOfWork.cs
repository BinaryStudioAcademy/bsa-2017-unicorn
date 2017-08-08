using System;
using System.Threading.Tasks;
using Unicorn.DataAccess.Context;

namespace Unicorn.DataAccess.Repositories.UnitOfWork
{
    class UnitOfWork : IDisposable
    {
        private AppContext context;

        public UnitOfWork()
        {
            context = new AppContext("DefaultConnection");
        }
        public UnitOfWork(string connectionString)
        {
            context = new AppContext(connectionString);
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
