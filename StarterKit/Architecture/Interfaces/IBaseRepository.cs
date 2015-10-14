using System.Collections.Generic;

namespace StarterKit.Architecture.Interfaces
{
    public interface IBaseRepository
    {

    }

    public interface IBaseRepository<T, TKey> : IBaseRepository
        where T : class, new() 
    {
        IEnumerable<T> Index();

        T Read(TKey id);

        T Create(T entity);

        T Update(T entity);

        void Delete(TKey id);

        void Delete(T entity);
    }
}