using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GenericRepository
{
    public interface IRepository<T> : IRead<T>, IRecord<T> where T : class { }

    public interface IRead<T> where T : class
    {
        T Entity(Expression<Func<T, bool>> predicate = null, params string[] includes);

        List<T> Entities(Expression<Func<T, bool>> predicate = null, params string[] includes);

        int Count(Expression<Func<T, bool>> predicate = null, params string[] includes);

        decimal Sum(Expression<Func<T, decimal>> predicate = null, params string[] includes);
    }

    public interface IRecord<T> where T : class
    {
        bool Add(T entity);

        bool Add(IEnumerable<T> entities);

        bool Update(T entity);

        bool Update(IEnumerable<T> entities);

        bool Delete(T entity);

        bool Delete(IEnumerable<T> entities);
    }
}