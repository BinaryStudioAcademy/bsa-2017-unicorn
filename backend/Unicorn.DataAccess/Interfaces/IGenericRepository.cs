using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Unicorn.DataAccess.Interfaces
{
    public interface IRepository
    { }
    public interface IGenericRepository<TEntity> : IRepository
    {
        void Delete(object id);
        void Delete(TEntity entityToDelete);
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        TEntity GetByID(object id);
        void Insert(TEntity entity);
        void Update(TEntity entityToUpdate);
    }
}
