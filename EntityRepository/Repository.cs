using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

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

            IQueryable<T> query = _table.AsQueryable();

            if (includes != null)
            {
                for (int i = 0; i < includes.Length; i++)
                {
                    query = query.Include(includes[i]);
                }
            }

            if (predicate != null)
            {
                return query.FirstOrDefault(predicate);
            }

            return query.FirstOrDefault();
        }

        public virtual List<T> Entities(Expression<Func<T, bool>> predicate = null, params string[] includes)
        {
            IQueryable<T> query = _table.AsQueryable();

            if (includes != null)
            {
                for (int i = 0; i < includes.Length; i++)
                {
                    query = query.Include(includes[i]);
                }
            }

            if (predicate != null)
            {
                return query.Where(predicate).ToList();
            }

            return query.ToList();
        }

        public virtual int Count(Expression<Func<T, bool>> predicate = null, params string[] includes)
        {
            IQueryable<T> query = _table.AsQueryable();

            if (includes != null)
            {
                for (int i = 0; i < includes.Length; i++)
                {
                    query = query.Include(includes[i]);
                }
            }

            if (predicate != null)
            {
                return query.Count(predicate);
            }

            return query.Count();
        }

        public virtual decimal Sum(Expression<Func<T, decimal>> predicate, params string[] includes)
        {
            IQueryable<T> query = _table.AsQueryable();

            if (includes != null)
            {
                for (int i = 0; i < includes.Length; i++)
                {
                    query = query.Include(includes[i]);
                }
            }

            return query.Sum(predicate);
        }

        public virtual bool Add(T entity)
        {
            Context.Set<T>().Add(entity);

            Context.SaveChanges();

            return true;
        }

        public virtual bool Add(IEnumerable<T> entities)
        {
            Context.Set<T>().AddRange(entities);

            Context.SaveChanges();

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
    }
}