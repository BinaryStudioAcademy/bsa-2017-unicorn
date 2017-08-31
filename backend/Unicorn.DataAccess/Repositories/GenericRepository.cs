using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Unicorn.DataAccess.Context;
using Unicorn.DataAccess.Interfaces;

namespace Unicorn.DataAccess.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
    {
        private DbContext context;
        private IDbSet<T> _entities;

        public GenericRepository(DbContext context)
        {
            this.context = context;
        }
        public T GetById(long id)
        {
            return Query.First(x => x.Id == id);
        }
        public async Task<T> GetByIdAsync(long id)
        {
            return await Query.SingleOrDefaultAsync(i => i.Id == id);

        }

        public async Task<T> GetByIdDeletedAsync(long id)
        {
            return await Deleted.SingleOrDefaultAsync(i => i.Id == id);
        }

        public T Create(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }
                return Entities.Add(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }
                context.Entry(entity).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(long id)
        {
            try
            {
                var entity = GetById(id);
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(id));
                }

                entity.IsDeleted = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task ForceDelete(long id)
        {
            try
            {
                var entity = await GetByIdDeletedAsync(id);
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(id));
                }
                Entities.Remove(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Restore(long id)
        {
            try
            {
                var entity = await GetByIdDeletedAsync(id);
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(id));
                }
                entity.IsDeleted = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<T> GetAll()
        {
            return Query.ToList();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Query.ToListAsync();
        }

        protected IDbSet<T> Entities => _entities ?? (_entities = context.Set<T>());

        public IQueryable<T> Query => Entities.Where(x => !x.IsDeleted);
        public IQueryable<T> Deleted => Entities.Where(x => x.IsDeleted);
    }
}
