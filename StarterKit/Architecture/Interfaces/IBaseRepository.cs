using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace StarterKit.Architecture.Interfaces
{
    public interface IBaseRepository
    {

    }

    public interface IBaseRepository<T, TKey> : IBaseRepository
        where T : class, new() 
    {
        IEnumerable<T> Index(params Expression<Func<T, object>>[] includeProperties);

        T Read(TKey id, params Expression<Func<T, object>>[] includeProperties);

        T FindBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);

        T Create(T entity);

        T Update(T entity);

        void Delete(TKey id);

        void Delete(T entity);

        void DeleteBy(Expression<Func<T, bool>> predicate);
    }
}