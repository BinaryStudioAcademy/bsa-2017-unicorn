using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Unicorn.DataAccess.Interfaces
{
    public interface IRepository
    { }
    public interface IGenericRepository<T> : IRepository
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(long id);
        Task<T> GetByIdDeletedAsync(long id);
        T Create(T item);
        void Update(T item);
        void Delete(long id);
        Task ForceDelete(long id);
        Task Restore(long id);
        IQueryable<T> Query { get; }
        IQueryable<T> Deleted { get; }
    }
}
