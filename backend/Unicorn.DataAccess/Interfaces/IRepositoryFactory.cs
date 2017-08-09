using System.Data.Entity;

namespace Unicorn.DataAccess.Interfaces
{
    public interface IRepositoryFactory
    {
        IGenericRepository<T> CreateRepository<T>(DbContext context) where T : class, IEntity;
    }
}
