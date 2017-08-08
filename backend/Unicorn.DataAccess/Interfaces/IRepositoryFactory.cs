using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorn.DataAccess.Interfaces
{
    public interface IRepositoryFactory
    {
        IGenericRepository<TEntity> CreateRepository<TEntity>(DbContext context) where TEntity : class;
    }
}
