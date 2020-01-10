using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace GenericRepository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public readonly DbContext Context;

        private readonly DbSet<T> _table;

        public Repository(DbContext context)
        {
            Context = context;

            _table = context.Set<T>();
        }

        public virtual T Entity(Expression<Func<T, bool>> predicate = null, params string[] includes)
        {
            IQueryable<T> query = Queryable(includes);

            if (predicate != null)
            {
                return query.FirstOrDefault(predicate);
            }

            return query.FirstOrDefault();
        }

        public virtual async Task<T> EntityAsync(Expression<Func<T, bool>> predicate = null, params string[] includes)
        {
            IQueryable<T> query = Queryable(includes);

            if (predicate != null)
            {
                return await query.FirstOrDefaultAsync(predicate);
            }

            return await query.FirstOrDefaultAsync();
        }

        public virtual List<T> Entities(Expression<Func<T, bool>> predicate = null, params string[] includes)
        {
            IQueryable<T> query = Queryable(includes);

            if (predicate != null)
            {
                return query.Where(predicate).ToList();
            }

            return query.ToList();
        }

        public virtual async Task<List<T>> EntitiesAsync(Expression<Func<T, bool>> predicate = null, params string[] includes)
        {
            IQueryable<T> query = Queryable(includes);

            if (predicate != null)
            {
                return query.Where(predicate).ToList();
            }

            return await query.ToListAsync();
        }

        public virtual int Count(Expression<Func<T, bool>> predicate = null, params string[] includes)
        {
            IQueryable<T> query = Queryable(includes);

            if (predicate != null)
            {
                return query.Count(predicate);
            }

            return query.Count();
        }

        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null, params string[] includes)
        {
            IQueryable<T> query = Queryable(includes);

            if (predicate != null)
            {
                return await query.CountAsync(predicate);
            }

            return await query.CountAsync();
        }

        public virtual decimal Sum(Expression<Func<T, decimal>> selector, params string[] includes)
        {
            IQueryable<T> query = Queryable(includes);

            return query.Sum(selector);
        }

        public virtual async Task<decimal> SumAsync(Expression<Func<T, decimal>> selector = null, params string[] includes)
        {
            IQueryable<T> query = Queryable(includes);

            return await query.SumAsync(selector);
        }

        public virtual bool Add(T entity)
        {
            Context.Set<T>().Add(entity);

            Context.SaveChanges();

            return true;
        }

        public virtual async Task<bool> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await Context.Set<T>().AddAsync(entity, cancellationToken);

            await Context.SaveChangesAsync(cancellationToken);

            return true;
        }

        public virtual bool Add(IEnumerable<T> entities)
        {
            Context.Set<T>().AddRange(entities);

            Context.SaveChanges();

            return true;
        }

        public virtual async Task<bool> AddAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            await Context.Set<T>().AddRangeAsync(entities, cancellationToken);

            await Context.SaveChangesAsync(cancellationToken);

            return true;
        }

        public virtual bool Update(T entity)
        {
            Context.Set<T>().Update(entity);

            Context.SaveChanges();

            return true;
        }

        public virtual bool Update(IEnumerable<T> entities)
        {
            Context.Set<T>().UpdateRange(entities);

            Context.SaveChanges();

            return true;
        }

        public virtual bool Delete(T entity)
        {
            Context.Set<T>().Remove(entity);

            Context.SaveChanges();

            return true;
        }

        public virtual bool Delete(IEnumerable<T> entities)
        {
            Context.Set<T>().RemoveRange(entities);

            Context.SaveChanges();

            return true;
        }

        private IQueryable<T> Queryable(params string[] includes)
        {
            IQueryable<T> query = _table.AsQueryable();

            if (includes != null)
            {
                for (int i = 0; i < includes.Length; i++)
                {
                    query = query.Include(includes[i]);
                }
            }

            return query;
        }
    }
}