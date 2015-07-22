using System.Collections.Generic;

namespace StarterKit.Architecture.Interfaces
{
    public interface IRepository<T, TKey>
    {
        List<T> Index();
        T Read(TKey id);
        T Create(T entity);
        bool Update(T entity);
    }
}