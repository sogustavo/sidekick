using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace GenericRepository
{
    public interface IRepository<T> : IRead<T>, IRecord<T> where T : class { }

    public interface IRead<T> where T : class
    {
        T Entity(Expression<Func<T, bool>> predicate = null, params string[] includes);

        Task<T> EntityAsync(Expression<Func<T, bool>> predicate = null, params string[] includes);

        IEnumerable<T> Entities(Expression<Func<T, bool>> predicate = null, params string[] includes);

        Task<List<T>> EntitiesAsync(Expression<Func<T, bool>> predicate = null, params string[] includes);

        int Count(Expression<Func<T, bool>> predicate = null, params string[] includes);

        Task<int> CountAsync(Expression<Func<T, bool>> predicate = null, params string[] includes);

        decimal Sum(Expression<Func<T, decimal>> selector, params string[] includes);

        Task<decimal> SumAsync(Expression<Func<T, decimal>> selector, params string[] includes);
    }

    public interface IRecord<T> where T : class
    {
        bool Add(T entity);

        Task<bool> AddAsync(T entity, CancellationToken cancellationToken = default);

        bool Add(IEnumerable<T> entities);

        Task<bool> AddAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

        bool Update(T entity);

        bool Update(IEnumerable<T> entities);

        bool Delete(T entity);

        bool Delete(IEnumerable<T> entities);
    }
}